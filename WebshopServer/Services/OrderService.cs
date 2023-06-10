using AutoMapper;
using EntityFramework.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Enums;
using WebshopServer.Exceptions;
using WebshopServer.Infrastructure;
using WebshopServer.Interfaces;
using WebshopServer.Models;
using WebshopServer.QueryParameters;

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

        public List<OrderResponseDto> GetAllOrders(OrderQueryParameters queryParameters)
        {
            List<Order> orders = new List<Order>();

            if (queryParameters.BuyerId > 0)
            {
                orders = _dbContext.Orders.Where(x => x.BuyerId == queryParameters.BuyerId).ToList();
            }
            else
            {
                orders = _dbContext.Orders.ToList();
            }

            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public OrderResponseDto GetOrderById(long id)
        {
            OrderResponseDto order = _mapper.Map<OrderResponseDto>(_dbContext.Orders.Find(id));

            if (order == null)
            {
                throw new ResourceNotFoundException("Order with specified id doesn't exist!");
            }

            return order;
        }

        public OrderResponseDto CreateOrder(OrderRequestDto requestDto, long userId)
        {
            Order order = _mapper.Map<Order>(requestDto);
            order.BuyerId = userId;

            Article article = _dbContext.Articles.Find(order.ArticleId);

            if (article == null)
            {
                throw new ResourceNotFoundException("Article with specified id doesn't exist!");
            }

            if (article.Quantity < order.Quantity)
            {
                throw new InvalidFieldsException("There are not enough articles in stock!");
            }

            article.Quantity -= order.Quantity;
            order.OrderStatus = OrderStatus.Pending;
            order.Price = article.Price * order.Quantity;
            order.CreatedAt = DateTime.UtcNow;
            order.DeliveryTime = new Random().Next(1, 25);

            _dbContext.Orders.Add(order);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidFieldsException("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<OrderResponseDto>(order);
        }

        public DeleteResponseDto CancelOrder(long id, long userId)
        {
            Order order = _dbContext.Orders.Find(id);

            if (order == null)
            {
                throw new ResourceNotFoundException("Order with specified id doesn't exist!");
            }

            if (order.BuyerId != userId)
            {
                throw new ForbiddenActionException("Buyers can only cancel their own orders!");
            }

            if ((DateTime.UtcNow - order.CreatedAt).Hours > 1)
            {
                throw new InvalidFieldsException("Orders can only be cancelled in the first hour!");
            }

            Article article = _dbContext.Articles.Find(order.ArticleId);

            if (article == null)
            {
                throw new ResourceNotFoundException("Article with specified id doesn't exist!");
            }

            article.Quantity += order.Quantity;

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

            return _mapper.Map<DeleteResponseDto>(order);
        }
    }
}
