using System;

namespace FinancialHub.Transactions.Domain.Exceptions
{
    /// <summary>
    /// Represents an exception raised when a transaction domain invariant is violated.
    /// </summary>
    public class TransactionDomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDomainException"/> class.
        /// </summary>
        /// <param name="message">The domain validation error message.</param>
        public TransactionDomainException(string message) : base(message) { }
    }
}
