using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Core
{
    class BoundaryDetection
    {
        public static List<List<Boundary>> DetectInitialSet(List<int[]> M, int thetaG)
        {
            

            int[] M1 = new int[M.Count];
            int[] M2 = new int[M.Count];
            for (int i = 0; i < M.Count; i++)
            {
                M1[i] = M[i][0];
                M2[i] = M[i][1];
            }


            BoundariesWithIndex initialBoundaries = new BoundariesWithIndex() { listOfBoundaries = new List<BoundaryWithIndex>() };
            BoundaryWithIndex boundary = new BoundaryWithIndex() { lower = M1[0], lowerIndex = 0 };
            for (int i = 1; i < M1.Length; i++)
            {
                if (Math.Abs(M1[i] - M1[i - 1]) > thetaG)
                {
                    boundary.upper = M1[i - 1];
                    boundary.upperIndex = i - 1;
                    initialBoundaries.listOfBoundaries.Add(boundary);

                    boundary = new BoundaryWithIndex() { lower = M1[i], lowerIndex = i };
                }
            }
            boundary.upper = M1[M1.Length - 1];
            boundary.upperIndex = M1.Length - 1;
            initialBoundaries.listOfBoundaries.Add(boundary);

            BoundariesWithIndex finalBoundaries = new BoundariesWithIndex() { listOfBoundaries = new List<BoundaryWithIndex>() };
            foreach (BoundaryWithIndex initialBoundary in initialBoundaries.listOfBoundaries)
            {
                boundary = new BoundaryWithIndex() { lower = M2[initialBoundary.lowerIndex], lowerIndex = initialBoundary.lowerIndex };
                for (int i = initialBoundary.lowerIndex + 1; i < initialBoundary.upperIndex; i++)
                {
                    if (Math.Abs(M2[i] - M2[i - 1]) > thetaG)
                    {
                        boundary.upper = M2[i - 1];
                        boundary.upperIndex = i - 1;
                        finalBoundaries.listOfBoundaries.Add(boundary);

                        boundary = new BoundaryWithIndex() { lower = M2[i], lowerIndex = i };
                    }
                }
                boundary.upper = M2[initialBoundary.upperIndex];
                boundary.upperIndex = initialBoundary.upperIndex;
                finalBoundaries.listOfBoundaries.Add(boundary);
            }

            List<List<Boundary>> boundaries = new List<List<Boundary>>();
            foreach (BoundaryWithIndex bound in finalBoundaries.listOfBoundaries)
            {

                Boundary susp = new Boundary() { lower = M1[bound.lowerIndex], upper = M1[bound.upperIndex] };
                Boundary original = new Boundary() { lower = M2[bound.lowerIndex], upper = M2[bound.upperIndex] };
                List<Boundary> mList = new List<Boundary> { susp, original };
                boundaries.Add(mList);

            }
            return boundaries;
        }


        //TEST


        //public static List<List<Boundary>> DetectInitialSetTest()
        //{
        //    List<int[]> M = new List<int[]>
        //    {
        //        new int[2] {5,30},
        //        new int[2] {6,31},
        //        new int[2] {7,32},
        //        new int[2] {8,33},
        //        new int[2] {9,34},
        //        new int[2] {10,35},
        //        new int[2] {11,20},
        //        new int[2] {12,21},
        //        new int[2] {13,22},
        //        new int[2] {14,23},
        //        new int[2] {15,24},
        //        new int[2] {23,0},
        //        new int[2] {24,1},
        //        new int[2] {25,2},
        //        new int[2] {26,3},
        //        new int[2] {27,50},
        //        new int[2] {28,51},
        //        new int[2] {29,52},
        //        new int[2] {30,53}

        //    };

        //    int[] M1 = new int[M.Count];
        //    int[] M2 = new int[M.Count];
        //    for (int i = 0; i < M.Count; i++)
        //    {
        //        M1[i] = M[i][0];
        //        M2[i] = M[i][1];
        //    }


        //    BoundariesWithIndex initialBoundaries = new BoundariesWithIndex() { listOfBoundaries = new List<BoundaryWithIndex>() };
        //    BoundaryWithIndex boundary = new BoundaryWithIndex() { lower = M1[0], lowerIndex = 0 };
        //    for (int i = 1; i < M1.Length; i++)
        //    {
        //        if (Math.Abs(M1[i] - M1[i - 1]) > 5)
        //        {
        //            boundary.upper = M1[i - 1];
        //            boundary.upperIndex = i - 1;
        //            initialBoundaries.listOfBoundaries.Add(boundary);

        //            boundary = new BoundaryWithIndex() { lower = M1[i], lowerIndex = i };
        //        }
        //    }
        //    boundary.upper = M1[M1.Length - 1];
        //    boundary.upperIndex = M1.Length - 1;
        //    initialBoundaries.listOfBoundaries.Add(boundary);

        //    BoundariesWithIndex finalBoundaries = new BoundariesWithIndex() { listOfBoundaries = new List<BoundaryWithIndex>() };
        //    foreach (BoundaryWithIndex initialBoundary in initialBoundaries.listOfBoundaries)
        //    {
        //        boundary = new BoundaryWithIndex() { lower = M2[initialBoundary.lowerIndex], lowerIndex = initialBoundary.lowerIndex };
        //        for (int i = initialBoundary.lowerIndex + 1; i < initialBoundary.upperIndex; i++) {
        //            if (Math.Abs(M2[i] - M2[i - 1]) > 5)
        //            {
        //                boundary.upper = M2[i - 1];
        //                boundary.upperIndex = i - 1;
        //                finalBoundaries.listOfBoundaries.Add(boundary);

        //                boundary = new BoundaryWithIndex() { lower = M2[i], lowerIndex = i };
        //            }
        //        }
        //        boundary.upper = M2[initialBoundary.upperIndex - 1];
        //        boundary.upperIndex = initialBoundary.upperIndex;
        //        finalBoundaries.listOfBoundaries.Add(boundary);
        //    }

        //    List<List<Boundary>> boundaries = new List<List<Boundary>>();
        //    foreach (BoundaryWithIndex bound in finalBoundaries.listOfBoundaries)
        //    {
                
        //        Boundary susp = new Boundary() { lower = M1[bound.lowerIndex], upper = M1[bound.upperIndex] };
        //        Boundary original = new Boundary() { lower = M2[bound.lowerIndex], upper = M2[bound.upperIndex] };
        //        List<Boundary> mList = new List<Boundary> { susp, original};
        //        boundaries.Add(mList);

        //    }
        //    return boundaries;
        //}
    }
}
