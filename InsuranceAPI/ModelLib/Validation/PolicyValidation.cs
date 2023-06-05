using ModelLib.Constants;
using ModelLib.Dtos;

namespace ModelLib.Validation;

public class PolicyValidation
{
    public bool ValidateRequired_Item(PolicyDto value)
    {
        switch (value.Product)
        {
            case RabbitConstants.CarInsuranceCod:
                return ValidateCarInsurance(value);
            case RabbitConstants.HomeInsuranceCod:
                return ValidateHomeInsurance(value);
            default:
                return true;
        }
    }

    private bool ValidateCarInsurance(PolicyDto value)
    {
        return String.IsNullOrEmpty(value.Item.Plate) || String.IsNullOrEmpty(value.Item.Model) || String.IsNullOrEmpty(value.Item.Frame);
    }
    private bool ValidateHomeInsurance(PolicyDto value)
    {
        return AddressValidation(value.Item.Address) || TenantValidation(value.Item.Tenant) || RecipientValidation(value.Item.Recipient);
    }

    private bool AddressValidation(Address? value)
    {
        return (value == null) || String.IsNullOrEmpty(value.Number.ToString()) || String.IsNullOrEmpty(value.Street) || !(value.Number > 0);
    }

    private bool TenantValidation(PhysicalPerson? value)
    {
        return (value == null) || String.IsNullOrEmpty(value.TaxIdNumber.ToString()) || String.IsNullOrEmpty(value.Name);
    }

    private bool RecipientValidation(LegalPerson? value)
    {
        return (value == null) || String.IsNullOrEmpty(value.FedTaxIdNumber.ToString()) || String.IsNullOrEmpty(value.Name);
    }

}
