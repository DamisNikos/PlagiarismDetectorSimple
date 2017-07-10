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
            List<StopWordNGram> nGramCollection = new List<StopWordNGram>();
            //iterate through all ngrams that belongs to the interesection of the two profiles
            foreach (var ngram in profile.ngrams)
            {
                int maxseq = 0;
                int membersofC = 0;
                int currentsq = 0;
                //iterate through all members of the ngram
                foreach (var word in ngram._stopWords)
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
                if ((membersofC < ngram._stopWords.Count - 1) && (maxseq < ngram._stopWords.Count - 2))
                {
                    nGramCollection.Add(ngram);
                }
            }
            ProfileStopWord profileStop = new ProfileStopWord() { ngrams = new List<StopWordNGram>()};
            profileStop.ngrams = nGramCollection;
            return profileStop;
        }

        public static ProfileStopWord ApplyMatchCriterion(ProfileStopWord profile)
        {
            List<string> mostCommon6 = new List<string> { "the", "of", "and", "a", "in", "to" };
            List<StopWordNGram> nGramCollection = new List<StopWordNGram>();
            //iterate through each n-gram
            foreach (var ngram in profile.ngrams)
            {
                int membersofC = 0;
                //iterate through each word in the n-gram
                foreach (var word in ngram._stopWords)
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
                if ((membersofC < ngram._stopWords.Count))
                {
                    nGramCollection.Add(ngram);
                }
            }
            ProfileStopWord profileStop = new ProfileStopWord() { ngrams = new List<StopWordNGram>()};
            profileStop.ngrams = nGramCollection;
            return profileStop;
        }

        public static List<int[]> MatchedNgramSet(ProfileStopWord suspiciousProfile, ProfileStopWord originalProfile, ProfileStopWord commonProfile)
        {
            List<int[]> setOfMatched = new List<int[]>();
            for (int i = 0; i < commonProfile.ngrams.Count; i++)
            {
                int locationSuspicious = 0;
                for (int j = 0; j < suspiciousProfile.ngrams.Count; j++)
                {
                    int matches = 0;
                    for (int k = 0; k < suspiciousProfile.ngrams[j]._stopWords.Count; k++)
                    {
                        if (suspiciousProfile.ngrams[j]._stopWords[k]._word.Equals(commonProfile.ngrams[i]._stopWords[k]._word))
                        {
                            matches++;
                        }
                    }
                    if (matches == suspiciousProfile.ngrams[j]._stopWords.Count)
                    {
                        locationSuspicious = j;
                    }
                }

                int locationOriginal = 0;
                for (int j = 0; j < originalProfile.ngrams.Count; j++)
                {
                    int matches = 0;
                    for (int k = 0; k < originalProfile.ngrams[j]._stopWords.Count; k++)
                    {
                        if (originalProfile.ngrams[j]._stopWords[k]._word.Equals(commonProfile.ngrams[i]._stopWords[k]._word))
                        {
                            matches++;
                        }
                    }
                    if (matches == originalProfile.ngrams[j]._stopWords.Count)
                    {
                        locationOriginal = j;
                    }
                }
                setOfMatched.Add(new int[] { locationSuspicious, locationOriginal });
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
