using FinancialHub.CQRS.Results.Errors;

namespace FinancialHub.CQRS.Results.Create
{
    public class CreateServiceResult : ServiceResult
    {
        public CreateServiceResult(ServiceError? error = null): base(error) { }


        public static implicit operator CreateServiceResult(ServiceError? error)
        {
            return new CreateServiceResult(error: error);
        }
    }
}
