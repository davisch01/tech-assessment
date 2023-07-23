namespace CSharp.Test.Controllers
{
    [TestClass]
    public class TestControllerTests
    {
        [TestMethod]
        public void GetReturnsSuccess()
        {
            var sut = new CSharp.Controllers.Test();

            var result = sut.Get();

            const string ExpectedMessage = "Success!";
            Assert.AreEqual(ExpectedMessage, result);
        }
    }
}