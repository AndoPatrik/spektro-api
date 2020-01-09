using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ServicesSetupController : ControllerBase
    {
        private int _hour = 7;
        private int _minute = 0;
        private AutomatizedServiceController asc = new AutomatizedServiceController(); //TODO : Implement dependency injection istead of direct obj creation.
        //TODO : Exception handling for all + logging

        [Route("SetJob/ReservationNotifications/h:{hour}/m:{minute}")]
        [HttpGet]
        public ActionResult ApplyOrUpdateReservationNotifications(int hour, int minute)
        {
            _hour = hour;
            _minute = minute;

            RecurringJob.AddOrUpdate(recurringJobId: "DailyReservationNotification", methodCall: () => asc.NotifyAllReservationToday(), Cron.Daily(_hour,_minute)); 
            return Ok();
        }


        [Route("SetJob/WaiterReminder/h:{hour}/m:{minute}")]
        [HttpGet]
        public ActionResult ApplyOrUpdateWaiterNotifications(int hour, int minute)
        {
            _hour = hour;
            _minute = minute;

            RecurringJob.AddOrUpdate(recurringJobId: "DailyWaiterNotification", methodCall: () => asc.NotifyMainWaiter(), Cron.Daily(_hour, _minute));
            return Ok();
        }


    }
}
