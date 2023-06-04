using LinkDotNet.ValidationExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Dto
{
    public class AmountDto
    {
        [Required]
        [Min(1)]
        [JsonPropertyName("PrecoTotal")]
        public double Total { get; set; }

        [Required]
        [Min(1)]
        [JsonPropertyName("Parcelas")]
        public int Parcel { get; set; }
    }
}
