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
        public static List<stopWord> GetStopWordPresentation(string[] docWords)
        {
            String[] top50words = DocumentParser.GetText(@"Files\\Top50UsedWords.docx");

            List<stopWord> StopWordPresentation = new List<stopWord>();

            //iterate through all document's words in text presentation
            for (int i = 0; i < docWords.Length; i++)
            {
                foreach (string commonWord in top50words)
                {
                    //if match is found add this word in the stopNword presentation
                    if (docWords[i].Equals(commonWord))
                    {
                        StopWordPresentation.Add(new stopWord() { _index = i, _word = docWords[i] });
                    }
                }
            }
            return StopWordPresentation;
        }


        //Step-2
        //1.Get the stopNword presentation
        //2.Calculate the size of nGram presentation
        //3.Create the document's profile in n-gram stopNword        
        public static ProfileStopWord GetProfileStopWord(List<stopWord> swPresentation, int nGramSize)
        {
            //calculate the size of nGram presentation
            int targetIndex = swPresentation.Count + 1 - nGramSize;
            List<StopWordNGram> docProfile = new List<StopWordNGram>();
            //iterate through each n-gram
            for (int i = 0; i < targetIndex; i++)
            {
                StopWordNGram ngram = new StopWordNGram() { _stopWords = new List<stopWord>() };
                //add words to each n-gram
                for (int j = 0; j < nGramSize; j++)
                {
                    ngram._stopWords.Add(swPresentation[i+j]);
                }
                //calculate the first and last index (in document's words) of the ngram
                ngram._firstIndex = ngram._stopWords[0]._index;
                ngram._lastIndex = ngram._stopWords[nGramSize - 1]._index;
                //add current n-gram to the profile
                docProfile.Add(ngram);
            }

            ProfileStopWord profile = new ProfileStopWord() { ngrams = new List<StopWordNGram>() };
            profile.ngrams = docProfile;
            return profile;
        }
        
    }
}
