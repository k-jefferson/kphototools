using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPhotoToolsLibrary
{
    public class IsolateRawFiles
    {
        public static async Task Start(string directoryPath, string rawExtension)
        {
            DirectoryInfo workingDir = new DirectoryInfo(directoryPath);
            if (!workingDir.Exists)
            {
                throw new Exception("The directory does not exists: " + directoryPath);
            }

            DirectoryInfo rawDirectory = new DirectoryInfo(Path.Combine(workingDir.FullName, "raw"));
            if (rawDirectory.Exists)
            {
                throw new Exception("the raw directory already exists, aborting");
            }

            rawDirectory.Create();

            foreach (FileInfo fileInfo in workingDir.GetFiles("*." + rawExtension.Replace(".", "")))
            {
                fileInfo.MoveTo(Path.Combine(rawDirectory.FullName, fileInfo.Name));
            }
        }
    }
}
