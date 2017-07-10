using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlagiarismDetectorSimple.Core
{
    class TempDirectory
    {
        public static void CreateTempDirectory(string directory, Document original, Document suspicious)
        {
            Directory.CreateDirectory(directory);
            ConverterToByteArray.ByteArrayToFile(directory + "\\" + Path.GetFileName(suspicious.DocName), suspicious.DocContent);
            ConverterToByteArray.ByteArrayToFile(directory + "\\" + Path.GetFileName(original.DocName), original.DocContent);
        }

        public static void DeleteTempDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFileSystemInfos()) { file.Delete(); }
            Directory.Delete(path);
        }
    }
}
