using CSharp.Models;
using CSharp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Services
{
    public static class OrdersRepository
    {
        //TODO later replaced by repository services
        public static List<OrderModel> Orders = new List<OrderModel>
        {
            new OrderModel{
                CustomerId = new Guid("85fb3113-c1c2-4a09-9eb3-f87c41d86709"),
                OrderId = new Guid("b0cc525d-485b-41d5-809a-2b385e64d97e"),
                Status = OrderStatus.Active,
                Items = new List<string>{"Pizza"}
            },
            new OrderModel{
                CustomerId = new Guid("85fb3113-c1c2-4a09-9eb3-f87c41d86709"),
                OrderId = new Guid("50dc750b-65da-4906-b081-fe989e868bce"),
                Status = OrderStatus.Canceled,
                Items = new List<string>{"Pie"}
            },
            new OrderModel{
                CustomerId = new Guid("85fb3113-c1c2-4a09-9eb3-f87c41d86709"),
                OrderId = new Guid("7afb484e-4a30-4bdf-907c-cc5e7f141449"),
                Status = OrderStatus.Active,
                Items = new List<string>{"Panini"}
            },
            new OrderModel{
                CustomerId = new Guid("2f5a2039-ea51-41a4-8bd2-14ff67618c35"),
                OrderId = new Guid("2ad32e29-d978-4e96-b400-68cb48b6fb11"),
                Status = OrderStatus.Active,
                Items = new List<string>{"Pizza"}
            },
            new OrderModel{
                CustomerId = new Guid("2f5a2039-ea51-41a4-8bd2-14ff67618c35"),
                OrderId = new Guid("2a5cc20f-518f-469a-bea9-bbfeb55d4af9"),
                Status = OrderStatus.Active,
                Items = new List<string>{"Pizza"}
            },
            new OrderModel{
                CustomerId = new Guid("2f5a2039-ea51-41a4-8bd2-14ff67618c35"),
                OrderId = new Guid("43dc83d9-4aa2-4f32-8f13-ef1eb2fe442f"),
                Status = OrderStatus.Active,
                Items = new List<string>{"Pizza"}
            }
        };
    }

    public class OrdersService : IOrdersService
    {
        public List<OrderModel> GetOrdersByCustomerId(Guid customerId)
        {
            return OrdersRepository.Orders.Where(i => i.CustomerId == customerId).ToList();
        }

        public OrderModel CreateOrder(OrderModel order)
        {
            //TODO validations
            var addedOrderModel = new OrderModel
            {
                CustomerId = order.CustomerId,
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.Active,
                Items = order.Items
            };

            OrdersRepository.Orders.Add(addedOrderModel);
            return addedOrderModel;
        }

        public OrderModel UpdateOrder(OrderModel order)
        {
            //TODO validations
            var existingOrder = OrdersRepository.Orders.Where(i => i.OrderId == order.OrderId).FirstOrDefault();

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.Items = order.Items;

            return existingOrder;
        }

        public OrderModel CancelOrder(Guid orderId)
        {
            var existingOrder = OrdersRepository.Orders.Where(i => i.OrderId == orderId).FirstOrDefault();

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.Status = OrderStatus.Canceled;

            return existingOrder;
        }
    }
}
