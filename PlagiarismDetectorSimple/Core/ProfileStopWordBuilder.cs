using PlagiarismDetectorSimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectorSimple.Core
{
    class ProfileStopWordBuilder
    {
        //Step-1
        //1.Get the normalized(all lowercase, no punctuation) text presentation
        //2.Create the stopNword presentation
        //3.Return the stopNword presentation
        public static List<String> GetStopWordPresentation(string[] docWords)
        {
            String[] top50words = DocumentParser.GetText(@"Files\\Top50UsedWords.docx");

            List<string> StopWordPresentation = new List<string>();

            //iterate through all document's words in text presentation
            foreach (string word in docWords)
            {
                //iterate through top 50 used words
                foreach (string commonword in top50words)
                {
                    //if match is found add this word in the stopNword presentation
                    if (word.Equals(commonword))
                    {
                        StopWordPresentation.Add(word);
                    }
                }
            }
            return StopWordPresentation;
        }


        //Step-2
        //1.Get the stopNword presentation
        //2.Calculate the size of nGram presentation
        //3.Create the document's profile in n-gram stopNword
        public static ProfileStopWord GetProfileStopWord(List<String> swPresentation, int nGramSize)
        {
            //calculate the size of nGram presentation
            int targetIndex = swPresentation.Count + 1 - nGramSize;
            List<List<string>> docProfile = new List<List<string>>();
            //iterate through each n-gram
            for (int i = 0; i < targetIndex; i++)
            {
                List<string> ngram = new List<string>();
                //add words to each n-gram
                for (int j = 0; j < nGramSize; j++)
                {
                    ngram.Add(swPresentation[i + j]);
                }
                //add current n-gram to the profile
                docProfile.Add(ngram);
            }

            ProfileStopWord profile = new ProfileStopWord();
            profile.ngrams = docProfile;
            return profile;
        }
    }
}
