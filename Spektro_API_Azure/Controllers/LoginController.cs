using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class LoginController : ControllerBase
    {
        [HttpPost]      
        public ActionResult AuthorizeLogin(dynamic user) //TODO: Exception handling + logging
        {
            string checkDatabaseForUser = "SELECT * FROM Users WHERE Email = @email AND Kodeord = @kodeord";

            using (SqlConnection connection = new SqlConnection(SecretStrings.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(checkDatabaseForUser, connection))
                {
                    cmd.Parameters.AddWithValue("@email", user.email.ToString());
                    cmd.Parameters.AddWithValue("@kodeord", user.kodeord.ToString());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int userID = reader.GetInt32(0);
                            string role = reader.GetString(1).Trim(); 
             
                            // Symmetric Key
                            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretStrings.GetSecretKey()));

                            // Signing credentials
                            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                            // Add Claims
                            var claims = new List<Claim>();
                            claims.Add(new Claim("UserID", $"{userID}"));
                            claims.Add(new Claim(ClaimTypes.Role, $"{role}"));

                            // Create token
                            var token = new JwtSecurityToken(
                                    issuer: "spektro.sk",
                                    audience: "users",
                                    expires: DateTime.Now.AddHours(1),
                                    signingCredentials: signingCredentials,
                                    claims: claims
                                    ); ;
                            
                            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                        }
                        return StatusCode(401);
                    }
                }
            }
        }
    }
}
