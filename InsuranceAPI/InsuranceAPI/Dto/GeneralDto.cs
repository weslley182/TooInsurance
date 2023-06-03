using InsuranceAPI.AnnotationsConfig;
using InsuranceAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Dto
{
    public class GeneralDto
    {
        [JsonPropertyName("Endereco")]        
        [RequiredIf(nameof(ProductModel), RabbitConstants.HomeInsuranceCod, ErrorMessage = "Este campo é obrigatório quando o Produto é 111.")]
        public Address? Address { get; set; }

        [JsonPropertyName("Inquilino")]
        [RequiredIf(nameof(ProductModel), RabbitConstants.HomeInsuranceCod, ErrorMessage = "Este campo é obrigatório quando o Produto é 111.")]
        public PhysicalPerson? Tenant { get; set; }

        [JsonPropertyName("Beneficiario")]
        [RequiredIf(nameof(ProductModel), RabbitConstants.HomeInsuranceCod, ErrorMessage = "Este campo é obrigatório quando o Produto é 111.")]
        public LegalPerson? Recipient { get; set; }

        [JsonPropertyName("Placa")]
        [RequiredIf(nameof(ProductModel), RabbitConstants.CarInsuranceCod, ErrorMessage = "Este campo é obrigatório quando o Produto é 222.")]
        public string? Plate { get; set; }

        [JsonPropertyName("Chassis")]
        [RequiredIf(nameof(ProductModel), RabbitConstants.CarInsuranceCod, ErrorMessage = "Este campo é obrigatório quando o Produto é 222.")]
        public int? Frame { get; set; }

        [JsonPropertyName("Modelo")]
        [RequiredIf(nameof(ProductModel), RabbitConstants.CarInsuranceCod, ErrorMessage = "Este campo é obrigatório quando o Produto é 222.")]
        public string? Model { get; set; }

        [JsonIgnore]
        public virtual int ProductModel { get; set; }
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