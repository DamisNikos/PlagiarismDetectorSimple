using System;
using System.Collections.Generic;
using System.Text;

namespace PlagiarismDetectorSimple.Models
{
    class Document
    {
        public int DocID { get; set; }
        public string DocName { get; set; }
        public byte[] DocContent { get; set; }
    }
}
