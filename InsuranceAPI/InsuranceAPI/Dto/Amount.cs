using System.Text.Json.Serialization;

namespace InsuranceAPI.Dto
{
    public class Amount
    {
        [JsonPropertyName("PrecoTotal")]
        public double Total { get; set; }
        
        [JsonPropertyName("Parcelas")]
        public int Parcel { get; set; }
    }
}
