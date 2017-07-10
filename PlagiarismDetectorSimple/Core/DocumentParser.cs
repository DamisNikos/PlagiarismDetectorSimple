using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toxy;
using System.IO;
using System.Diagnostics;

namespace PlagiarismDetectorSimple.Core
{
    class DocumentParser
    {
        public static string[] GetText(string path)
        {
            string text = null;
            string extension = Path.GetExtension(path);
            try
            {
                ParserContext context = new ParserContext(path);
                if (extension.Equals(".txt"))
                {
                    ITextParser parser = ParserFactory.CreateText(context);
                    text = parser.Parse().ToString().ToLower().Replace('\n', ' ').Replace('\r', ' ')
                .Replace('\t', ' ');
                }
                else if (extension.Equals(".pdf") || extension.Equals(".docx") || extension.Equals(".doc"))
                {
                    IDocumentParser parser = ParserFactory.CreateDocument(context);
                    text = parser.Parse().ToString().ToLower().Replace('\n', ' ').Replace('\r', ' ')
                .Replace('\t', ' ');
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception found at GetText()");
                Debug.WriteLine(e.Message);
            }
            text = RemovePunctuation(text);
            string[] words = text.Split(default(Char[]), StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        //Removes punctuation
        private static string RemovePunctuation(string s)
        {
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }

            s = sb.ToString();
            return s;
        }

    }
}
