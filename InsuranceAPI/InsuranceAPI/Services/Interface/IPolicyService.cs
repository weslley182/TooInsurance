using ModelLib.Dtos;

namespace InsuranceAPI.Services.Interface
{
    public interface IPolicyService
    {
        Task SendPolicy(PolicyDto policy);
    }
}
