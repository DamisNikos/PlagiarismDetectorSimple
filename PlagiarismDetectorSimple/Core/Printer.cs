//using PlagiarismDetectorSimple.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlagiarismDetectorSimple.Core
//{
//    class Printer
//    {
//        public static void PrintProfiles(ProfileStopWord profile1) {
//            foreach (List<string> ngram in profile1.ngrams)
//            {
//                Console.Write("{");
//                foreach (string word in ngram){Console.Write("{0},", word);}
//                Console.Write("}");
//                Console.WriteLine();
//            }
//        }
//        public static void PrintMatchedList(List<int[]> M) {
//            Console.Write('{');
//            foreach (int[] match in M)
//            {
//                Console.Write("({0},{1})  ,", match[0], match[1]);
//            }
//            Console.Write('}');

//        }

//    }
//}

