using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Enums;
using WebshopServer.Infrastructure;
using WebshopServer.Interfaces;
using WebshopServer.Models;

namespace WebshopServer.Services
{
    public class OrderService : IOrderService
    {
        private readonly WebshopDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(WebshopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<OrderDto> GetAllOrders()
        {
            return _mapper.Map<List<OrderDto>>(_dbContext.Orders.ToList());
        }

        public OrderDto GetOrderById(long id)
        {
            return _mapper.Map<OrderDto>(_dbContext.Orders.Find(id));
        }

        public OrderDto CreateOrder(OrderDto orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            Article article = _dbContext.Articles.Find(order.ArticleId);

            if (article.Quantity < order.Quantity)
            {
                return null;
            }

            article.Quantity -= order.Quantity;
            order.OrderStatus = OrderStatus.Pending;
            order.Price = article.Price * order.Quantity;
            order.CreatedAt = DateTime.UtcNow;
            order.DeliveryTime = new Random().Next(1, 25);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return _mapper.Map<OrderDto>(order);
        }

        public void CancelOrder(long id)
        {
            Order order = _dbContext.Orders.Find(id);
            Article article = _dbContext.Articles.Find(order.ArticleId);

            if (DateTime.UtcNow - order.CreatedAt >= new TimeSpan(1, 0, 0))
            {
                return;
            }

            article.Quantity += order.Quantity;

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }
    }
}
