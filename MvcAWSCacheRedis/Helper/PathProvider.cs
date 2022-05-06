using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAWSCacheRedis.Helper
{
    public enum Folders { Images, Documents, Temp}

    public class PathProvider
    {
        private IWebHostEnvironment webHost;
        public PathProvider(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public string MapPath(string fileName,Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Documents)
            {
                carpeta = "documents";
            }
            else if (folder==Folders.Images)
            {
                carpeta = "images";
            }
            string path = Path.Combine(this.webHost.WebRootPath, carpeta, fileName);
            return path;

        }
    }
}
