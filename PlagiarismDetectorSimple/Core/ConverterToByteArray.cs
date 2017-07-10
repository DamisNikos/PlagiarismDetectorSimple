using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlagiarismDetectorSimple.Core
{
    class ConverterToByteArray
    {
        //Converting from any file to byte array for trasportation and storage use maybe
        public static Document FileToByteArray(string fileName)
        {
            byte[] fileContent = null;

            using (FileStream fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {

                    long byteLength = new FileInfo(fileName).Length;
                    fileContent = binaryReader.ReadBytes((Int32)byteLength);

                }
            }
            Document document = new Document();
            document.DocName = fileName;
            document.DocContent = fileContent;
            return document;
        }

        //recovering a file (given correct fileName path) from a byte array
        public static void ByteArrayToFile(string fileName, byte[] fileContent)
        {
            using (Stream file = File.OpenWrite(fileName))
            {
                file.Write(fileContent, 0, fileContent.Length);
            }
        }

    }
}
