using DataBaseModel.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;


namespace InsuranceAPI.Tests.Controller;

public class HomeParcelsControllerTests
{
    private InsuranceApiApplication _application;
    private string _url = "v1/HomeParcels";

    [SetUp]
    public void BaseSetUp()
    {
        _application = new InsuranceApiApplication();
    }


    [Test]
    public async Task GET_must_receive_service_unavailable()
    {
        var homeRepoMock = new Mock<IHomeInsuranceRepository>();
        homeRepoMock.Setup(p => p.GetAllAsync()).ThrowsAsync(new Exception("Mocked exception"));

        var client = BuildApp(homeRepoMock);
        var result = await client.GetAsync(_url);

        Assert.AreEqual(HttpStatusCode.ServiceUnavailable, result.StatusCode);
    }

    private HttpClient BuildApp(Mock<IHomeInsuranceRepository> mock)
    {
        return _application.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var serv = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IHomeInsuranceRepository));
                services.Remove(serv);

                services.AddScoped(serv => mock.Object);
            });
        }).CreateClient();
    }
}
