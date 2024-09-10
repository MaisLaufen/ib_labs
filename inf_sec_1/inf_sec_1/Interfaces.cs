using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfaces
{
    interface IAlphabetOperations 
    {
        uint getBinaryCode(char letter);
        char getChar(uint binaryCode);
        char getSum(char X, char Y);
        uint getDiff(char X, char Y);
    }

    interface ITritimus 
    {
        List<char> getModifiedAlphabet(string key);
        char encryptTheChar(char letter, List<char> alphabet);
        char decryptTheChar(char encryptedChar, List<char> alphabet);
        string encryptTheWord(string word, List<char> alphabet);
        string decryptTheWord(string encryptedWord, List<char> alphabet);
        string encryptPolyTritimus(string word, string key, int jIn);
        string decryptPolyTritimus(string word, string key, int jIn);
        string encryptSBlockTritimus(string word, string key, int jIn);
        string decryptSBlockTritimus(string word, string key, int jIn);
        string encryptImproveBlock(string blockIn, string keyIn, int jIn);
        string decryptImproveBlock(string blockIn, string keyIn, int jIn);
    }
}
