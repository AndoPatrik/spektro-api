namespace Spektro_API_Azure.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using global::Spektro_API_Azure.Model;
    using global::Spektro_API_Azure.Service;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;

    namespace Spektro_API_Azure.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CouponController : ControllerBase
        {
            // GET: api/Coupon
            [HttpGet]
            public IEnumerable<CouponModel> GetAllCoupons()
            {
                const string selectString = "select * from Coupons order by id";
                using (SqlConnection databaseConnection = new SqlConnection(ConnectionString.GetConnectionString()))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            List<CouponModel> couponList = new List<CouponModel>();
                            while (reader.Read())
                            {
                                CouponModel coupon = ReadCoupons(reader);
                                couponList.Add(coupon);
                            }
                            return couponList;
                        }
                    }
                }
            }


            private static CouponModel ReadCoupons(IDataRecord reader)
            {
                int id = reader.GetInt32(0);
                string label = reader.IsDBNull(1) ? null : reader.GetString(1);
                string desription = reader.IsDBNull(2) ? null : reader.GetString(2);
                int discount = reader.GetInt32(3);
                DateTime dateOfIssue = reader.GetDateTime(4);
                DateTime dateOfExpiry = reader.GetDateTime(5);
                bool validity = reader.GetBoolean(6);



                CouponModel coupon = new CouponModel
                {
                    Id = id,
                    Label = label,
                    Description = desription,
                    Discount = discount,
                    DateOfIssue = dateOfIssue,
                    DateOfExpiry = dateOfExpiry,
                    Validity = validity
                };
                return coupon;
            }

            // GET: api/Coupon/5
            [Route("{id}")]
            public CouponModel GeCouponById(int id)
            {
                const string selectString = "select * from Coupons where id=@id";
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
                            return ReadCoupons(reader);
                        }
                    }
                }
            }

            // POST: api/Coupon
            [HttpPost]
            public ActionResult Post([FromBody] CouponModel input)
            {
                string commandStringInsert = "INSERT INTO Coupons (Label, Description, Discount, DateOfIssue, DateOfExpiry, Validity, CustomerId)" +
                                       "VALUES(@Label, @Description, @Discount, @DateOfIssue, @DateOfExpiry, @Validity, @CustomerId)";


                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
                    {
                        connection.Open();


                        using (SqlCommand cmd = new SqlCommand(commandStringInsert, connection))
                        {
                            cmd.Parameters.AddWithValue("@Label", input.Label);
                            cmd.Parameters.AddWithValue("@Description", input.Description);
                            cmd.Parameters.AddWithValue("@Discount", input.Discount);
                            cmd.Parameters.AddWithValue("@DateOfIssue", input.DateOfIssue);
                            cmd.Parameters.AddWithValue("@DateOfExpiry", input.DateOfExpiry);
                            cmd.Parameters.AddWithValue("@Validity", input.Validity);
                            cmd.Parameters.AddWithValue("@CustomerId", 1); //TODO Need to be changed to dynamic data
                            int effectedDBrows = cmd.ExecuteNonQuery();

                            if (effectedDBrows < 0)
                            {
                                return Conflict("Coupon couldn't be created.");
                            }
                            return Ok("Coupon created.");
                        }

                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }

            }

            // PUT: api/Coupon/5
            [HttpPut("{id}")]
            public ActionResult UpdateCoupon(int id, [FromBody] CouponModel input)
            {
                const string updateString =
                    "update Coupons set label=@label, description=@description, discount=@discount, dateofissue=@dateofissue, validity=@validity where id=@id;";
                using (SqlConnection databaseConnection = new SqlConnection(ConnectionString.GetConnectionString()))
                {
                    databaseConnection.Open();
                    using (SqlCommand updateCommand = new SqlCommand(updateString, databaseConnection))
                    {
                        updateCommand.Parameters.AddWithValue("@label", input.Label);
                        updateCommand.Parameters.AddWithValue("@description", input.Description);
                        updateCommand.Parameters.AddWithValue("@discount", input.Discount);
                        updateCommand.Parameters.AddWithValue("@dateofissue", input.DateOfIssue);
                        updateCommand.Parameters.AddWithValue("@dateofexpiry", input.DateOfExpiry);
                        updateCommand.Parameters.AddWithValue("@validity", input.Validity);
                        updateCommand.Parameters.AddWithValue("@id", id);
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected < 0)
                        {
                            return NotFound("Coupon couldn't be updated on id: " + id);
                        }
                        return Ok("Coupon updated on id: " + id);
                    }
                }
            }


            // DELETE: api/ApiWithActions/5
            [HttpDelete("{id}")]
            public ActionResult DeleteCoupon(int id)
            {
                const string deleteStatement = "delete from Coupons where id=@id";
                using (SqlConnection databaseConnection = new SqlConnection(ConnectionString.GetConnectionString()))
                {
                    databaseConnection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(deleteStatement, databaseConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", id);
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected < 0)
                        {
                            return NotFound("Coupon couldn't be found on id: " + id);
                        }
                        return Ok("Coupon deleted from id: " + id);
                    }
                }
            }
        }
    }
}
