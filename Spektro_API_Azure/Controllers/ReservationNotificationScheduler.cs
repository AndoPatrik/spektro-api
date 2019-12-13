using System;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationNotificationScheduler : ControllerBase
    {
        private int _hour = 7;
        private int _minute = 0;
        private TableReservationController trc = new TableReservationController();


        [Route("NextTrigger")]
        [HttpGet]
        public ActionResult GetSetupInfo()
        {

            return Ok($"Notifications will be sent each day at {_hour}:{_minute}") ;
        }

        [Route("SetJob/h:{hour}/m:{minute}")]
        [HttpGet]
        public ActionResult ApplyOrUpdateReservationNotifications(int hour, int minute)
        {
            _hour = hour;
            _minute = minute;

            RecurringJob.AddOrUpdate(recurringJobId: "DailyReservationNotification", methodCall: () => trc.NotifyAll(), Cron.Daily(_hour,_minute)); 
            return Ok();
        }
    }
}
