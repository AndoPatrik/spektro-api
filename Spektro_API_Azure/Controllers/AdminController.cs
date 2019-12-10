using System;
using System.Collections.Generic;
using System.Data;
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
            int id = reader.GetInt32(8);




            ReservationModel reservation = new ReservationModel
            {
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = emailadress,
                PhoneNumber = phonenumber,
                EmailNotifcation = emailnotification,
                SmsNotification = smsnotification,
                NoOfPeople = noofpeople,
                DateOfReservation = dateofreservation,
                Id = id,
            };
            return reservation;
        }

        // GET: api/Admin/5
        [HttpGet]
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
        [Route("TableReservation/Update/{id}")]
        public ActionResult UpdateReservation(int id, [FromBody] ReservationModel input)
        {
            string updateString = "update Reservations set FirstName = @FirstName , LastName = @LastName , Email = @Email ,PhoneNo = @PhoneNo , EmailNotification = @EmailNotification, SmsNotification = @SmsNotification, NoOfPeople = @NoOfPeople, DateOfReservation = @DateOfReservation where Id = @Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(updateString, connection))
                {
                    //TODO cmd.Params.Addwithvalue
                    cmd.Parameters.AddWithValue("@FirstName", input.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", input.LastName);
                    cmd.Parameters.AddWithValue("@Email", input.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNo", input.PhoneNumber);
                    cmd.Parameters.AddWithValue("@EmailNotification", input.EmailNotifcation);
                    cmd.Parameters.AddWithValue("@SmsNotification", input.SmsNotification);
                    cmd.Parameters.AddWithValue("@NoOfPeople", input.NoOfPeople);
                    cmd.Parameters.AddWithValue("@DateOfReservation ", input.DateOfReservation);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int updatedRows = cmd.ExecuteNonQuery();

                    if (updatedRows < 0) 
                    {
                        return Conflict("The reservations could not be updated on id: " + id );
                    }
                    return Ok("Reservation updated on id: " + id);
                }
            }
        }

        // DELETE: api/ApiWithActions/5

        
        [HttpDelete("{id}")]
        [Route("TableReservation/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            string deleteString = "DELETE FROM Reservations WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(deleteString, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    int rowEffected = cmd.ExecuteNonQuery();

                    if (rowEffected < 0)
                    {
                        return NotFound("Reservation couldn't be found on id: " + id);
                    }
                    return Ok("Reservation deleted on id: " + id);
                }
            }
        }
    }
}