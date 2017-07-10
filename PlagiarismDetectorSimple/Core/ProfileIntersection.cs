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
            List<List<string>> intersection = new List<List<string>>();
            foreach (List<string> ngram1 in profile1.ngrams)
            {
                int countEquals = 0;
                foreach (List<string> ngram2 in profile2.ngrams)
                {
                    int countEqualWords = 0;
                    for (int i = 0; i < ngram1.Count; i++)
                    {
                        if (ngram1[i].Equals(ngram2[i]))
                        {
                            countEqualWords++;
                        }
                    }
                    if (countEqualWords == ngram1.Count)
                    {
                        countEquals++;
                    }
                }
                if (countEquals > 0)
                {
                    intersection.Add(ngram1);
                }
            }

            ProfileStopWord profile = new ProfileStopWord();
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
