using BookStoreMvc.Models.Domain;
using BookStoreMvc.Models.DTO;
using BookStoreMvc.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;

namespace BookStoreMvc.Controllers
{
    public class AdminController : Controller
    {
        //private readonly DatabaseContext ctx;
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            //this.ctx = ctx;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var ordersDict = new Dictionary<int,OrderDetails>();

            string connString = _configuration.GetConnectionString("conn");

            using (SqlConnection con = new SqlConnection(connString))
            {
                await con.OpenAsync();
                string query = @"
                    SELECT o.*, c.Id AS CartItemId, c.BookId, c.Quantity, c.IsOrdered, c.OrderId, b.Title, b.Price
                    FROM OrderDetails o
                    LEFT JOIN CartItems c ON o.Id = c.OrderId
                    LEFT JOIN Books b on c.BookId = b.Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while(await reader.ReadAsync())
                    {
                        int orderId = reader.GetInt32(reader.GetOrdinal("Id"));
                        if (!ordersDict.TryGetValue(orderId, out var order)){
                            order = new OrderDetails
                            {
                                Id = orderId,
                                UserId = reader.GetString(reader.GetOrdinal("UserId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                PhoneNumber = reader.GetInt32(reader.GetOrdinal("PhoneNumber")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                CityId = reader.GetInt32(reader.GetOrdinal("CityId")),
                                CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                                TotalOrderAmount = reader.GetDouble(reader.GetOrdinal("TotalOrderAmount")),
                                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod")),
                                OrderStatus = reader.GetString(reader.GetOrdinal("OrderStatus")),
                                CartItems = new List<Models.DTO.CartItem>()
                            };
                            ordersDict[orderId] = order;
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("CartItemId")))
                        {
                            var book = new Book
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BookId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetDouble(reader.GetOrdinal("Price"))
                            };

                            var cartItem = new CartItem
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CartItemId")),
                                BookId = book.Id,
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                IsOrdered = reader.GetBoolean(reader.GetOrdinal("IsOrdered")),
                                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                                Book = book
                            };
                            
                            order.CartItems.Add(cartItem);

                        }
                    }
                }
            }
            //return Ok(ordersDict.Values.ToList());
            return View(ordersDict.Values.ToList());
        }

        /*
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var allOrders = await ctx.OrderDetails.Include(x=>x.CartItems).FromSqlRaw("Select * from OrderDetails").ToListAsync();
            return Ok(allOrders);
        }
        */
    }
}
