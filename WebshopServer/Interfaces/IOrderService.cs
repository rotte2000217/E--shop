using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;

namespace WebshopServer.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(long id);
        OrderDto CreateOrder(OrderDto orderDto, long userId);
        void CancelOrder(long id, long userId);
    }
}
