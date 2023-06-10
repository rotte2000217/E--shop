using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.QueryParameters;

namespace WebshopServer.Interfaces
{
    public interface IOrderService
    {
        List<OrderResponseDto> GetAllOrders(OrderQueryParameters queryParameters);
        OrderResponseDto GetOrderById(long id);
        OrderResponseDto CreateOrder(OrderRequestDto requestDto, long userId);
        void CancelOrder(long id, long userId);
    }
}
