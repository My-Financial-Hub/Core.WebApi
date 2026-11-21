namespace FinancialHub.CQRS.Results.Errors
{
    public abstract class ServiceError
    {
        public string Message { get; }

        protected ServiceError(string message)
        {
            this.Message = message;
        }
    }
}
