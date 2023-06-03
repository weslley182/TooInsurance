using InsuranceAPI.Constants;
using InsuranceAPI.Dto;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace InsuranceAPI.Tests.Controller;

public class InsuranceControllerTests
{    
    private InsuranceApiApplication _application;

    [SetUp]
    public void BaseSetUp()
    {
        _application = new InsuranceApiApplication();
    }

    [Test]
    public async Task POST_Must_Create_Message()
    {
        var counter = 0;
        var carItem = new GeneralDto() { Model = "Fusca", Frame = 13253578, Plate = "CQB15153" };
        var carParcel = new AmountDto() { Total = 5000, Parcel = 3 };
        var carInsurance = new PolicyDto() { Product = RabbitConstants.CarInsuranceCod, Item = carItem, Values = carParcel };

        //_policyService.When(x => x.SendPolicy(carInsurance)).Do(x => counter++);

        await InsuranceMockData.CreateMessages(_application, false);
        var url = "v1/Insurance";
        var client = _application.CreateClient();


        var teste = System.Text.Json.JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await client.PostAsync(url, new StringContent(teste, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Product_Must_return_bad_request()
    {     
        var carItem = new GeneralDto() { Model = "Fusca", Frame = 13253578, Plate = "BSC1315" };
        var carParcel = new AmountDto() { Total = 5000, Parcel = 3 };
        var carInsurance = new PolicyDto() { Item = carItem, Values = carParcel };

        //_policyService.When(x => x.SendPolicy(carInsurance)).Do(x => counter++);

        await InsuranceMockData.CreateMessages(_application, false);
        var url = "v1/Insurance";
        var client = _application.CreateClient();


        var teste = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await client.PostAsync(url, new StringContent(teste, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Plate_Must_return_bad_request()
    {     
        var carItem = new GeneralDto() { Model = "Fusca", Frame = 13253578, Plate = "" };
        var carParcel = new AmountDto() { Total = 5000, Parcel = 3 };
        var carInsurance = new PolicyDto() { Product = RabbitConstants.CarInsuranceCod, Item = carItem, Values = carParcel };

        //_policyService.When(x => x.SendPolicy(carInsurance)).Do(x => counter++);

        await InsuranceMockData.CreateMessages(_application, false);
        var url = "v1/Insurance";
        var client = _application.CreateClient();


        var teste = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await client.PostAsync(url, new StringContent(teste, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

}
