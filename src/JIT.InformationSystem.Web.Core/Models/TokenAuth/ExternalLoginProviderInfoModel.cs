using Abp.AutoMapper;
using JIT.InformationSystem.Authentication.External;

namespace JIT.InformationSystem.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
