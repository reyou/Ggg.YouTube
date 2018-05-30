using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace GggYoutubeDataApi.Managers
{
    public class LoggingManager
    {
        public static void WriteLine(string result)
        {
            Debug.Write(result);
        }

        public static void CreateFile(string fileName, object content)
        {
            // D:\Git\Ggg.Github\Ggg.YouTube\apps\app-docs-dotnet\GggYoutubeDataApi\bin\Debug\netcoreapp2.0\
            string location = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(location);
            if (directoryInfo.Parent?.Parent != null)
            {
                DirectoryInfo parentParent = directoryInfo.Parent.Parent.Parent;
                if (parentParent != null)
                {
                    string parentParentFullName = parentParent.FullName;
                    string fullPath = parentParentFullName + @"\Assets\" + fileName;
                    string serializeObject = JsonConvert.SerializeObject(content, Formatting.Indented);
                    if (content is string)
                    {
                        serializeObject = content.ToString();
                    }
                    File.WriteAllText(fullPath, serializeObject);
                }
                else
                {
                    throw new Exception("Directory could not be found.");
                }
            }
            else
            {
                throw new Exception("Directory could not be found.");
            }
        }
    }
}