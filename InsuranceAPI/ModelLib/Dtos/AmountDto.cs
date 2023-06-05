using LinkDotNet.ValidationExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelLib.Dtos
{
    public class AmountDto
    {
        [Required]
        [Min(1)]
        [JsonPropertyName("PrecoTotal")]
        public decimal Total { get; set; }

        [Required]
        [Min(1)]
        [JsonPropertyName("Parcelas")]
        public int Parcel { get; set; }
    }
}
