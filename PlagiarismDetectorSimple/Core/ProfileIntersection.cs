using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Core
{
    class ProfileIntersection
    {
        public static ProfileStopWord IntersectProfiles(ProfileStopWord profile1, ProfileStopWord profile2)
        {
            List<StopWordNGram> intersection = new List<StopWordNGram>();
            int ngramLength = profile1.ngrams[0]._stopWords.Count;
            for (int i = 0; i < profile1.ngrams.Count; i++)
            {
                int countEquals = 0;
                for (int j = 0; j < profile2.ngrams.Count; j++)
                {
                    int countEqualWords = 0;
                    for (int k = 0; k < ngramLength; k++)
                    {
                        if (profile1.ngrams[i]._stopWords[k]._word.Equals(profile2.ngrams[j]._stopWords[k]._word))
                        {
                            countEqualWords++;
                        }
                    }
                    if (countEqualWords == ngramLength)
                    {
                        countEquals++;
                    }
                }
                if (countEquals > 0)
                {
                    intersection.Add(new StopWordNGram() {
                        _stopWords = profile1.ngrams[i]._stopWords,
                        _firstIndex = -1,
                        _lastIndex  = -1
                    });
                }
            }
            
            ProfileStopWord profile = new ProfileStopWord() {ngrams = new List<StopWordNGram>() };
            profile.ngrams = intersection;
            return profile;
        }

        public  static ProfileCharacter IntersectProfiles(ProfileCharacter profile1, ProfileCharacter profile2)
        {
            List<List<char>> ngramsCollection = new List<List<char>>();
            foreach (var ngram1 in profile1.ngrams)
            {
                int countEquals = 0;
                foreach (var ngram2 in profile2.ngrams)
                {
                    int countEqualLetters = 0;
                    for (int i = 0; i < ngram1.Count; i++)
                    {
                        if (ngram1[i].Equals(ngram2[i]))
                        {
                            countEqualLetters++;
                        }
                    }
                    if (countEqualLetters == ngram1.Count)
                    {
                        countEquals++;
                    }
                }
                if (countEquals > 0)
                {
                    ngramsCollection.Add(ngram1);
                }
            }
            ProfileCharacter profile = new ProfileCharacter() { ngrams = ngramsCollection };
            return profile;
        }
    }

}
