using System;
using System.Collections.Generic;
using System.Text;
using Org.X2A.SpellChecker.API;
namespace Org.X2A.SpellChecker.Core
{
    /*
     * Corrects the word with the closet word in the dictionary. If multiple close words exist, the correction is selected arbitrarily.
     * 
     * Checking for correctness is O(1). Searching for a correction is around O(log n) (with a worst case of O(n)) where n is the dictionary size.
     */
    public class SimpleSpellChecker : ISpellChecker
    {
        private ISet<string> words;
        private WordTree correctionTree;


        public SimpleSpellChecker(ICollection<string> words)
        {
            words = new HashSet<string>(words);
            correctionTree = new WordTree(words);
        }

        public string GetBestMatch(string word)
        {
            if (!IsWordCorrect(word))
            {
                List<string> corrections = correctionTree.GetBestMatch(word);
                if (corrections.Count > 0)
                {
                    return corrections[0];
                } else
                {
                    return null;
                }
            } else
            {
                return word;
            }
        }

        public bool IsWordCorrect(string word)
        {
            return words.Contains(word);
        }
    }
}
