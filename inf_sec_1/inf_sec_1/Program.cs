using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using alphabet;
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
            string d = tritimus.encryptPolyTritimus("ПОЛДЕНЬ", "ВЕРСАЛЬ");
            string c = tritimus.decryptPolyTritimus("ЩШЙОДЧК", "ВЕРСАЛЬ");
            string gg = tritimus.encryptSBlockTritimus("ГРОТ", "РОЗА", 0);
            string kk = tritimus.encryptSBlockTritimus("АТОЛ", "ГОРАЦИО", 3);
            string ff = tritimus.decryptImproveBlock("ЬООЫ", "ГОРАЦИО", 3);
            string jj = tritimus.encryptSTritimusM("КРОТ", "РОЗА", 0);
            Console.Write(ff);
            
            Console.Read();
        }
    }
}
