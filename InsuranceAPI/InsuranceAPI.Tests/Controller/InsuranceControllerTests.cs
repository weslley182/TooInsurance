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
        var carInsurance = GetPolicy();

        var url = "v1/Insurance";
        var client = BuildApp(url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Product_Must_return_bad_request()
    {
        var carInsurance = GetPolicy();
        //carInsurance.Product = null;

        var url = "v1/Insurance";
        var client = BuildApp(url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Plate_Must_return_bad_request()
    {        
        var carInsurance = GetPolicy();
        carInsurance.Item.Plate = "";

        var url = "v1/Insurance";
        var client = BuildApp(url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    private PolicyDto GetPolicy() 
    {
        var carItem = new GeneralDto() { Model = "Fusca", Frame = 13253578, Plate = "BSC13456" };
        var carParcel = new AmountDto() { Total = 5000, Parcel = 3 };
        return  new PolicyDto() { Product = RabbitConstants.CarInsuranceCod, Item = carItem, Values = carParcel };
    }

    private async Task<HttpClient>? BuildApp(string url)
    {
        await InsuranceMockData.CreateMessages(_application, false);    
        return _application.CreateClient();
    }


}
