using System.Collections;

namespace inf_sec;

static class Program
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

            //List<char> alp = tritimus.getModifiedAlphabet("ВЕРСАЛЬ");

            Console.WriteLine();
            string b = tritimus.encryptSBlockTritimus("БЛОК", "ЗВЗЁДНАЯ_НОЧЬ", 11);

            Console.Write(b);
            
            Console.Read();
        }
    
}
