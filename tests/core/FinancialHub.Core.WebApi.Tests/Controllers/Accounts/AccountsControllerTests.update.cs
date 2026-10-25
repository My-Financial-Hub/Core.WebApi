using FinancialHub.Core.Domain.DTOS.Accounts;

namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class AccountsControllerTests
    {
        [Test]
        [TestCase(Description = "Update account existing Returns Ok", Category = "Update")]
        public async Task Update_Valid_ReturnsOk()
        {
            var body = this.updateAccountDtoBuilder.Generate();
            var resultDto = this.accountDtoBuilder
                .FromUpdateDto(body)
                .Generate();
            var guid = Guid.NewGuid();
            var mockResult = new ServiceResult<AccountDto>(resultDto);

            this.mockService
                .Setup(x => x.UpdateAsync(guid, body))
                .ReturnsAsync(mockResult)
                .Verifiable();

            var response = await this.controller.Update(guid, body);

            Assert.IsInstanceOf<OkResult>(response);

            this.mockService.Verify(x => x.UpdateAsync(guid, body), Times.Once);
        }

        [Test]
        [TestCase(Description = "Update Account invalid account returns BadRequest", Category = "Update")]
        public async Task Update_Invalid_ReturnsBadRequest()
        {
            var errorMessage = $"Invalid thing : {Guid.NewGuid()}";
            var body = this.updateAccountDtoBuilder.Generate();
            var resultDto = this.accountDtoBuilder
                .FromUpdateDto(body)
                .Generate();
            var guid = Guid.NewGuid();

            var mockResult = new ServiceResult<AccountDto>(resultDto, new InvalidDataError(errorMessage));

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
            Assert.AreEqual(mockResult.Error.Message, listResponse?.Message);

            this.mockService.Verify(x => x.UpdateAsync(guid, body), Times.Once);
        }
    }
}
