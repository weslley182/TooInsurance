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
    private Mock<IPolicyService> _policyServiceMock;

    [SetUp]
    public void BaseSetUp()
    {
        _policyServiceMock = new Mock<IPolicyService>();
        _application = new InsuranceApiApplication();
        _httpClient = BuildApp(_policyServiceMock);
    }

    [TearDown]
    public void BaseTearDown()
    {

    }

    [Test]
    public async Task POST_Must_Create_Policy_Car_Message()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .Build();

        await InsuranceMockData.CreateMessages(_application, false);

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [Test]
    public async Task POST_Must_Create_Policy_Home_Message()
    {
        var homeInsurance = new PolicyDtoBuilder()
            .WithHomeFullFilled()
            .WithValuesFilled()
            .Build();

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

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

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

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

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

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

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

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

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

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

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

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

        var json = JsonSerializer.Serialize(homeInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await _httpClient.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    private HttpClient BuildApp(Mock<IPolicyService> mock)
    {
        return _application.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(serv => mock.Object);
            });
        }).CreateClient();
    }
}
