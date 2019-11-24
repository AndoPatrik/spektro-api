using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static ProductModel ReaderProductModel(SqlDataReader reader) 
        {
            ProductModel product = new ProductModel();
            product.Id = reader.GetInt32(0);
            product.ProductType = reader.GetString(1);
            product.ProdcutName = reader.GetString(2);
            product.ImgSrc = reader.GetString(3);
            product.Price = reader.GetDouble(4);
            product.Ingredients = reader.GetString(5);
            product.Allergens = reader.GetString(6);
            return product;
     
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        [HttpGet("{product}", Name = "Get")]
        public List<ProductModel> GetByCategory(string product)
        {
            string sqlString = "select * from Products where ProductType = @product";

            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString())) 
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sqlString, connection)) 
                {
                    cmd.Parameters.AddWithValue("@product", product);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            List<ProductModel> productsToReturn = new List<ProductModel> { };
                            while (reader.Read())
                            {
                                ProductModel item = ReaderProductModel(reader);
                                productsToReturn.Add(item);
                            }
                            return productsToReturn;
                        }
                        return null;
                    }
                }
            } 
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
