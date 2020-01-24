using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spektro_API_Azure.Service;
using System;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DynamicEmailController : ControllerBase
    {
        // GET: api/DynamicEmail
        [HttpGet]
        public string Get()
        {
            return "Dynamic email sender works!";
        }

        // POST: api/DynamicEmail
        [HttpGet]
        public IActionResult Post(string firstname, string lastname, string email, DateTime date, string mailtype, string subject)
        {
            try
            {
                DynamicEmailSender.SendCustomEmail(firstname, lastname, email.ToLower(), date, mailtype.ToUpper(), subject, errorText: null);
                return Ok("Email sent successfully.");
            }
            catch (System.Exception)
            {
                return NotFound("Error. Email was not sent.");
                throw;
                //TODO: Log to DB
            }
        }
    }
}
