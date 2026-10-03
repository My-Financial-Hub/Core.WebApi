using FinancialHub.Core.Domain.DTOS.Transactions;

namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class TransactionsControllerTests
    {
        [Test]
        [TestCase(Description = "Create valid transaction Return Ok", Category = "Create")]
        public async Task Create_Valid_ReturnsOk()
        {
            var body = this.createTransactionDtoBuilder.Generate();
            var serviceResult = this
                .transactionDtoBuilder
                .FromCreateDto(body)
                .Generate();
            var mockResult = new ServiceResult<TransactionDto>(serviceResult);

            this.mockService
                .Setup(x => x.CreateAsync(body))
                .ReturnsAsync(mockResult)
                .Verifiable();

            var response = await this.controller.Create(body);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        [TestCase(Description = "Create invalid Transaction Return BadRequest", Category = "Create")]
        public async Task Create_Invalid_ReturnsBadRequest()
        {
            var errorMessage = $"Invalid thing : {Guid.NewGuid()}";
            var body = this.createTransactionDtoBuilder.Generate();
            var serviceResult = this
                .transactionDtoBuilder
                .FromCreateDto(body)
                .Generate();
            var mockResult = new ServiceResult<TransactionDto>(serviceResult, new InvalidDataError(errorMessage));

            this.mockService
                .Setup(x => x.CreateAsync(body))
                .ReturnsAsync(mockResult)
                .Verifiable();

            var response = await this.controller.Create(body);

            var result = response as ObjectResult;

            Assert.AreEqual(400, result?.StatusCode);
            Assert.IsInstanceOf<ValidationErrorResponse>(result?.Value);

            var listResponse = result?.Value as ValidationErrorResponse;
            Assert.AreEqual(mockResult.Error!.Code, listResponse?.Code);
            Assert.AreEqual(mockResult.Error!.Message, listResponse?.Message);

            this.mockService.Verify(x => x.CreateAsync(body), Times.Once);
        }
    }
}
