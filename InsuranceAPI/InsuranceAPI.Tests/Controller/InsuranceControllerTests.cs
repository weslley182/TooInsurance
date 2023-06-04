using InsuranceAPI.Constants;
using InsuranceAPI.Dto;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using InsuranceAPI.Services.Interface;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using InsuranceAPI.Tests.Builder;

namespace InsuranceAPI.Tests.Controller;

public class InsuranceControllerTests
{    
    private InsuranceApiApplication _application;
    private string _url = "v1/Insurance";

    [SetUp]
    public void BaseSetUp()
    {
        var policyServiceMock = new Mock<IPolicyService>();
        _application = new InsuranceApiApplication();        
    }

    [Test]
    public async Task POST_Must_Create_Message()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .Build();

        
        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

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
            .WithCarPlate("")
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

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Test]
    public async Task POST_No_Values_Must_return_bad_request()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()            
            .Build();

        var client = BuildApp(_url).Result;

        var json = JsonSerializer.Serialize(carInsurance, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        var result = await client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    private async Task<HttpClient>? BuildApp(string url, Mock mock = null)
    {
        if(mock == null)
        {
            mock = new Mock<IPolicyService>();
        }

        var client = _application.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {                
                services.AddScoped(serv => mock.Object);
            });
        })
        .CreateClient();

        await InsuranceMockData.CreateMessages(_application, false);
        return client;//_application.CreateClient();
    }


}
