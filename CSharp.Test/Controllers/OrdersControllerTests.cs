using CSharp.Controllers;

namespace CSharp.Test.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        OrdersController Sut = new OrdersController();

        [TestInitialize]
        public void Startup()
        {
        }

        [TestMethod]
        public void GetReturnsSuccess()
        {
            var result = Sut.Get();

            Assert.AreEqual(default, result);
        }
    }
}
