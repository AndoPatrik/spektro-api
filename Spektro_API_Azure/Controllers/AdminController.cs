using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // GET: api/Admin
        [Route("TableReservation")]
        [HttpGet]
        public IEnumerable<ReservationModel> GetAllReservations()
        {
            const string selectString = "select * from Reservations order by id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<ReservationModel> reservationList = new List<ReservationModel>();
                        while (reader.Read())
                        {
                            ReservationModel reservation = ReadReservations(reader);
                            reservationList.Add(reservation);
                        }
                        return reservationList;
                    }
                }
            }
        }


        private static ReservationModel ReadReservations(IDataRecord reader)
        {

            string firstname = reader.IsDBNull(0) ? null : reader.GetString(0).Trim();
            string lastname = reader.IsDBNull(1) ? null : reader.GetString(1).Trim();
            string emailadress = reader.IsDBNull(2) ? null : reader.GetString(2).Trim();
            string phonenumber = reader.IsDBNull(3) ? null : reader.GetString(3).Trim();
            bool emailnotification = reader.GetBoolean(4);
            bool smsnotification = reader.GetBoolean(5);
            int noofpeople = reader.GetInt32(6);
            DateTime dateofreservation = reader.GetDateTime(7);




            ReservationModel reservation = new ReservationModel
            {
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = emailadress,
                PhoneNumber = phonenumber,
                EmailNotifcation = emailnotification,
                SmsNotification = smsnotification,
                NoOfPeople = noofpeople,
                DateOfReservation = dateofreservation
            };
            return reservation;
        }

        // GET: api/Admin/5
        [Route("TableReservation/{id}")]
        public ReservationModel GetReservationById(int id)
        {
            const string selectString = "select * from Reservations where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); // advance cursor to first row
                        return ReadReservations(reader);
                    }
                }
            }
        }

        // POST: api/Admin
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}