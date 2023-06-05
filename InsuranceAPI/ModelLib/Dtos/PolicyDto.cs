using ModelLib.Constants;
using LinkDotNet.ValidationExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ModelLib.Dtos;
using ModelLib.Validation;

namespace ModelLib.Dtos
{
    public class PolicyDto
    {
        [Required]
        [JsonPropertyName("Produto")]
        public int Product { get; set; }

        [JsonPropertyName("Item")]
        [RequiredDynamic(nameof(ValidateRequired_ItemTest), ErrorMessagesConstants.ErrorMessageItem)]
        public GeneralDto Item { get; set; }

        [Required]
        [JsonPropertyName("Valores")]
        public AmountDto Values { get; set; }

        public int? MessageId { get; set; }

        public bool ValidateRequired_ItemTest(PolicyDto value)
        {
            var validation = new PolicyValidation();
            return validation.ValidateRequired_Item(value);
        }
    }
}