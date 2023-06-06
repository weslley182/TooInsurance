using DataBaseModel.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;


namespace InsuranceAPI.Tests.Controller;

public class CarParcelsControllerTests
{
    private InsuranceApiApplication _application;
    private string _url = "v1/CarParcels";

    [SetUp]
    public void BaseSetUp()
    {
        _application = new InsuranceApiApplication();
    }


    [Test]
    public async Task GET_must_receive_service_unavailable()
    {
        var carRepoMock = new Mock<ICarInsuranceRepository>();
        carRepoMock.Setup(p => p.GetAllAsync()).ThrowsAsync(new Exception("Mocked exception"));

        var client = BuildApp(carRepoMock);
        var result = await client.GetAsync(_url);

        Assert.AreEqual(HttpStatusCode.ServiceUnavailable, result.StatusCode);
    }

    private HttpClient BuildApp(Mock<ICarInsuranceRepository> mock)
    {
        return _application.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var serv = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ICarInsuranceRepository));
                services.Remove(serv);

                services.AddScoped(serv => mock.Object);
            });
        }).CreateClient();
    }
}
