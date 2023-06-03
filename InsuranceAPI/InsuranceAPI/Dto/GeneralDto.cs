using System.Text.Json.Serialization;

namespace InsuranceAPI.Dto
{
    public class GeneralDto
    {
        [JsonPropertyName("Endereco")]
        public Address? Address { get; set; }

        [JsonPropertyName("Inquilino")]        
        public PhysicalPerson? Tenant { get; set; }

        [JsonPropertyName("Beneficiario")]        
        public LegalPerson? Recipient { get; set; }

        [JsonPropertyName("Placa")]        
        public string? Plate { get; set; }

        [JsonPropertyName("Chassis")]        
        public int? Frame { get; set; }

        [JsonPropertyName("Modelo")]        
        public string? Model { get; set; }
        
    }

    public class Address
    {
        [JsonPropertyName("Rua")]
        public string? Street { get; set; }
        
        [JsonPropertyName("Numero")]
        public int? Number { get; set; }
    }
    public class Person
    {
        [JsonPropertyName("Nome")]
        public string? Name { get; set; }
    }

    public class LegalPerson: Person
    {
        [JsonPropertyName("CNPJ")]
        public double? FedTaxIdNumber { get; set; }
    }

    public class PhysicalPerson: Person
    {
        [JsonPropertyName("CPF")]
        public double? TaxIdNumber { get; set; }
    }    
}