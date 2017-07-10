using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Models
{
    class Boundary
    {
        public int lower { get; set; }
        public int upper { get; set; }
    }

    class Boundaries
    {
        public List<Boundary> listOfBoundaries { get; set; }
    }

    class BoundaryWithIndex
    {
        public int lower { get; set; }
        public int upper { get; set; }
        public int lowerIndex { get; set; }
        public int upperIndex { get; set; }
    }

    class BoundariesWithIndex
    {
        public List<BoundaryWithIndex> listOfBoundaries { get; set; }
    }

}
