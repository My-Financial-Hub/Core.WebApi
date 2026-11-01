using Microsoft.AspNetCore.Mvc;

namespace FinancialHub.Transactions.WebApi.Controllers.Categories
{
    [ApiController]
    [Route("api/categories")]
    [Produces("application/json")]
    public abstract class BaseCategoriesController : ControllerBase { }
}
