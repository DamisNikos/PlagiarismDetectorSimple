using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PlagiarismDetectorSimple.Models;
using System.Threading.Tasks;
using PlagiarismDetectorSimple.Core;

namespace PlagiarismDetectorSimple.Demos
{
    class AgainstEmulatedDatabase
    {
        public static void Run() {
            string database = @"Files\Database\";
            string inputs = @"Files\Inputs\taskd";
            string[] inputFiles = Directory.GetFiles(inputs, "*.pdf", SearchOption.AllDirectories);

            foreach (string suspiciousFile in inputFiles)
            {
                //Emulate a file being inserted into the database and compared to all the files currently in it
                string[] originalFiles = Directory.GetFiles(database, "*.pdf", SearchOption.AllDirectories);

                Console.WriteLine("***NEW INPUT***");   //***************************************************************
                //*******************************************************************************************************
                int printCounter = 0;
                Console.WriteLine("File {0} will be examined against file(s):", Path.GetFileName(suspiciousFile));
                foreach (string printFile in originalFiles)
                {

                    Console.WriteLine("{0} : {1}", ++printCounter, Path.GetFileName(printFile));
                }
                //*******************************************************************************************************

                for (int i = 0; i < originalFiles.Length; i++)
                {
                    Document original = ConverterToByteArray.FileToByteArray(originalFiles[i]);
                    Document suspicious = ConverterToByteArray.FileToByteArray(suspiciousFile);


                    //now we have 2 byte arrays
                    string pathToTemp = "Files\\Temp";
                    //Create a temporary directory and restore the files
                    TempDirectory.CreateTempDirectory(pathToTemp, original, suspicious);

                    //Run algorithm
                    string tempPathToSuspicious = pathToTemp + "\\" + Path.GetFileName(suspiciousFile);
                    string tempPathToOriginal = pathToTemp + "\\" + Path.GetFileName(originalFiles[i]);
                    Algorithm.Run(tempPathToSuspicious, tempPathToOriginal);

                    Console.WriteLine("Examined Against File:{0}", Path.GetFileName(originalFiles[i]));//**************************

                    //Console.ReadLine();

                    //Deleting the temporary array
                    ConverterToByteArray.ByteArrayToFile(database + Path.GetFileName(suspicious.DocName), suspicious.DocContent);
                    TempDirectory.DeleteTempDirectory(pathToTemp);
                }

                Console.WriteLine();                                                               //**************************
                Console.WriteLine();                                                               //**************************
            }

        }

    }
}
