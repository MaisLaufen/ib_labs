using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interfaces;

namespace alphabet
{
    class Symbol
    {
        public char symbol;
        public string bits;
        public Symbol(char symbol, string bits)
        {
            this.symbol = symbol;
            this.bits = bits;
        }
    }

    public class AlphabetOperations : IAlphabetOperations
    {
        static string alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_";
        public uint getBinaryCode(char letter) 
        {
            for (uint i = 0; i < alphabet.Length; i++)
            {
                if (alphabet[(int)i] == letter) return i+1;
            }
            return 0;
        }

        public char getChar(uint binaryCode)
        {
            return alphabet[(int)binaryCode-1];
        }
        
        public char getSum(char X, char Y)
        {
            int n1 = 0;
            int n2 = 0;

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (alphabet[i] == X){n1 = i+1;}
                else if (alphabet[i] == Y) { n2 = i+1; }
            }

            int sum = n1 + n2;
            int index = sum % alphabet.Length;

            return alphabet[index];
        }

        public uint getDiff(char X, char Y)
        {
            int n1 = 0;
            int n2 = 0;

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (alphabet[i] == X) { n1 = i + 1; }
                else if (alphabet[i] == Y) { n2 = i + 1; }
            }

            int diff = n1 - n2;

            if (diff >= 0) return alphabet[diff];
            else
            {
                return alphabet[alphabet.Length + diff];
            }
        }
    }
}
