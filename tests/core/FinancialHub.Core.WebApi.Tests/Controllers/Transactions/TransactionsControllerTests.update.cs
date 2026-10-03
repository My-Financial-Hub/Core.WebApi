using FinancialHub.Core.Domain.DTOS.Transactions;

namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class TransactionsControllerTests
    {
        [Test]
        [TestCase(Description = "Update valid Transaction returns Ok", Category = "Update")]
        public async Task Update_Valid_ReturnsOk()
        {
            var body = this.updateTransactionDtoBuilder.Generate();
            var guid = Guid.NewGuid();
            var serviceResult = this
                .transactionDtoBuilder
                    .WithId(guid)
                    .FromUpdateDto(body)
                    .Generate(); 
            var mockResult = new ServiceResult<TransactionDto>(serviceResult);

            this.mockService
                .Setup(x => x.UpdateAsync(guid, body))
                .ReturnsAsync(mockResult)
                .Verifiable();

            var response = await this.controller.Update(guid, body);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        [TestCase(Description = "Update Transaction invalid returns BadRequest", Category = "Update")]
        public async Task Update_Invalid_ReturnsBadRequest()
        {
            var errorMessage = $"Invalid thing : {Guid.NewGuid()}";
            var body = this.updateTransactionDtoBuilder.Generate();
            var guid = Guid.NewGuid();
            var serviceResult = this
                .transactionDtoBuilder
                    .WithId(guid)
                    .FromUpdateDto(body)
                    .Generate();

            var mockResult = new ServiceResult<TransactionDto>(serviceResult, new InvalidDataError(errorMessage));

            this.mockService
                .Setup(x => x.UpdateAsync(guid,body))
                .ReturnsAsync(mockResult)
                .Verifiable();

            var response = await this.controller.Update(guid,body);

            var result = response as ObjectResult;

            Assert.AreEqual(400, result?.StatusCode);
            Assert.IsInstanceOf<ValidationErrorResponse>(result?.Value);

            var listResponse = result?.Value as ValidationErrorResponse;
            Assert.AreEqual(mockResult.Error!.Code, listResponse?.Code);
            Assert.AreEqual(mockResult.Error!.Message, listResponse?.Message);

            this.mockService.Verify(x => x.UpdateAsync(guid, body), Times.Once);
        }
    }
}
