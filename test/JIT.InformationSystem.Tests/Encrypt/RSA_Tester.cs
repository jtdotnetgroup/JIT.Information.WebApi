using System.Security.Cryptography;
using System.Text;
using Xunit;
using Encrypt;

namespace JIT.InformationSystem.Tests.Encrypt
{
    public class RSA_Tester: InformationSystemTestBase
    {
        [Fact]
        public void Test()
        {
            string privateKey = @"";
            RSAHelper helper=new RSAHelper(RSAType.RSA2,Encoding.UTF8,privateKey);

            

        }
    }
}