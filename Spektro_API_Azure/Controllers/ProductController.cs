using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Spektro_API_Azure.Model;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : ControllerBase
    {
        private static ProductModel ReaderProductModel(SqlDataReader reader) 
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
            return new string[] { "Product lineup to use:", "Burger", "Drink", "Salat", "Pasta", "Dessert" };
        }

        // GET: api/Product/5
        [HttpGet("{product}", Name = "GetByCategory")]
        public List<ProductModel> GetByCategory(string product) //TODO: Try catch + logging implementation
        {
            string sqlString = "select * from Products where ProductType = @product";

            using (SqlConnection connection = new SqlConnection(SecretStrings.GetConnectionString())) 
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
    }
}
