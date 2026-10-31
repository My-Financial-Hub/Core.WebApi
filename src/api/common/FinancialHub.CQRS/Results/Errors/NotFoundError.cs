namespace FinancialHub.CQRS.Results.Errors
{
    public sealed class NotFoundError : ServiceError
    {
        public NotFoundError(string message) : base(message)
        {
        }
    }
}
