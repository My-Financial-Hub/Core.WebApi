using FinancialHub.Domain;
using FinancialHub.Transactions.Domain.Data;
using FinancialHub.Transactions.Domain.Data.Create;
using FinancialHub.Transactions.Domain.Data.Restore;
using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Domain.Enums;
using FinancialHub.Transactions.Domain.Events;
using FinancialHub.Transactions.Domain.Exceptions;
using FinancialHub.Transactions.Domain.ValueObjects;
using System;

namespace FinancialHub.Transactions.Domain
{
    /// <summary>
    /// Represents a financial operation recorded against an balance and category.
    /// </summary>
    public sealed class Transaction
    {
        #region Properties
        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        public Guid Id { get; }

        public string Description { get; private set; }

        /// <summary>
        /// Gets the balance that owns the transaction.
        /// </summary>
        public TransactionBalance Balance { get; private set; }
        /// <summary>
        /// Gets the category assigned to the transaction.
        /// </summary>
        public TransactionCategory Category { get; private set; }
        /// <summary>
        /// Gets the transaction amount.
        /// </summary>
        public Money Amount { get; private set; }

        /// <summary>
        /// Gets the transaction kind.
        /// </summary>
        public TransactionKind Kind { get; private set; }
        /// <summary>
        /// Gets the lifecycle status of the transaction.
        /// </summary>
        public TransactionStatus Status { get; private set; }

        public DateTimeOffset? TargetDate { get; private set; }
        public DateTimeOffset? FinishDate { get; private set; }

        /// <summary>
        /// Gets the timestamp when the transaction was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }
        /// <summary>
        /// Gets the timestamp when the transaction was most recently updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class with a new unique identifier.
        /// </summary>
        /// <param name="balance">The balance that owns the transaction.</param>
        /// <param name="category">The category assigned to the transaction.</param>
        /// <param name="amount">The transaction amount.</param>
        /// <param name="kind">The transaction kind.</param>
        /// <param name="status">The lifecycle status of the transaction.</param>
        /// <exception cref="TransactionDomainException"></exception>
        public Transaction(
            TransactionBalance balance, TransactionCategory category,
            Money amount, TransactionKind kind, TransactionStatus status
        )
        {
            this.Id = Guid.NewGuid();

            this.Balance = balance ?? throw new TransactionDomainException("Balance is required.");
            this.Category = category ?? throw new TransactionDomainException("Category is required.");

            this.Amount = amount;
            this.Kind = kind;
            this.Status = status;

            this.CreatedAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="id">The transaction identifier.</param>
        /// <param name="balance">The balance that owns the transaction.</param>
        /// <param name="category">The category assigned to the transaction.</param>
        /// <param name="amount">The transaction amount.</param>
        /// <param name="kind">The transaction kind.</param>
        /// <param name="status">The lifecycle status of the transaction.</param>
        /// <exception cref="TransactionDomainException"></exception>
        public Transaction(
            Guid id,
            TransactionBalance balance, TransactionCategory category,
            Money amount, TransactionKind kind, TransactionStatus status
        )
        {                                                                
            if (id == Guid.Empty)
                throw new TransactionDomainException("Transaction id is required.");

            this.Id = id;
            this.Balance = balance ?? throw new TransactionDomainException("Balance is required.");
            this.Category = category ?? throw new TransactionDomainException("Category is required.");

            this.Amount = amount;
            this.Kind = kind;
            this.Status = status;
        }
        #endregion

        #region Factories
        public static Transaction Restore(TransactionRestoreData restoreData)
        {
            if (!Enum.TryParse<TransactionStatus>(restoreData.Details.Status, out var status))
                throw new TransactionDomainException($"Invalid transaction status: {restoreData.Details.Status}");

            if (!Enum.TryParse<TransactionKind>(restoreData.Details.Kind, out var kind))
                throw new TransactionDomainException($"Invalid transaction kind: {restoreData.Details.Kind}");

            var amount = new Money(restoreData.Details.Amount);

            return new Transaction(
                restoreData.Id,
                restoreData.Relations.Balance, restoreData.Relations.Category,
                amount, kind, status
            )
            {
                Description = restoreData.Details.Description,

                CreatedAt = restoreData.Dates.CreatedAt,
                UpdatedAt = restoreData.Dates.UpdatedAt,

                TargetDate = restoreData.Dates.TargetDate,
                FinishDate = restoreData.Dates.FinishDate
            };
        }

        public static Transaction Create(TransactionCreateData createData)
        {
            var relations = createData.Relations;
            var details = createData.Details;
            var dates = createData.Dates;
            var transaction = new Transaction(
                relations.Balance, relations.Category,
                details.Amount, details.Kind, details.Status
            )
            {
                UpdatedAt = DateTimeOffset.UtcNow,
                Description = details.Description,
                TargetDate = dates.TargetDate,
                FinishDate = dates.FinishDate
            };

            return transaction;
        }
        #endregion

        public void Update(
            TransactionBalance? balance, TransactionCategory? category,
            decimal? amount, string? description,
            TransactionKind? kind, TransactionStatus? status,
            DateTimeOffset? targetDate, DateTimeOffset? finishDate
        )
        {
            if(balance != null && balance.Id != this.Balance.Id)
                this.Balance = balance;
            if(category != null && category.Id != this.Category.Id)
                this.Category = category;

            if(amount != null && amount.Value != this.Amount.Value)
                this.Amount = new Money(amount.Value);
            if(description != this.Description)
                this.Description = description;

            if(kind != null && kind.Value != this.Kind)
                this.Kind = kind.Value;
            if(status != null && status.Value != this.Status)
                this.Status = status.Value;

            if(targetDate != this.TargetDate)
                this.TargetDate = targetDate;
            if(finishDate != this.FinishDate)
                this.FinishDate = finishDate;

            this.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
