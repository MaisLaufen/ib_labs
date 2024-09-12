using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<char> shiftTable(List<char> alphabet,int k)
        {
            alphabet.Insert(k, alphabet[_alphLen - 1]);
            return alphabet;
        }

        public string encryptPolyTritimus(string word, string key)
        {
            int wordLen = word.Length;
            char[] encryptedWord = new char[wordLen];
            List<char> modifAlph = getModifiedAlphabet(key);

            for (int i = 0; i < wordLen; i++)
            {
                encryptedWord[i] = encryptTheChar(word[i], modifAlph);
                modifAlph = shiftTable(modifAlph, ((i+1)% wordLen)%_alphLen);
            }

            return new string(encryptedWord);
        }

        public string decryptPolyTritimus(string word, string key)
        {
            int wordLen = word.Length;
            char[] decryptedWord = new char[wordLen];
            List<char> modifAlph = getModifiedAlphabet(key);

            for (int i = 0; i < wordLen; i++)
            {
                decryptedWord[i] = decryptTheChar(word[i], modifAlph);
                modifAlph = shiftTable(modifAlph, ((i + 1) % wordLen) % _alphLen);
            }

            return new string(decryptedWord);
        }

        public string encryptSBlockTritimus(string blockIn, string keyIn, int jIn)
        {
            if (blockIn.Length != 4)
            {
                return "input_error";
            }

            List<char> keyTable = getModifiedAlphabet(keyIn);
            int jm = jIn % _alphLen;
            if (jm > 0)
            {
                for (int j = 0; j < jm; j++)
                {
                    keyTable = shiftTable(keyTable, j);
                }
            }

            string output = "";
            for (int i = 0; i < 4; i++)
            {
                char tmp = blockIn.Substring(i, 1)[0];
                int t = (jIn + i) % 32;
                keyTable = shiftTable(keyTable, t);
                int pos = keyTable.IndexOf(tmp);
                char csym = keyTable[(pos + 8) % 32];
                output += csym;
            }

            return output;
        }

        public string decryptSBlockTritimus(string blockIn, string keyIn, int jIn)
        {
            if (blockIn.Length != 4)
            {
                return "input_error";
            }

            List<char> keyTable = getModifiedAlphabet(keyIn);
            int jm = jIn % _alphLen;
            if (jm > 0)
            {
                for (int j = 0; j < jm; j++)
                {
                    keyTable = shiftTable(keyTable, j);
                }
            }

            string output = "";
            for (int i = 0; i < 4; i++)
            {
                char tmp = blockIn.Substring(i, 1)[0];
                int t = (jIn + i) % 32;
                keyTable = shiftTable(keyTable, t);
                int pos = keyTable.IndexOf(tmp);
                char csym = keyTable[(32  + pos - 8) % 32];
                output += csym;
            }

            return output;
        }

        public string encryptImproveBlock(string blockIn, string keyIn, int jIn)
        {
            string t = keyIn;
            while (jIn > t.Length - 4)
            {
                t += t; 
            }
            alphabet.AlphabetOperations op = new alphabet.AlphabetOperations();

            string key = t.Substring(jIn, 4);

            uint[] k = op.textToArray(key);
            uint[] b = op.textToArray(blockIn);

            uint q = (k[0] + k[1] + k[2] + k[3]) % 4;

            for (uint i = 0; i <= 2; i++)
            {
                uint j = (q + i + 1) % 4;
                uint l = (q + i) % 4;
                b[j] = (b[j] + b[l]) % 32;
            }

            return op.arrayToText(b);
        }
        public string encryptSTritimusM(string blockIn, string keyIn, int jIn)
        {
            string tmp = encryptSBlockTritimus(blockIn, keyIn, jIn);
            string output = encryptImproveBlock(tmp, keyIn, jIn);
            return output;
        }

        public string decryptImproveBlock(string blockIn, string keyIn, int jIn)
        {
            string t = keyIn;
            while (jIn > t.Length - 4)
            {
                t += t;
            }
            alphabet.AlphabetOperations op = new alphabet.AlphabetOperations();

            string key = t.Substring(jIn, 4);

            uint[] k = op.textToArray(key);
            uint[] b = op.textToArray(blockIn);

            uint q = (k[0] + k[1] + k[2] + k[3]) % 4;

            for (int i = 2; i >= 0; i--)
            {
                uint j = (uint)(q + i + 1) % 4;
                uint l = (uint)(q + i) % 4;
                b[j] = (b[j] - b[l] + 32) % 32;
            }

            return op.arrayToText(b);
        }

        public string decryptSTritimusM(string blockIn, string keyIn, int jIn)
        {
            string tmp = decryptSBlockTritimus(blockIn, keyIn, jIn);
            string output = decryptImproveBlock(tmp, keyIn, jIn);
            return output;
        }
    }
}
