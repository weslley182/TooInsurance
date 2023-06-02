using InsuranceAPI.Dto;

namespace InsuranceAPI.Services.Interface
{
    public interface IPolicyService
    {
        Task SendPolicy(Policy policy);
    }
}
