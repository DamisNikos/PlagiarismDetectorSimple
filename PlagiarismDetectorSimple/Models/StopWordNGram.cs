using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Models
{
    class StopWordNGram
    {
        public List<stopWord> _stopWords { get; set; }
        public int _firstIndex { get; set; }
        public int _lastIndex { get; set; }
    }
}
