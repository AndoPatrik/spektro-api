using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Service;
using System;

namespace Spektro_API_Azure.Controllers
{
[Route("api/[controller]")]
    [ApiController]
    public class DynamicEmailController : ControllerBase
    {
        // GET: api/DynamicEmail
        [HttpGet]
        public string Get()
        {
            return "Dynamic email sender works!";
        }

        // POST: api/DynamicEmail
        [HttpPost]
        public string Post(string firstname, string lastname, string email, DateTime date, string mailtype, string subject)
        {
            try
            {
                DynamicEmailSender.SendCustomEmail(firstname, lastname, email.ToLower(), date, mailtype.ToUpper(), subject);
            }
            catch (System.Exception)
            {
                throw;
            }

            return "Email sent!";
        }
    }
}
