namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class AccountsControllerTests
    {
        [Test]
        [TestCase(Description = "Delete Account returns NoContent", Category = "Delete")]
        public async Task Delete_ServiceSuccess_ReturnsNoContent()
        {
            var response = await this.controller.Delete(Guid.NewGuid());

            Assert.IsInstanceOf<NoContentResult>(response);
        }
    }
}
