using CSharp.Controllers;
using CSharp.Services.Interfaces;
using NSubstitute;

namespace CSharp.Test.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        private IOrdersService OrdersServiceMock;
        private OrdersController Sut;

        [TestInitialize]
        public void Startup()
        {
            OrdersServiceMock = Substitute.For<IOrdersService>();
            Sut = new OrdersController(OrdersServiceMock);
        }

        [TestMethod]
        public void GetReturnsSuccess()
        {
            var result = Sut.Get();

            Assert.AreEqual(default, result);
        }
    }
}
