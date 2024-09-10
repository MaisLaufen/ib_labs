using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static alphabet.AlphabetOperations;
using tritimus;

namespace inf_sec_1
{
    internal class Program
    {
        struct Symbol
        {
            public char symbol;
            public BitArray bits;
            public int index;
        }

        static void Main(string[] args)
        {
            string origAlphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_";
            Tritimus tritimus = new Tritimus(origAlphabet, 8);

            List<char> alp = tritimus.getModifiedAlphabet("ВЕРСАЛЬ");

            Console.WriteLine();
            string b = tritimus.encryptTheWord("ПОЛДЕНЬ", alp);
            Console.Write(b);
            
            Console.Read();
        }
    }
}
