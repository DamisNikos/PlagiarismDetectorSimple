using PlagiarismDetectorSimple.Core;
using PlagiarismDetectorSimple.Demos;
using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple
{
    class Program
    {

        static void Main(string[] args)
        {
            string original = @"Files\Paper\experiment2\original.pdf";
            string suspicious = @"Files\Paper\experiment2\plagiarized.pdf";

            //Algorithm.Run(suspicious, original);

            //original = @"Files\Database\orig_taskd.pdf";
            //suspicious = @"Files\Inputs\taskd\g0pA_taskd_heavy.pdf";
            //Algorithm.Run(suspicious, original);

            //suspicious = @"Files\Inputs\taskd\g0pC_taskd_cut.pdf";
            //Algorithm.Run(suspicious, original);

            original = @"Files\Paper\original1.pdf";
            suspicious = @"Files\Paper\plagiarized.pdf";
            Algorithm.Run(suspicious, original);




            Debugger.Break();

        }
    }
}
