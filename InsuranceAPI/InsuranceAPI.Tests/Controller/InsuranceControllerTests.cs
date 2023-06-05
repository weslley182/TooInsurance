using InsuranceAPI.Services.Interface;
using InsuranceAPI.Tests.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Tests.Controller;

public class InsuranceControllerTests
{
    private InsuranceApiApplication _application;
    private string _url = "v1/Insurance";
    private HttpClient _httpClient;

    [SetUp]
    public void BaseSetUp()
    {
        var policyServiceMock = new Mock<IPolicyService>();
        _application = new InsuranceApiApplication();
        _httpClient = _application.CreateClient();
    }

    [Test]
    public async Task POST_Must_Create_Policy_Car_Message()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .Build();

        await InsuranceMockData.CreateMessages(_application, false);
        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [Test]
    public async Task POST_Must_Create_Policy_Home_Message()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Product_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithProduct(0)
            .WithValuesFilled()
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Plate_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithCarPlate(null)
            .WithValuesFilled()
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Model_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .WithCarModel(null)
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Frame_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithCarFrame(null)
            .WithValuesFilled()
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Parcel_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .WithValuesParcel(0)
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Total_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .WithValuesTotal(0)
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Values_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Adress_Must_return_bad_request()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .WithHomeAdress(null)
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Street_Must_return_bad_request()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .WithHomeStreet(null)
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_StreetNumber_Must_return_bad_request()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .WithHomeStreetNumber(0)
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_StreetNumber_Must_return_bad_request3()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .WithHomeStreetNumber(0)
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_StreetNumber_Must_return_bad_request2()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .WithHomeStreetNumber(0)
            .Build();

        //var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    private async Task<HttpClient>? BuildApp(string url, Mock mock = null)
    {
        await InsuranceMockData.CreateMessages(_application, false);
        if (mock == null)
        {
            mock = new Mock<IPolicyService>();
            return _application.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //services.Remove<IPolicyService>();
                    services.AddScoped(serv => mock.Object);
                });
            }).CreateClient();
        }

        return _application.CreateClient();
    }


}
