using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        [HttpPost]
        public ActionResult Post([FromBody] CustomerModel input)
        {
            string commandStringSelect = "select * from Users where Email = @Email OR PhoneNo = @PhoneNo;";

            string commandStringInsert = "INSERT INTO Users (UserRole, Email, Kodeord, FirstName, LastName, EmailNotification, SmsNotification, PhoneNo)" +
                                   "VALUES(@UserRole, @Email, @Kodeord, @FirstName, @LastName, @EmailNotification, @SmsNotification, @PhoneNo)";

            if (input.UserRole.Length > 10 || input.Email.Length > 50 || input.Kodeord.Length > 60 || input.FirstName.Length > 30 || 
                input.LastName.Length > 30 || input.PhoneNo.Length > 20 || input.EmailNotification.GetType() != typeof(bool) ||
                input.SmsNotification.GetType() != typeof(bool))

            {
                return Conflict("Incorrect data format or length");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand checkCmd = new SqlCommand(commandStringSelect, connection)) 
                    {
                        checkCmd.Parameters.AddWithValue("@Email", input.Email);
                        checkCmd.Parameters.AddWithValue("@PhoneNo", input.PhoneNo);

                        using (SqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return Conflict("This email or phone number has already been used.");
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand(commandStringInsert, connection))
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

                            if (effectedDBrows > 0)
                            {
                                return Ok("User created");
                            }
                            return Conflict();
                        }
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
