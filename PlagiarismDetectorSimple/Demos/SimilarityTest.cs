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
    class SimilarityTest
    {
        public static float RunSimilarityTest()
        {
            string original = @"Files\Paper\original1.pdf";
            string suspicious = @"Files\Paper\plagiarized.pdf";
            int n3 = 3;
            //Step-1.1
            //Get a normalised(all lowercase, no punctuation) string[] of all the words in the documents
            string[] wordsOfSuspicious = DocumentParser.GetText(suspicious);
            string[] wordsOfOriginal = DocumentParser.GetText(original);


            Boundaries passageBoundariesSuspicious = new Boundaries()
            {
                listOfBoundaries = new List<Boundary>
                {
                    new Boundary()
                    {
                        lower = 0,
                        upper = 54
                    }
                }
            };
            Boundaries passageBoundariesOriginal = new Boundaries()
            {
                listOfBoundaries = new List<Boundary>
                {
                    new Boundary()
                    {
                        lower = 0,
                        upper = 49
                    }
                }
            };

            ProfileCharacter passageSuspicious = ProfileCharacterBuilder.GetProfileCharacter(
                    wordsOfSuspicious, passageBoundariesSuspicious.listOfBoundaries[0], n3);
            ProfileCharacter passageOriginal = ProfileCharacterBuilder.GetProfileCharacter(
                wordsOfOriginal, passageBoundariesOriginal.listOfBoundaries[0], n3);
            //Step-4.1.1
            //Remove duplicate entries
            passageSuspicious = ProfileCharacterBuilder.RemoveDuplicates(passageSuspicious);
            passageOriginal = ProfileCharacterBuilder.RemoveDuplicates(passageOriginal);

            passageSuspicious = ProfileCharacterBuilder.RemoveDuplicates(passageSuspicious);
            passageOriginal = ProfileCharacterBuilder.RemoveDuplicates(passageOriginal);
            //============================Step-5 Post-processing=============================================
            //Step-5.1  (overloading method used on step-2.1 and step-3.2)
            //Get the intersected(common ngrams) profile of the 2 passages
            ProfileCharacter passageIntersection = ProfileIntersection.IntersectProfiles(
                 passageSuspicious, passageOriginal);
            //Step-5.2
            //Apply criterion (5) to find the similarity score between the 2 profiles
            float similarityScore = Criteria.SimilarityScore(
                passageSuspicious, passageOriginal, passageIntersection);

            return similarityScore;
        }
    }
}
