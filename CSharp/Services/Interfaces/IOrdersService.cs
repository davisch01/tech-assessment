using CSharp.Models;
using System;
using System.Collections.Generic;

namespace CSharp.Services.Interfaces
{
    public interface IOrdersService
    {
        public List<OrderModel> GetOrdersByCustomerId(Guid customerId);
        public OrderModel UpdateOrder(OrderModel order);
        public OrderModel CancelOrder(Guid orderId);
    }
}
