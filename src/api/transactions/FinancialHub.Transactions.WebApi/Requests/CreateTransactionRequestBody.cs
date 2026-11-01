using FinancialHub.Transactions.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialHub.Transactions.WebApi.Requests
{
    public sealed class CreateTransactionRequestBody
    {
        [JsonRequired] 
        [JsonPropertyName("balance_id")]
        public Guid BalanceId { get; set; }

        [JsonRequired]
        [JsonPropertyName("category_id")]
        public Guid CategoryId { get; set; }

        [JsonRequired]
        [JsonPropertyName("amount")]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [StringLength(500)]
        [JsonPropertyName("description")]
        [Description("A brief description of the transaction.")]
        public string? Description { get; set; }

        [JsonRequired]
        [JsonPropertyName("status")]
        [EnumDataType(typeof(TransactionStatus))]
        [Description("The lifecycle status of the transaction.")]
        public TransactionStatus Status { get; set; }

        [JsonRequired]
        [JsonPropertyName("kind")]
        [EnumDataType(typeof(TransactionKind))]
        [Description("The kind of the transaction, indicating whether it is an Earn or an expense.")]
        public TransactionKind Kind { get; set; }

        [JsonPropertyName("target_date")]
        public DateTime? TargetDate { get; set; }

        [JsonPropertyName("finish_date")]
        public DateTime? FinishDate { get; set; }
    }
}
