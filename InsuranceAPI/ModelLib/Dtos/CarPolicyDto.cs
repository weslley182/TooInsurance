
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelLib.Dtos
{
    public class CarPolicyDto
    {
        [Required]
        [JsonPropertyName("Produto")]
        public int Product { get; set; }

        [JsonPropertyName("Item")]
        public GeneralDto Item { get; set; }

        [Required]
        [JsonPropertyName("Valores")]
        public AmountDto Values { get; set; }

        public int? MessageId { get; set; }


    }
}