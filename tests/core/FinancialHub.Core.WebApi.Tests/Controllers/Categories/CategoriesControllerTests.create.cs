using FinancialHub.Core.Domain.DTOS.Categories;

namespace FinancialHub.Core.WebApi.Tests.Controllers
{
    public partial class CategoriesControllerTests
    {
        [Test]
        [TestCase(Description = "Create valid category returns Ok", Category = "Create")]
        public async Task Create_Valid_ReturnsOk()
        {
            var body = this.createCategoryDtoBuilder.Generate();
            var resultMock = this.categoryDtoBuilder
                .FromCreateDto(body)
                .Generate();
            var mockResult = new ServiceResult<CategoryDto>(resultMock);

            this.mockService
                .Setup(x => x.CreateAsync(body))
                .ReturnsAsync(mockResult)
                .Verifiable();

            var response = await this.controller.Create(body);

            Assert.IsInstanceOf<CreatedResult>(response);

            this.mockService.Verify(x => x.CreateAsync(body), Times.Once);
        }

        [Test]
        [TestCase(Description = "Create invalid Category returns BadRequest", Category = "Create")]
        public async Task Create_Invalid_ReturnsBadRequest()
        {
            var errorMessage = $"Invalid thing : {Guid.NewGuid()}";
            var body = this.createCategoryDtoBuilder.Generate();
            var resultMock = this.categoryDtoBuilder
                .FromCreateDto(body)
                .Generate();
            var mockResult = new ServiceResult<CategoryDto>(resultMock, new InvalidDataError(errorMessage));

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
