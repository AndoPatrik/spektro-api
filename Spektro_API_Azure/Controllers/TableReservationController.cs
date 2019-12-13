using System;
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
            reservation.FirstName = reader.GetString(0);
            reservation.LastName = reader.GetString(1);
            reservation.EmailAddress = reader.GetString(2);
            reservation.PhoneNumber = reader.GetString(3);
            reservation.EmailNotifcation = reader.GetBoolean(4);
            reservation.SmsNotification = reader.GetBoolean(5);
            reservation.NoOfPeople = reader.GetInt32(6);
            reservation.DateOfReservation = reader.GetDateTime(7);
            return reservation;
        }

        private static bool ValidateData(ReservationModel reservation) 
        {
            if (reservation.FirstName!= null || reservation.FirstName != string.Empty) //TODO : Implement all casees
            {
                return true;
            }
            return false;
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

            

            try 
            {
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
                            if (input.EmailNotifcation)
                            {
                                try
                                {
                                    EmailSenderService.SendEmailNotificationForReservation(input);
                                }
                                catch (System.Exception) 
                                {
                                    throw;
                                }
                                
                            }
                            if (input.SmsNotification)
                            {
                                //TODO: Implement SMS sender service.
                            }
                            return Ok();
                        }

                        return Problem();
                    }
                }
            }
            catch (SqlException e) 
            {
                throw e;

            }
        }

        [HttpGet]
        [Route("NotifyAll")]
        public ActionResult NotifyAll() 
        {
            string todaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd").Replace(".","-").Trim();
            string cmdTodaysReservations = $"select * from Reservations where DateOfReservation = '{todaysDate}' and EmailNotification='true' or  DateOfReservation = '{todaysDate}' and SmsNotification = 'true'";
            List<ReservationModel> todaysReservations = new List<ReservationModel> { };
            int successful = 0;
            int failed = 0;
            int sentEmail = 0;
            int sentSMS = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdTodaysReservations, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    ReservationModel rm = BuildReservationModel(reader);
                                    todaysReservations.Add(rm);

                                    if (reader.GetBoolean(4) == true)
                                    {
                                        //TODO: CALL EMAIL SENDER
                                        sentEmail++;
                                        successful++;
                                    }
                                    if (reader.GetBoolean(5) == true)
                                    {
                                        //TODO: CALL SMS SENDER
                                        sentSMS++;
                                        successful++;
                                    }
                                }
                            }
                        }
                    }
                }
                return Ok($"Today we have {todaysReservations.Count} reservations a day prior. We have sent {successful} reminder. We failed to send to {failed} email. Emails = {sentEmail} , SMS = {sentSMS}");
            }
            catch (SqlException e)
            {
                return NotFound(e.ToString());
            }
        }
    }
}
