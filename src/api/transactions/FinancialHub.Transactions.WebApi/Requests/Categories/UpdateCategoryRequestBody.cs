using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialHub.Transactions.WebApi.Requests.Categories
{
    public sealed class UpdateCategoryRequestBody
    {
        [JsonRequired]
        [StringLength(100)]
        [JsonPropertyName("name")]
        [Description("The name of the category.")]
        public string Name { get; set; }
    }
}
