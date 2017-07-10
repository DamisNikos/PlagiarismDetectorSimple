using PlagiarismDetectorSimple.Core;
using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Demos
{
    class Algorithm
    {
        public static void Run(string _suspicious, string _original) {
            int n1 = 11;
            int n2 = 8;
            int n3 = 3;
            int thetaG = 5;
            string original = _original;
            string suspicious = _suspicious;

            //========================Step-1 CREATING THE PROFILES OF STOP-N-WORDS NGRAMS===========================
            //Step-1.1
            //Get a normalised(all lowercase, no punctuation) string[] of all the words in the documents
            string[] wordsOfSuspicious = DocumentParser.GetText(suspicious);
            string[] wordsOfOriginal = DocumentParser.GetText(original);
            //Step-1.2
            //Get a list of strings of all 50 most used words contained in the document
            List<String> stopWordSuspicious = ProfileStopWordBuilder.GetStopWordPresentation(wordsOfSuspicious);
            List<String> stopWordOriginal = ProfileStopWordBuilder.GetStopWordPresentation(wordsOfOriginal);
            //Step-1.3
            //Get the document's profile in stopword ngrams
            ProfileStopWord profileSuspicious = ProfileStopWordBuilder.GetProfileStopWord(stopWordSuspicious, n1);
            ProfileStopWord profileOriginal = ProfileStopWordBuilder.GetProfileStopWord(stopWordOriginal, n1);


            //=======================++==========Step-2 Canditate Retrieval=========================================
            //Step-2.1
            //Get the intersected(common ngrams) profile of the 2 documents
            ProfileStopWord inter = ProfileIntersection.IntersectProfiles(profileSuspicious, profileOriginal);

            //Step-2.1.1
            //Check to see if any common ngrams found
            if (inter.ngrams.Count == 0)
            {
                Console.WriteLine("----------------");                                              //**************************
                Console.WriteLine("File {0} checked against file {1} and is not a plagiarism case", //**************************
                                      Path.GetFileName(suspicious), Path.GetFileName(original));    //**************************
                return;
            }
            //Step-2.2
            //Apply criterion (1) to the canditate intersection to filter out false positives
            ProfileStopWord finalInter = Criteria.ApplyCanditateRetrievalCriterion(inter);
            //Step-2.1.1
            //Check to see if any common ngrams found after the applying criterion(1)
            if (finalInter.ngrams.Count == 0)
            {
                Console.WriteLine("----------------");                                              //**************************
                Console.WriteLine("File {0} checked against file {1} and is not a plagiarism case", //**************************
                                      Path.GetFileName(suspicious), Path.GetFileName(original));    //**************************
                return;
            }

            //===============++++++=============Step-3 Passage Boundary Detection========++==========================
            //Step-3.1. REDO Step-1.3 to produce profile of ngrams with different size in order to apply criterion (2)
            ProfileStopWord profileSuspiciousBound = ProfileStopWordBuilder.GetProfileStopWord(stopWordSuspicious, n2);
            ProfileStopWord profileOriginalBound = ProfileStopWordBuilder.GetProfileStopWord(stopWordOriginal, n2);
            //Step-3.2 REDO Step-2.1 to intersect the profiles
            ProfileStopWord interBound = ProfileIntersection.IntersectProfiles(profileSuspiciousBound, profileOriginalBound);
            //Step-3.4
            //Apply criterion (2) to avoid noise of coincidental matches
            ProfileStopWord finalInterBound = Criteria.ApplyMatchCriterion(interBound);
            //Step-3.5
            //Get a list M of matched Ngrams
            //where members of M are ordered according to the first appearance of a match in the suspicious document
            List<int[]> M = Criteria.MatchedNgramSet(profileSuspiciousBound, profileOriginalBound, finalInterBound);
            //Step-3.6 Apply criterion (3)
            List<List<Boundary>> boundaries = BoundaryDetection.DetectInitialSet(M, thetaG);
            //Step-3.7 Apply criterion (4)
            Boundaries boundariesSuspicious = new Boundaries() { listOfBoundaries = new List<Boundary>() };
            Boundaries boundariesOriginal = new Boundaries() { listOfBoundaries = new List<Boundary>() };
            foreach (List<Boundary> mBoundary in boundaries)
            {
                boundariesSuspicious.listOfBoundaries.Add(mBoundary[0]);
                boundariesOriginal.listOfBoundaries.Add(mBoundary[1]);
            }

            Boundaries passageBoundariesSuspicious = BoundaryConverter.StopWordToWord(boundariesSuspicious, wordsOfSuspicious, n2);
            Boundaries passageBoundariesOriginal = BoundaryConverter.StopWordToWord(boundariesOriginal, wordsOfOriginal, n2);
            Console.WriteLine($"{passageBoundariesSuspicious.listOfBoundaries.Count} " +
                              $"matching passages detected between documents {Path.GetFileName(suspicious)}" +
                              $" and {Path.GetFileName(original)}");
            for (int i = 0; i < passageBoundariesOriginal.listOfBoundaries.Count; i++)
            {
                Console.WriteLine($"Passage no.{i+1}:");
                Console.WriteLine($"Document {Path.GetFileName(suspicious)}");
                for (int j = passageBoundariesSuspicious.listOfBoundaries[i].lower;
                    j <= passageBoundariesSuspicious.listOfBoundaries[i].upper; j++)
                {
                    Console.Write($"{wordsOfSuspicious[j]} ");
                }
                Console.WriteLine();
                Console.WriteLine($"Document {Path.GetFileName(original)}");
                for (int j = passageBoundariesOriginal.listOfBoundaries[i].lower;
                    j <= passageBoundariesOriginal.listOfBoundaries[i].upper; j++)
                {
                    Console.Write($"{wordsOfOriginal[j]} ");
                }
                Console.WriteLine();
            }



            return;




        }
    }
}
