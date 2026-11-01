using Microsoft.AspNetCore.Mvc;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase { }
}
