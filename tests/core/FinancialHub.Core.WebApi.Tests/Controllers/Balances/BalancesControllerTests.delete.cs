namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class BalancesControllerTests
    {
        [Test]
        public async Task DeleteMyBalances_ServiceSuccess_ReturnsNoContent()
        {
            var response = await this.controller.Delete(Guid.NewGuid());
            Assert.IsInstanceOf<NoContentResult>(response);
        }
    }
}
