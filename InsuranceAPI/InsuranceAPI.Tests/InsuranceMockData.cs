using DataBaseModel.Data;
using DataBaseModel.Model;
using InsuranceAPI.Constants;
using InsuranceAPI.Dto;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Tests;

public class InsuranceMockData
{
    public static async Task CreateMessages(InsuranceApiApplication application, bool create)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var dbContext = provider.GetRequiredService<AppDbContext>())
            {
                var address = new Address() { Street = "Street Fighter", Number = 6 };
                var legalPerson = new LegalPerson() { Name = "Capcom Inc", FedTaxIdNumber = 67076672030 };
                var physicalPerson = new PhysicalPerson() { Name = "Ken Masters", TaxIdNumber = 71679717000150 };
                var homeItem = new GeneralDto() { Address = address, Tenant = physicalPerson, Recipient = legalPerson };
                var homeParcel = new AmountDto() { Total = 100, Parcel = 3 };
                var homeInsurance = new PolicyDto() { Product = RabbitConstants.HomeInsuranceCod, Item = homeItem, Values = homeParcel };

                var carItem = new GeneralDto() { Model = "Fusca", Frame = 13253578, Plate = "CQB15153" };
                var carParcel = new AmountDto() { Total = 5000, Parcel = 3 };
                var carInsurance = new PolicyDto() { Product = RabbitConstants.CarInsuranceCod, Item = carItem, Values = carParcel };


                await dbContext.Database.EnsureCreatedAsync();

                if (create)
                {
                    await dbContext.Messages.AddAsync(new MessageModel
                        { Id = 1, Model = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }) }
                    );

                    await dbContext.Messages.AddAsync(new MessageModel
                        { Id = 2, Model = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }) }
                    );

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}