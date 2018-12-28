using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPhotoToolsLibrary
{
    public class DeleteRawFromJpg
    {
        public static async Task Start(string directoryPath, string jpgExtension, string rawExtension)
        {
            DirectoryInfo workingDir = new DirectoryInfo(directoryPath);
            if (!workingDir.Exists)
            {
                throw new Exception("The directory does not exists: " + directoryPath);
            }

            DirectoryInfo rawDirectory =
                workingDir.GetDirectories().Where(d => d.Name.ToLower() == "raw").FirstOrDefault();

            if (rawDirectory == null)
            {
                throw new Exception("The is no directory named raw in the working directory: " + directoryPath);
            }

            var jpgFiles = workingDir.GetFiles("*." + jpgExtension.Replace(".", ""));
            var rawFiles = rawDirectory.GetFiles("*." + rawExtension.Replace(".", ""));

            foreach (FileInfo raw in rawFiles)
            {
                string fileName = raw.Name.Replace(rawExtension, "");
                if (!jpgFiles.Any(jpg => jpg.Name == fileName + jpgExtension))
                {
                    raw.Delete();
                }
            }
        }
    }
}
