using System.Diagnostics;
using GggYoutubeDataApi.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggYoutubeDataApi
{
    [TestClass]
    public class CredentialsManagerTests
    {
        [TestMethod]
        public void GetApiKey()
        {
            string apiKey = CredentialsManager.GetApiKey();
            Debugger.Break();
        }
    }
}
