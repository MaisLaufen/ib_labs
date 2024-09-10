using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alphabet;
using interfaces;

namespace tritimus
{
    public class Tritimus: ITritimus
    {
        string _origAlph = "";
        int _alphLen = 0;
        int _shift = 0;

        public Tritimus(string originAlphabet, int shift)
        {
            _origAlph = originAlphabet;
            _alphLen = originAlphabet.Length;
            _shift = shift;
        }

        public List<char> getModifiedAlphabet(string key)
        {
            List<char> newAlph = (key + _origAlph).Distinct().ToList();
            return newAlph;
        }

        public char encryptTheChar(char letter, List<char> alphabet)
        {
            int index = alphabet.IndexOf(letter);
            if (index + _shift >= _alphLen) index = index + _shift - _alphLen - 1;
            else index = index + _shift;
            return alphabet[index];
        }

        public char decryptTheChar(char encryptedChar, List<char> alphabet)
        {
            int index = alphabet.IndexOf(encryptedChar);
            if (index - _shift < 0) index = _alphLen - index + _shift;
            else index = index - _shift;
            return alphabet[index];
        }

        public string encryptTheWord(string word, List<char> alphabet)
        {
            int wordLen = word.Length;
            char[] encryptedWord = new char[wordLen];
            for (int i = 0; i < wordLen; i++)
            {
                encryptedWord[i] = encryptTheChar(word[i], alphabet);
            }
            return new string(encryptedWord);
        }

        public string decryptTheWord(string word, List<char> alphabet)
        {
            int wordLen = word.Length;
            char[] decryptedWord = new char[wordLen];
            for (int i = 0; i < wordLen; i++)
            {
                decryptedWord[i] = decryptTheChar(word[i], alphabet);
            }
            return new string(decryptedWord);
        }
    }
}
