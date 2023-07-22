using CSharp.Services;
using CSharp.Services.Interfaces;

namespace CSharp.Test.Services
{
    [TestClass]
    public class OrdersServiceTests
    {
        IOrdersService Sut;

        [TestInitialize]
        public void Startup()
        {
            Sut = new OrdersService();
        }
    }
}
