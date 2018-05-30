using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggYoutubeDataApi.Managers
{
    [TestClass]
    public class LoggingManagerTests
    {
        /// <summary>
        /// https://stackoverflow.com/questions/3991933/get-path-for-my-exe
        /// </summary>
        [TestMethod]
        public void CreateFile()
        {
            LoggingManager.CreateFile("text.txt", "test");
        }
    }
}