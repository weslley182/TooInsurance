using System.Net.Http.Json;
using System.Net;
using DataBaseModel.Model;

namespace InsuranceAPI.Tests.Controller;

public class MessageControllerTests
{
    private InsuranceApiApplication _application;

    [SetUp]
    public void BaseSetUp() 
    {
        _application = new InsuranceApiApplication();
    }

    [Test]
    public async Task GET_Return_all_messages()
    {
        await InsuranceMockData.CreateMessages(_application, true);
        var url = "v1/Message";

        var client = _application.CreateClient();

        var result = await client.GetAsync(url);
        var messages = await client.GetFromJsonAsync<List<MessageModel>>(url);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.IsNotNull(messages);
        Assert.AreEqual(2, messages.Count);
    }

    [Test]
    public async Task GET_Return_must_be_not_found()
    {
        await using var _application = new InsuranceApiApplication();
        
        var url = "v1/Message";

        var client = _application.CreateClient();

        var result = await client.GetAsync(url);        

        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        
    }
}
