namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class TransactionsControllerTests
    {
        [Test]
        [TestCase(Description = "Delete Transaction Returns NoContent", Category = "Delete")]
        public async Task Delete_ServiceSuccess_ReturnsNoContent()
        {
            var response = await this.controller.Delete(Guid.NewGuid());

            var result = response as ObjectResult;

            Assert.IsNull(result?.Value);
        }
    }
}
