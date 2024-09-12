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
        uint[] textToArray(string txtIn);
        string arrayToText(uint[] arrIn);
    }

    interface ITritimus 
    {
        List<char> getModifiedAlphabet(string key);
        char encryptTheChar(char letter, List<char> alphabet);
        char decryptTheChar(char encryptedChar, List<char> alphabet);
        string encryptTheWord(string word, List<char> alphabet);
        string decryptTheWord(string encryptedWord, List<char> alphabet);
        List<char> shiftTable(List<char> alphabet,int k);
        string encryptPolyTritimus(string word, string key);
        string decryptPolyTritimus(string word, string key);
        string encryptSBlockTritimus(string blockIn, string keyIn, int jIn);
        string decryptSBlockTritimus(string blockIn, string keyIn, int jIn);
        string encryptImproveBlock(string blockIn, string keyIn, int jIn);
        string decryptImproveBlock(string blockIn, string keyIn, int jIn);
        string encryptSTritimusM(string blockIn, string keyIn, int jIn);
        string decryptSTritimusM(string blockIn, string keyIn, int jIn);
    }
}
