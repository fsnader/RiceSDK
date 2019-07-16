using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Rice.SDK.Utils
{
    public static class PathExtensions
    {
        public static string GetFilePath(this IHostingEnvironment env, string filePath)
        {
            filePath = filePath.Replace("/", 
                Path.DirectorySeparatorChar.ToString());
            filePath = filePath.Replace("\\", 
                Path.DirectorySeparatorChar.ToString());
            
            return env.WebRootPath + Path.DirectorySeparatorChar.ToString() + filePath;
        }
    }
}