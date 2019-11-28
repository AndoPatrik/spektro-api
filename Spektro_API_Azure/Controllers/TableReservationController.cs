using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;

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
            return new List<ReservationModel>{ };
        }

        // GET: api/TableReservation/Mark
        [HttpGet("{name}", Name = "Get")]
        public ReservationModel GetReservationByName(string name)
        {
            return new ReservationModel();
        }

        // POST: api/TableReservation
        [HttpPost]
        public void Post([FromBody] ReservationModel input)
        {
            //TODO: Write values from input to DB.
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
