using System;
using System.Collections.Generic;

namespace CSharp.Models
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<string> Items { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Unknown,
        Active,
        Canceled
    }
}
