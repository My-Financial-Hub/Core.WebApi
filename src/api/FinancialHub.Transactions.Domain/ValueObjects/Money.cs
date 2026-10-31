using System;
using FinancialHub.Transactions.Domain.Exceptions;

namespace FinancialHub.Transactions.Domain.ValueObjects
{
    /// <summary>
    /// Represents a non-negative monetary amount with an optional currency code.
    /// </summary>
    public readonly struct Money : IEquatable<Money>
    {
        #region Properties
        /// <summary>
        /// Gets the monetary amount.
        /// </summary>
        public decimal Value { get; }
        /// <summary>
        /// Gets the optional currency code.
        /// </summary>
        public string Currency { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> struct.
        /// </summary>
        /// <param name="value">The monetary amount.</param>
        /// <param name="currency">The optional ISO-like currency code.</param>
        public Money(decimal value, string currency = null)
        {
            if (value < 0)
                throw new TransactionDomainException("Money value cannot be negative.");

            Value = value;
            Currency = string.IsNullOrWhiteSpace(currency) ? null : currency.Trim().ToUpperInvariant();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> struct.
        /// </summary>
        /// <param name="value">The monetary amount.</param>
        /// <param name="currency">The optional ISO-like currency code.</param>
        public Money(double value, string currency = null)
        {
            if (value < 0)
                throw new TransactionDomainException("Money value cannot be negative.");

            Value = (decimal)value;
            Currency = string.IsNullOrWhiteSpace(currency) ? null : currency.Trim().ToUpperInvariant();
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds two compatible money values.
        /// </summary>
        /// <param name="left">The left money value.</param>
        /// <param name="right">The right money value.</param>
        /// <returns>The sum of both money values.</returns>
        public static Money operator +(Money left, Money right)
        {
            EnsureCompatible(left, right);
            return new Money(left.Value + right.Value, left.Currency);
        }
        /// <summary>
        /// Subtracts one compatible money value from another.
        /// </summary>
        /// <param name="left">The left money value.</param>
        /// <param name="right">The right money value.</param>
        /// <returns>The difference between both money values.</returns>
        public static Money operator -(Money left, Money right)
        {
            EnsureCompatible(left, right);

            if (left.Value < right.Value)
            {
                throw new TransactionDomainException("Money amount cannot become negative.");
            }

            return new Money(left.Value - right.Value, left.Currency);
        }
        #endregion

        #region Equality Methods
        /// <inheritdoc />
        public bool Equals(Money other)
        {
            return Value == other.Value && Currency == other.Currency;
        }
        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Money other && Equals(other);
        }
        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Currency);
        }
        private static void EnsureCompatible(Money left, Money right)
        {
            if (left.Currency != right.Currency)
            {
                throw new TransactionDomainException("Money currencies must match.");
            }
        }
        #endregion
    }
}
