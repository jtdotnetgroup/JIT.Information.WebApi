using Abp.AutoMapper;
using JIT.InfomationSystem.Authentication.External;

namespace JIT.InfomationSystem.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
