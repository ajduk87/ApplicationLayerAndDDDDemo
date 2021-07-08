using CommercialApplicationQueries.Dtoes;
using CommercialApplicationQueries.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommercialApplicationQueries.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderDtoRepository orderDtoRepository;

        public OrderController()
        {
            this.orderDtoRepository = new OrderDtoRepository();
        }

        [HttpGet]
        [Route("api/order/{id}")]
        public OrderDto GetOrder(long id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=commercialApplicationDb;User Id=postgres;Password=postgres;"))
            {
                try
                {
                    string customerName = this.orderDtoRepository.GetCustomerName(connection, id/*, transaction*/);
                    IEnumerable<OrderItemDto> orderItems = this.orderDtoRepository.GetOrderItems(connection, id/*, transaction*/);
                    OrderDto orderDto = new OrderDto
                    {
                        CustomerName = customerName,
                        OrderItems = orderItems
                    };

                    return orderDto;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return new OrderDto();
                }

            }
        }
    }
}
