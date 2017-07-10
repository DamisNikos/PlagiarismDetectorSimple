using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Core
{
    class Criteria
    {
        public static ProfileStopWord ApplyCanditateRetrievalCriterion(ProfileStopWord profile)
        {
            List<string> mostCommon6 = new List<string> { "the", "of", "and", "a", "in", "to" };
            List<List<string>> nGramCollection = new List<List<string>>();
            //iterate through all ngrams that belongs to the interesection of the two profiles
            foreach (var ngram in profile.ngrams)
            {
                int maxseq = 0;
                int membersofC = 0;
                int currentsq = 0;
                //iterate through all members of the ngram
                foreach (var word in ngram)
                {
                    bool found = false;
                    //iterate through all words in C
                    for (int i = 0; i < mostCommon6.Count; i++)
                    {
                        //increase sequence and member if match is found and stop iterating C
                        if (word.Equals(mostCommon6[i]))
                        {
                            membersofC++;
                            currentsq++;
                            found = true;
                            break;
                        }
                    }
                    //if match is not found compare this sequence against previous maximal and update accordingly
                    if (!found)
                    {
                        if (maxseq < currentsq) maxseq = currentsq;
                        currentsq = 0;
                    }

                }
                //if criterion (1) is satisfied add this ngram to the collection
                if ((membersofC < ngram.Count - 1) && (maxseq < ngram.Count - 2))
                {
                    nGramCollection.Add(ngram);
                }
            }
            ProfileStopWord profileStop = new ProfileStopWord();
            profileStop.ngrams = nGramCollection;
            return profileStop;
        }

        public static ProfileStopWord ApplyMatchCriterion(ProfileStopWord profile)
        {
            List<string> mostCommon6 = new List<string> { "the", "of", "and", "a", "in", "to" };
            List<List<String>> nGramCollection = new List<List<string>>();
            //iterate through each n-gram
            foreach (var ngram in profile.ngrams)
            {
                int membersofC = 0;
                //iterate through each word in the n-gram
                foreach (var word in ngram)
                {
                    //iterate through each word in C
                    for (int i = 0; i < mostCommon6.Count; i++)
                    {
                        //if match is found increase members
                        if (word.Equals(mostCommon6[i]))
                        {
                            membersofC++;
                            break;
                        }
                    }
                }
                //if criterion (2) is satisfied add this ngram to the collection
                if ((membersofC < ngram.Count))
                {
                    nGramCollection.Add(ngram);
                }
            }
            ProfileStopWord profileStop = new ProfileStopWord();
            profileStop.ngrams = nGramCollection;
            return profileStop;
        }

        public static List<int[]> MatchedNgramSet(ProfileStopWord suspiciousProfile, ProfileStopWord originalProfile, ProfileStopWord commonProfile)
        {
            List<int[]> setOfMatched = new List<int[]>();
            foreach (var ngram in commonProfile.ngrams)
            {
                int index1 = suspiciousProfile.ngrams.IndexOf(ngram);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
                //.....................IndexOF() does not work for the second profile..................//
                int location = 0;
                for (int index2 = 0; index2 < originalProfile.ngrams.Count; index2++)
                {
                    int matches = 0;
                    for (int i = 0; i < originalProfile.ngrams[index2].Count; i++)
                    {
                        if (originalProfile.ngrams[index2][i].Equals(ngram[i]))
                        {
                            matches++;
                        }
                    }
                    if (matches == originalProfile.ngrams[index2].Count)
                    {
                        location = index2;
                    }
                }
                setOfMatched.Add(new int[] { index1, location  });
            }
            return setOfMatched;
        }

        public static float SimilarityScore(ProfileCharacter suspicious, ProfileCharacter original, ProfileCharacter intersection)
        {
            float similarity;
            float sizeOfProfile1 = suspicious.ngrams.Count;
            float sizeOfProfile2 = original.ngrams.Count;
            float sizeOfIntersection = intersection.ngrams.Count;
            float maxSize;
            maxSize = sizeOfProfile1 > sizeOfProfile2 ? sizeOfProfile1 : sizeOfProfile2;

            similarity = sizeOfIntersection / maxSize;
            return similarity;
        }
    }
}
