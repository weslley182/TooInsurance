using System.Net.Http.Json;
using System.Net;
using DataBaseModel.Model;

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
}
