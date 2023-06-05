using Bogus;
using Bogus.Extensions.Brazil;
using InsuranceAPI.Constants;
using InsuranceAPI.Dto;
using System.Security.Cryptography.Pkcs;
using System.Text.RegularExpressions;

namespace InsuranceAPI.Tests.Builder;

public class PolicyDtoBuilder
{
    private Faker _faker = new();
    private PolicyDto _policyDto = new();
    private AmountDto _amountDto = new();
    private GeneralDto _generalDto = new();
    
    public PolicyDtoBuilder WithProduct(int cod)
    {
        _policyDto.Product = cod;
        return this;
    }

    public PolicyDtoBuilder WithValuesFilled()
    {
        _amountDto.Total = _faker.Random.Decimal(1000, 100000);
        _amountDto.Parcel = _faker.Random.Int(1, 12);
        return this;
    }

    public PolicyDtoBuilder WithValuesTotal(Decimal total)
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
        var onlyNumbers = new Regex(@"[^\d]");
        
        _policyDto.Product = RabbitConstants.HomeInsuranceCod;
        var address = new Address();
        var tenant = new PhysicalPerson();
        var recipient = new LegalPerson();

        address.Street = _faker.Address.StreetName();
        address.Number = _faker.Address.Random.Int(50, 9999);

        tenant.Name = _faker.Person.FullName;
        var cpf = _faker.Person.Cpf();
        tenant.TaxIdNumber = Double.Parse(onlyNumbers.Replace(cpf, ""));

        recipient.Name = _faker.Company.CompanyName();
        var cnpj = _faker.Company.Cnpj();
        recipient.FedTaxIdNumber = Double.Parse(onlyNumbers.Replace(cnpj, ""));
        
        _generalDto.Address = address;
        _generalDto.Tenant = tenant;
        _generalDto.Recipient = recipient;
        return this;
    }

    public PolicyDtoBuilder WithHomeAdress(Address? address)
    {
        _generalDto.Address = address;
        return this;
    }

    public PolicyDtoBuilder WithHomeStreet(string street)
    {
        _generalDto.Address.Street = street;
        return this;
    }

    public PolicyDtoBuilder WithHomeStreetNumber(int number)
    {
        _generalDto.Address.Number = number;
        return this;
    }

    public PolicyDtoBuilder WithHomeRecipient(LegalPerson? recipient)
    {
        _generalDto.Recipient = recipient;
        return this;
    }

    public PolicyDtoBuilder WithHomeRecName(string name)
    {
        _generalDto.Recipient.Name = name;
        return this;
    }

    public PolicyDtoBuilder WithHomeRecId(double number)
    {
        _generalDto.Recipient.FedTaxIdNumber= number;
        return this;
    }


    public PolicyDto Build()
    {
        _policyDto.Values = _amountDto;
        _policyDto.Item = _generalDto;

        return _policyDto;
    }
}
