using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Core
{
    class BoundaryConverter
    {
        public static Boundaries StopWordToWord(Boundaries boundaries, string[] wordsOfDocument, int nGramSize)
        {
            String[] top50words = DocumentParser.GetText(@"Files\\Top50UsedWords.docx");
            Boundaries targetBoundaries = new Boundaries() { listOfBoundaries = new List<Boundary>() };

            int currentIndexOfStopWord = 0;
            

            foreach (Boundary boundary in boundaries.listOfBoundaries)
            {
                boundary.upper += nGramSize - 1;
                bool foundBoundary = false;
                Boundary targetBoundary = new Boundary();
                for (int i = 0; i<wordsOfDocument.Length; i++)
                {
                    foreach (string commonWord in top50words)
                    {
                        if (wordsOfDocument[i].Equals(commonWord))
                        {
                            if (currentIndexOfStopWord == boundary.lower)
                            {
                                targetBoundary.lower = i;
                            }
                            if (currentIndexOfStopWord == boundary.upper) {
                                targetBoundary.upper = i;
                                targetBoundaries.listOfBoundaries.Add(targetBoundary);
                                foundBoundary = true;
                                break;
                            }
                            currentIndexOfStopWord++;
                        }
                    }
                    if (foundBoundary)
                    {
                        break;
                    }
                }
            }
            return targetBoundaries;
        }
    }
}
