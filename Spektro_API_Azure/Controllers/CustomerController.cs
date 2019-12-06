﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private static CustomerModel BuildCustomerObj(SqlDataReader reader) 
        {
            CustomerModel customerFromSql = new CustomerModel();
            customerFromSql.Id = reader.GetInt32(0);
            customerFromSql.UserRole = reader.GetString(1);
            customerFromSql.UserRole = customerFromSql.UserRole.Trim();
            customerFromSql.Email = reader.GetString(2);
            customerFromSql.Email = customerFromSql.Email.Trim();
            customerFromSql.Kodeord = reader.GetString(3);
            customerFromSql.Kodeord = customerFromSql.Kodeord.Trim();
            customerFromSql.FirstName = reader.GetString(4);
            customerFromSql.FirstName = customerFromSql.FirstName.Trim();
            customerFromSql.LastName = reader.GetString(5);
            customerFromSql.LastName = customerFromSql.LastName.Trim();
            customerFromSql.EmailNotification = reader.GetBoolean(6);
            customerFromSql.SmsNotification = reader.GetBoolean(7);
            customerFromSql.PhoneNo = reader.GetString(8);
            customerFromSql.PhoneNo = customerFromSql.PhoneNo.Trim();
            return customerFromSql;
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "GetSingleUser")]
        public ActionResult GetSingleUser(int id)
        {
            string commandStringSelect = "SELECT * FROM Users WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(commandStringSelect, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        CustomerModel customerToRetrieve = new CustomerModel();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                customerToRetrieve = BuildCustomerObj(reader);
                            }
                        }

                        if (customerToRetrieve.Id != 0)
                        {
                            return Ok(customerToRetrieve);
                        }
                        return NotFound("No customer on ID: " + id);
                    }
                }
            }
        }

        // POST: api/Customer
        [HttpPost]
        public ActionResult RegisterCustomer([FromBody] CustomerModel input)
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

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] CustomerModel input)
        {
            string commandStringUpdate = "UPDATE Users SET UserRole = @UserRole,Email = @Email, Kodeord = @Kodeord, FirstName = @FirstName," +
                        " LastName = @LastName, EmailNotification = @EmailNoti, SmsNotification = @SmsNoti, PhoneNo = @PhoneNo WHERE Id = @Id;";


            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(commandStringUpdate, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", input.Id);
                    cmd.Parameters.AddWithValue("@UserRole", input.UserRole);
                    cmd.Parameters.AddWithValue("@Email", input.Email);
                    cmd.Parameters.AddWithValue("@Kodeord", input.Kodeord);
                    cmd.Parameters.AddWithValue("@FirstName", input.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", input.LastName);
                    cmd.Parameters.AddWithValue("@EmailNoti", input.EmailNotification);
                    cmd.Parameters.AddWithValue("@SmsNoti", input.SmsNotification);
                    cmd.Parameters.AddWithValue("@PhoneNo", input.PhoneNo);

                    int updatedRow = cmd.ExecuteNonQuery();

                    if (updatedRow > 0 )
                    {
                        return Ok("Record on id= " +id + " has been updated.");
                    }

                    return Conflict("No record was updated.");
                }
            }
        }
    }
}
