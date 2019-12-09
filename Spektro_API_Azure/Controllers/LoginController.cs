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
 //   [Authorize] Use this to make a controller avaliable to a certain audience
    public class LoginController : ControllerBase
    {

        // POST: api/Login
        [HttpPost]      
        public ActionResult AuthorizeLogin(dynamic user)
        {
            string checkDatabaseForUser = "SELECT * FROM Users WHERE Email = @email AND Kodeord = @kodeord";

            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
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
                            // advance cursor to first row
                            reader.Read();

                            // gets the ID of the user
                            int userID = reader.GetInt32(0);
                            string role = reader.GetString(1).Trim(); 

                            // Security Key  from Service
                            string securityKey = ConnectionString.GetSecretKey();

                            // Symmetric Key
                            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                            // Signing credentials
                            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                            // Add Claims
                             var claims = new List<Claim>();
                            claims.Add(new Claim("Identity", $"{userID}"));
                            claims.Add(new Claim("Role", $"{role}"));


                            // Create token
                            var token = new JwtSecurityToken(
                                    issuer: "spektro.sk",
                                    audience: "users",
                                    expires: DateTime.Now.AddHours(20),
                                    signingCredentials: signingCredentials,
                                    claims: claims
                                    ); ;
                            

                            // return token
                            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                        }
                        return StatusCode(401);
                    }
                }
            }
        }


      }
}
