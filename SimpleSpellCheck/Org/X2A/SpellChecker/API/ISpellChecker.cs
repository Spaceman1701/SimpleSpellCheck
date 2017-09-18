using System;
using System.Collections.Generic;
using System.Text;

namespace Org.X2A.SpellChecker.API
{
    public interface ISpellChecker
    {
        bool IsWordCorrect(string word);
        string GetBestMatch(string word);
    }
}
