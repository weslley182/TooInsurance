using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Dto
{
    public class Policy
    {
        [Required]
        [JsonPropertyName("Produto")]
        public int Product { get; set; }

        [JsonPropertyName("Item")]
        public General Item { get; set; }
        
        [Required]
        [JsonPropertyName("Valores")]
        public Amount Values { get; set; }
    }
}