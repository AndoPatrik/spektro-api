using Habanero.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;
using System.Collections.Generic;
using System.Data;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        
        [HttpGet]
        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            const string selectString = "select * from Users order by id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            { 
                databaseConnection.Open();
            using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
            {
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    List<CustomerModel> customerList = new List<CustomerModel>();
                    while (reader.Read())
                    {
                        CustomerModel customer = ReadCustomers(reader);
                        customerList.Add(customer);
                    }
                    return customerList;
                }
            }
        }
        }


        private static CustomerModel ReadCustomers(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string userrole = reader.IsDBNull(1) ? null : reader.GetString(1);
            string email = reader.IsDBNull(2) ? null : reader.GetString(2);
            string kodeord = reader.IsDBNull(3) ? null : reader.GetString(3);
            string firstname = reader.IsDBNull(4) ? null : reader.GetString(4);
            string lastname = reader.IsDBNull(5) ? null : reader.GetString(5);
            bool emailnotification = reader.GetBoolean(6);
            bool smsnotification = reader.GetBoolean(7);
            string phoneno = reader.IsDBNull(8) ? null : reader.GetString(8);


            CustomerModel customer = new CustomerModel
            {
                Id = id,
                UserRole = userrole,
                Email = email,
                Kodeord = kodeord,
                FirstName = firstname,
                LastName = lastname,
                EmailNotification = emailnotification,
                SmsNotification = smsnotification,
                PhoneNo = phoneno
            };
            return customer;
        }

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
