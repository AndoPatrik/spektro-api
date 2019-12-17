using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomatizedServiceController : ControllerBase
    { 
        private string todaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd").Replace(".", "-").Trim();
        
        [HttpGet]
        [Route("NotifyAllReservations")]
        public ActionResult NotifyAllReservationToday()
        {
           
            string cmdTodaysReservations = $"select * from Reservations where Format([DateOfReservation], 'yyyy-MM-dd') = '{todaysDate}' and EmailNotification='true' or Format([DateOfReservation], 'yyyy-MM-dd') = '{todaysDate}' and SmsNotification = 'true'";
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
                                    ReservationModel rm = TableReservationController.BuildReservationModel(reader);
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

        private string reservationRemainderStringBuilder(List<ReservationModel> reservations) 
        {
            string s = "";

            foreach (var res in reservations)
            {
                s = s + $" {res.NoOfPeople} person at {res.DateOfReservation.TimeOfDay} | ";
            }

            if (reservations.Count <= 0) 
            {
                return "reservations during the day!";
            }
            return s;
        }

        [HttpGet]
        [Route("NotifyMainWaiter")]
        public ActionResult NotifyMainWaiter() 
        {
            string cmdTodaysReservations = $"select * from Reservations where Format([DateOfReservation], 'yyyy-MM-dd') = '{todaysDate}'";
            List<ReservationModel> todaysReservations = new List<ReservationModel> { };

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
                                    ReservationModel rm = TableReservationController.BuildReservationModel(reader);
                                    todaysReservations.Add(rm);

                                }
                            }
                            //string textToWaiter = $"Remember today we already have {todaysReservations.Count} reservations. Be prepared for {reservationRemainderStringBuilder(todaysReservations)} ";
                            //TODO : Send email to the main waiter 
                        }
                    }
                }
                return Ok($"Remember today we already have {todaysReservations.Count} reservations. Be prepared for {reservationRemainderStringBuilder(todaysReservations)}.");
            }
            catch (SqlException e)
            {
                return NotFound(e.ToString());
            }
        }
    }
}
