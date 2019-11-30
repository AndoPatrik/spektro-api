using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableReservationController : ControllerBase
    {
        private static ReservationModel BuildReservationModel(SqlDataReader reader) 
        {
            ReservationModel reservation = new ReservationModel();
            //TODO : Implement SQL data => C# obj
            return reservation;
        }

        // GET: api/TableReservation
        [HttpGet]
        public List<ReservationModel> GetAllReservations()
        {
            ReservationModel r = new ReservationModel();
            r.EmailAddress = "yo";
            return new List<ReservationModel> { r };
        }

        // GET: api/TableReservation/Mark
        [HttpGet("{name}", Name = "GetReservationByName")]
        public ReservationModel GetReservationByName(string name)
        {
            return new ReservationModel();
        }

        // POST: api/TableReservation
        [HttpPost]
        public ActionResult Post([FromBody] ReservationModel input)
        {
            string commandString = "INSERT INTO Reservations (FirstName, LastName, Email, PhoneNo, EmailNotification, SmsNotification, NoOfPeople , DateOfReservation)"+
                                   "VALUES(@FirstName, @LastName, @Email, @PhoneNo, @EmailNoti, @SmsNoti, @NoOfPeople, @DateOfReservation)";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(commandString, connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", input.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", input.LastName);
                    cmd.Parameters.AddWithValue("@Email", input.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNo", input.PhoneNumber);
                    cmd.Parameters.AddWithValue("@EmailNoti", input.EmailNotifcation);
                    cmd.Parameters.AddWithValue("@SmsNoti", input.SmsNotification);
                    cmd.Parameters.AddWithValue("@NoOfPeople", input.NoOfPeople);
                    cmd.Parameters.AddWithValue("@DateOfReservation", input.DateOfReservation);

                    int effectedDBrows = cmd.ExecuteNonQuery();

                    if (effectedDBrows > 0)
                    {

                        return Ok();
                    }

                    return Problem();
                }
            }
        }

        // PUT: api/TableReservation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ReservationModel input)
        {
            //TODO: Update reservation in DB
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           //TODO: Delete reservation record.
        }
    }
}
