using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GggYoutubeDataApi.Managers
{
    public class AssetsManager
    {
        /// <summary>
        /// https://github.com/mediaelement/mediaelement-files/blob/master/big_buck_bunny.mp4
        /// </summary>
        /// <returns></returns>
        public static string GetSampleVideoPath()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(location);
            DirectoryInfo parentParent = directoryInfo.Parent?.Parent?.Parent;
            if (parentParent != null)
            {
                string parentParentFullName = parentParent.FullName;
                string fileName = "big_buck_bunny.mp4";
                string fullPath = parentParentFullName + @"\Assets\" + fileName;
                return fullPath;
            }
            throw new FileNotFoundException("Video file could not be found.");
        }
    }
}
