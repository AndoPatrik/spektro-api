using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;
using System;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class TableReservationController : ControllerBase
    {
        public static ReservationModel BuildReservationModel(SqlDataReader reader) 
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

        [HttpPost]
        public ActionResult Post([FromBody] ReservationModel input)
        {
            string commandString = "INSERT INTO Reservations (FirstName, LastName, Email, PhoneNo, EmailNotification, SmsNotification, NoOfPeople , DateOfReservation)"+
                                   "VALUES(@FirstName, @LastName, @Email, @PhoneNo, @EmailNoti, @SmsNoti, @NoOfPeople, @DateOfReservation)"; //TODO: Implement when the reservation was created.
            try 
            {
                using (SqlConnection connection = new SqlConnection(SecretStrings.GetConnectionString()))
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
                                    DynamicEmailSender.SendCustomEmail(input.FirstName, input.LastName, input.EmailAddress, input.DateOfReservation, "RESERVATION", "Spektro - Reservation confirmation", errorText: null);
                                }
                                catch (Exception e) 
                                {
                                    ErrorLogger.LogToDb(e.ToString(), "TableReservationController/Post");
                                    throw;
                                    // TODO: Log exception to db 
                                }
                                
                            }
                            if (input.SmsNotification)
                            {
                                //Enable only on demo day. 
                                //SMSService.SendSMS(input.PhoneNumber, $"Thank you for your reservation. See you at {input.DateOfReservation}.");
                            }
                            return Ok();
                        }
                        return Problem();
                    }
                }
            }
            catch (SqlException e) 
            {
               
                ErrorLogger.LogToDb(e.ToString(), "TableReservationController/Post");
                throw;
            }
        }
    }
}
