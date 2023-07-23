using CSharp.Controllers;
using CSharp.Models;
using CSharp.Services;
using CSharp.Services.Interfaces;
using NSubstitute;

namespace CSharp.Test.Services
{
    public class OrdersServiceTestBase
    {
        protected OrdersService Sut;

        protected readonly Guid ValidCustomerId = new("85fb3113-c1c2-4a09-9eb3-f87c41d86709");
        protected OrderModel CreateModel = new OrderModel
        {
            CustomerId = new Guid("85fb3113-c1c2-4a09-9eb3-f87c41d86709"),
            Status = OrderStatus.Active,
            Items = new List<string> { "Peanuts" }
        };
        protected OrderModel UpdateModel = new OrderModel
        {
            CustomerId = new Guid("85fb3113-c1c2-4a09-9eb3-f87c41d86709"),
            OrderId = new Guid("7afb484e-4a30-4bdf-907c-cc5e7f141449"),
            Status = OrderStatus.Active,
            Items = new List<string> { "Pasta" }
        };

        [TestInitialize]
        public void Startup()
        {
            Sut = new OrdersService();
        }
    }

    [TestClass]
    public class GetOrdersByCustomerIdTests : OrdersServiceTestBase
    {
        [TestMethod]
        public void ReturnsOrdersWithValidCustomerId()
        {
            var result = Sut.GetOrdersByCustomerId(ValidCustomerId);

            Assert.IsInstanceOfType(result, typeof(List<OrderModel>));
        }

        [TestMethod]
        public void ReturnsOnlyOrdersWithCustomerId()
        {
            var result = Sut.GetOrdersByCustomerId(ValidCustomerId);

            var customerIds = result.Select(i => i.CustomerId).Distinct();

            Assert.AreEqual(1, customerIds.Count());
            Assert.AreEqual(customerIds.FirstOrDefault(), ValidCustomerId);
        }
    }

    [TestClass]
    public class CreateOrderTests : OrdersServiceTestBase
    {
        [TestMethod]
        public void ReturnsCreatedOrder()
        {
            var result = Sut.CreateOrder(CreateModel);

            Assert.AreEqual(CreateModel.CustomerId, result.CustomerId);
            Assert.AreEqual(CreateModel.Items, result.Items);
            Assert.IsNotNull(result.OrderId);
        }

        [TestMethod]
        public void ReturnAddsOrderToRepository()
        {
            var result = Sut.CreateOrder(CreateModel);

            var searchResults = Sut.GetOrdersByCustomerId(CreateModel.CustomerId)
                .FirstOrDefault(i => i.Items.Contains(CreateModel.Items.First()));

            Assert.IsNotNull(searchResults);
            Assert.AreEqual(CreateModel.CustomerId, result.CustomerId);
            Assert.AreEqual(CreateModel.Items, result.Items);
        }
    }

    [TestClass]
    public class UpdateOrderTests : OrdersServiceTestBase
    {
        [TestMethod]
        public void ReturnsUpdatedOrder()
        {
            var result = Sut.UpdateOrder(UpdateModel);

            Assert.AreEqual(UpdateModel.CustomerId, result.CustomerId);
            Assert.AreEqual(UpdateModel.Items, result.Items);
        }

        [TestMethod]
        public void ReturnsNullIfNoExistingOrder()
        {
            var result = Sut.UpdateOrder(new OrderModel { OrderId = new Guid() });

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnUpdatedOrderInRepository()
        {
            var result = Sut.CreateOrder(UpdateModel);

            var searchResults = Sut.GetOrdersByCustomerId(UpdateModel.CustomerId)
                .FirstOrDefault(i => i.Items.Contains(UpdateModel.Items.First()));

            Assert.IsNotNull(searchResults);
            Assert.AreEqual(UpdateModel.Items, result.Items);
        }
    }

    [TestClass]
    public class CancelOrderTests : OrdersServiceTestBase
    {
        [TestMethod]
        public void ReturnsCanceledOrder()
        {
            var result = Sut.CancelOrder(UpdateModel.OrderId);

            Assert.AreEqual(UpdateModel.OrderId, result.OrderId);
        }

        [TestMethod]
        public void ReturnsOrderWithCanceledStatus()
        {
            var result = Sut.CancelOrder(UpdateModel.OrderId);

            Assert.AreEqual(OrderStatus.Canceled, result.Status);
        }

        [TestMethod]
        public void ReturnsNullIfNoExistingOrder()
        {
            var result = Sut.CancelOrder(new Guid());

            Assert.IsNull(result);
        }

        [TestMethod]
        public void OrderIsCanceledInRepository()
        {
            var result = Sut.CancelOrder(UpdateModel.OrderId);

            var searchResults = Sut.GetOrdersByCustomerId(UpdateModel.CustomerId)
                .FirstOrDefault(i => i.OrderId == UpdateModel.OrderId && i.Status == OrderStatus.Canceled);

            Assert.IsNotNull(searchResults);
        }
    }
}
