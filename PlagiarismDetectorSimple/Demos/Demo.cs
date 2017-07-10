using PlagiarismDetectorSimple.Core;
using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Demos
{
    class Demo
    {
        public static void DemoRun() {
            int n1 = 11;
            int n2 = 8;
            int n3 = 3;
            int thetaG = 100;
            string original1 = @"Files\Paper\original1.pdf";
            string suspicious1 = @"Files\Paper\plagiarized.pdf";

            string[] wordsOfSuspicious = DocumentParser.GetText(suspicious1);
            string[] wordsOfOriginal = DocumentParser.GetText(original1);
            List<String> stopWordSuspicious = ProfileStopWordBuilder.GetStopWordPresentation(wordsOfSuspicious);
            List<String> stopWordOriginal = ProfileStopWordBuilder.GetStopWordPresentation(wordsOfOriginal);
            ProfileStopWord profileSuspicious = ProfileStopWordBuilder.GetProfileStopWord(stopWordSuspicious, n1);
            ProfileStopWord profileOriginal = ProfileStopWordBuilder.GetProfileStopWord(stopWordOriginal, n1);
            ProfileStopWord inter = ProfileIntersection.IntersectProfiles(profileSuspicious, profileOriginal);
            ProfileStopWord finalInter = Criteria.ApplyCanditateRetrievalCriterion(inter);

            ProfileStopWord profileSuspiciousBound = ProfileStopWordBuilder.GetProfileStopWord(stopWordSuspicious, n2);
            ProfileStopWord profileOriginalBound = ProfileStopWordBuilder.GetProfileStopWord(stopWordOriginal, n2);
            ProfileStopWord interBound = ProfileIntersection.IntersectProfiles(profileSuspiciousBound, profileOriginalBound);
            ProfileStopWord finalInterBound = Criteria.ApplyMatchCriterion(interBound);

            List<int[]> M = Criteria.MatchedNgramSet(profileSuspiciousBound, profileOriginalBound, finalInterBound);


            List<List<Boundary>> boundaries = BoundaryDetection.DetectInitialSet(M, thetaG);
            Boundaries boundariesSuspicious = new Boundaries() { listOfBoundaries = new List<Boundary>() };
            Boundaries boundariesOriginal = new Boundaries() { listOfBoundaries = new List<Boundary>() };
            foreach (List<Boundary> mBoundary in boundaries)
            {
                boundariesSuspicious.listOfBoundaries.Add(mBoundary[0]);
                boundariesOriginal.listOfBoundaries.Add(mBoundary[1]);
            }

            //Post-proccessing
            Boundaries passageBoundariesSuspicious = BoundaryConverter.StopWordToWord(boundariesSuspicious, wordsOfSuspicious, n2);
            Boundaries passageBoundariesOriginal = BoundaryConverter.StopWordToWord(boundariesOriginal, wordsOfOriginal, n2);

            List<ProfileCharacter> passagesSuspicious = new List<ProfileCharacter>();
            List<ProfileCharacter> passagesOriginal = new List<ProfileCharacter>();
            for (int i = 0; i < passageBoundariesOriginal.listOfBoundaries.Count; i++)
            {
                passagesSuspicious.Add(ProfileCharacterBuilder.GetProfileCharacter(wordsOfSuspicious,
                                                                                     passageBoundariesSuspicious.listOfBoundaries[i], n3));
                passagesOriginal.Add(ProfileCharacterBuilder.GetProfileCharacter(wordsOfOriginal,
                                                                                     passageBoundariesOriginal.listOfBoundaries[i], n3));


            }

            for (int i = 0; i < passagesSuspicious.Count; i++)
            {
                ProfileCharacter intersected = ProfileIntersection.IntersectProfiles(passagesSuspicious[i], passagesOriginal[i]);
                float similarityScore = Criteria.SimilarityScore(passagesSuspicious[i], passagesOriginal[i], intersected);
                Console.WriteLine();
                Console.WriteLine(similarityScore);
            }

            Debugger.Break();

        }


    }
}
