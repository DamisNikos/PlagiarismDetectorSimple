using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Demos
{
    class IntersectionTest
    {
        public static void Test1() {
            List<List<string>> myList1 = new List<List<string>>()
            {
                new List<string>(){"one", "two", "three"},
                new List<string>(){"two", "one", "three" },
                new List<string>(){ "1", "2", "3"},
                new List<string>(){ "3", "2", "1"}
            };

            List<List<string>> myList2 = new List<List<string>>()
            {
                new List<string>(){"three", "four", "fibne" },
                new List<string>(){ "3", "2", "1"},
                new List<string>(){ "one","two","the"},
                new List<string>(){ "one","two","three"}
            };


            List<int[]> intList = new List<int[]>()
            {
                new int[] {1,2,5,8,9 },
                new int[] {1,2,5,8,9 },
                new int[] {3,7,1,10,5,8 },
                new int[] {3,7,2,10,5,8 },
                new int[] {3,7,3,10,5,8 }
            };
            List<int[]> intList1 = new List<int[]>()
            {
                new int[] {3,7,1,10,5,8 },
                new int[] {3,7,1,10,6,8 },
                new int[] {3,7,1,10,7,8 },
                new int[] {3,7,1,10,8,8 },
                new int[] {1,2,5,8,9 }

            };

            List<int[]> list = intList.Except(intList1).ToList();


        }

    }
}
