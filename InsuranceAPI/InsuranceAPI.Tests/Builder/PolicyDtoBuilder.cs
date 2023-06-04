using Bogus;
using Bogus.Extensions.Brazil;
using InsuranceAPI.Constants;
using InsuranceAPI.Dto;


namespace InsuranceAPI.Tests.Builder;

public class PolicyDtoBuilder
{
    private PolicyDto _policyDto = new();
    private AmountDto _amountDto = new();
    private GeneralDto _generalDto = new();
    private Faker _faker = new();
    
    public PolicyDtoBuilder WithProduct(int cod)
    {
        _policyDto.Product = cod;
        return this;
    }

    public PolicyDtoBuilder WithValuesFilled()
    {
        _amountDto.Total = _faker.Random.Double(1000, 100000);
        _amountDto.Parcel = _faker.Random.Int(1, 12);
        return this;
    }

    public PolicyDtoBuilder WithValuesTotal(double total)
    {
        _amountDto.Total = total;
        return this;
    }

    public PolicyDtoBuilder WithValuesParcel(int parcel)
    {        
        _amountDto.Parcel = parcel;
        return this;
    }

    public PolicyDtoBuilder WithCarFullFilled()
    {
        _policyDto.Product = RabbitConstants.CarInsuranceCod;
        _generalDto.Plate = _faker.Random.Word();
        _generalDto.Model = _faker.Random.Word();
        _generalDto.Frame = _faker.Random.Number(10);
        return this;
    }

    public PolicyDtoBuilder WithCarPlate(string plate)
    {
        _generalDto.Plate = plate;
        return this;
    }

    public PolicyDtoBuilder WithCarModel(string model)
    {
        _generalDto.Model = model;
        return this;
    }

    public PolicyDtoBuilder WithCarFrame(int frame)
    {        
        _generalDto.Frame = frame;
        return this;
    }

    public PolicyDtoBuilder WithHomeFullFilled()
    {
        _policyDto.Product = RabbitConstants.HomeInsuranceCod;
        var address = new Address();
        var tenant = new PhysicalPerson();
        var recipient = new LegalPerson();

        address.Street = _faker.Address.StreetName();
        address.Number = _faker.Address.Random.Int(50, 9999);

        tenant.Name = _faker.Person.FullName;
        tenant.TaxIdNumber = int.Parse(_faker.Person.Cpf());

        recipient.Name = _faker.Company.CompanyName();
        recipient.FedTaxIdNumber = int.Parse(_faker.Company.Cnpj());
        
        _generalDto.Address = address;
        _generalDto.Tenant = tenant;
        _generalDto.Recipient = recipient;
        return this;
    }

    public PolicyDto Build()
    {
        _policyDto.Values = _amountDto;
        _policyDto.Item = _generalDto;

        return _policyDto;
    }
}
