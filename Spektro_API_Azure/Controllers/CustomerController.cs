using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        [HttpPost]
        public ActionResult Post([FromBody] CustomerModel input)
        {
            string commandString = "INSERT INTO Users (UserRole, Email, Kodeord, FirstName, LastName, EmailNotification, SmsNotification, PhoneNo)" +
                                   "VALUES(@UserRole, @Email, @Kodeord, @FirstName, @LastName, @EmailNotification, @SmsNotification, @PhoneNo)";



            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(commandString, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserRole", input.UserRole);
                        cmd.Parameters.AddWithValue("@Email", input.Email);
                        cmd.Parameters.AddWithValue("@Kodeord", input.Kodeord);
                        cmd.Parameters.AddWithValue("@FirstName", input.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", input.LastName);
                        cmd.Parameters.AddWithValue("@EmailNotification", input.EmailNotification);
                        cmd.Parameters.AddWithValue("@SmsNotification", input.SmsNotification);
                        cmd.Parameters.AddWithValue("@PhoneNo", input.PhoneNo);

                        int effectedDBrows = cmd.ExecuteNonQuery();

                        return Problem();
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;

            }
        }
    }
}
