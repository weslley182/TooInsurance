using DataBaseModel.Model;
using InsuranceAPI.Services.Interface;
using InsuranceAPI.Tests.Builder;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DataBaseModel.Repository.Interface;

namespace InsuranceAPI.Tests.Controller;

public class MessageControllerTests
{
    private InsuranceApiApplication _application;
    private HttpClient _client;
    private string _url = "v1/Message";

    [SetUp]
    public void BaseSetUp()
    {
        _application = new InsuranceApiApplication();
        _client = _application.CreateClient();
    }

    [Test]
    public async Task GET_Return_all_messages()
    {
        await InsuranceMockData.CreateMessages(_application, true);

        var result = await _client.GetAsync(_url);
        var messages = await _client.GetFromJsonAsync<List<MessageModel>>(_url);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.IsNotNull(messages);
        Assert.AreEqual(2, messages.Count);
    }
    
    [Test]
    public async Task GET_must_receive_exception_connection()
    {
        var carInsurance = new PolicyDtoBuilder()
            .WithCarFullFilled()
            .WithValuesFilled()
            .Build();

        var messageRepoMock = new Mock<IMessageRepository>();
        messageRepoMock.Setup(p => p.GetAllAsync()).ThrowsAsync(new Exception("Mocked exception"));

        var client = BuildApp(messageRepoMock);        
        var result = await client.GetAsync(_url);

        Assert.AreEqual(result.StatusCode, HttpStatusCode.InternalServerError);
    }

    private HttpClient BuildApp(Mock<IMessageRepository> mock)
    {
        return _application.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var policyServ = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IPolicyService));
                services.Remove(policyServ);

                services.AddScoped(serv => mock.Object);
            });
        }).CreateClient();
    }
}
