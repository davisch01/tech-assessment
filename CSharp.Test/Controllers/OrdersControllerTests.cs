using CSharp.Controllers;
using CSharp.Models;
using CSharp.Services.Interfaces;
using NSubstitute;

namespace CSharp.Test.Controllers
{
    public class OrdersControllerTestBase
    {
        protected IOrdersService OrdersServiceMock;
        protected OrdersController Sut;

        protected readonly Guid ValidCustomerId = new("85fb3113-c1c2-4a09-9eb3-f87c41d86709");
        protected List<OrderModel> OrderModels = new();

        [TestInitialize]
        public void Startup()
        {
            OrderModels = new List<OrderModel>
            {
                new OrderModel { OrderId = new Guid(), CustomerId = ValidCustomerId, Items = new List<string>{"Pizza"} },
                new OrderModel { OrderId = new Guid(), CustomerId = ValidCustomerId, Items = new List<string>{"Pie"} },
                new OrderModel { OrderId = new Guid(), CustomerId = ValidCustomerId, Items = new List<string>{"Panini"} }
            };

            OrdersServiceMock = Substitute.For<IOrdersService>();
            OrdersServiceMock.GetOrdersByCustomerId(default).ReturnsForAnyArgs(OrderModels);
            OrdersServiceMock.CreateOrder(default).ReturnsForAnyArgs(new OrderModel());
            OrdersServiceMock.UpdateOrder(default).ReturnsForAnyArgs(new OrderModel());
            OrdersServiceMock.CancelOrder(default).ReturnsForAnyArgs(new OrderModel());

            Sut = new OrdersController(OrdersServiceMock);
        }
    }

    [TestClass]
    public class OrdersControllerGetByCustomerIdTests : OrdersControllerTestBase
    {
        [TestMethod]
        public void CallsOrdersServiceGetByCustomerId()
        {
            _ = Sut.GetByCustomerId(ValidCustomerId);

            OrdersServiceMock.Received(1).GetOrdersByCustomerId(ValidCustomerId);
        }

        [TestMethod]
        public void ReturnsListOfOrdersWithValidCustomerId()
        {
            var result = Sut.GetByCustomerId(ValidCustomerId);

            Assert.IsInstanceOfType(result, typeof(List<OrderModel>));
        }

        [TestMethod]
        public void ReturnsOnlyOrdersWithCustomerId()
        {
            var result = Sut.GetByCustomerId(ValidCustomerId);

            var customerIds = result.Select(i => i.CustomerId).Distinct();

            Assert.AreEqual(1, customerIds.Count());
            Assert.AreEqual(customerIds.FirstOrDefault(), ValidCustomerId);
        }
    }

    [TestClass]
    public class OrdersControllerPostTests : OrdersControllerTestBase
    {
        [TestMethod]
        public void CallsOrdersServiceCreateOrder()
        {
            var testOrder = OrderModels.Last();

            _ = Sut.Post(testOrder);

            OrdersServiceMock.Received(1).CreateOrder(testOrder);
        }

        [TestMethod]
        public void ReturnsCreatedOrder()
        {
            var testOrder = OrderModels.Last();

            var result = Sut.Post(testOrder);

            Assert.IsNotNull(result);
        }
    }

    [TestClass]
    public class OrdersControllerPutTests: OrdersControllerTestBase
    {
        [TestMethod]
        public void CallsOrdersServiceUpdateOrder()
        {
            var testOrder = OrderModels.Last();

            _ = Sut.Update(testOrder);

            OrdersServiceMock.Received(1).UpdateOrder(testOrder);
        }

        [TestMethod]
        public void ReturnsUpdatedOrder()
        {
            var testOrder = OrderModels.Last();

            var result = Sut.Update(testOrder);

            Assert.IsNotNull(result);
        }
    }

    [TestClass]
    public class OrdersControllerDeleteTests : OrdersControllerTestBase
    {
        [TestMethod]
        public void CallsOrdersServiceCancelOrder()
        {
            var testOrderId = OrderModels.Last().OrderId;

            _ = Sut.Delete(testOrderId);

            OrdersServiceMock.Received(1).CancelOrder(testOrderId);
        }

        [TestMethod]
        public void ReturnsCanceledOrder()
        {
            var testOrderId = OrderModels.Last().OrderId;

            var result = Sut.Delete(testOrderId);

            Assert.IsNotNull(result);
        }
    }
}
