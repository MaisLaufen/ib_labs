using System.Collections;
using inf_sec.lcg_generator;
using inf_sec.tritimus_encode;

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

        List<char> modAlph = tritimus.getModifiedAlphabet("ПТЕРАДАКТИЛЬ");
        Converter converter = new Converter();

        List<Char>[] seed1 = [
            new List<Char>{'А', 'П', 'Ч', 'Х'},
            new List<Char>{'Ч', '_', 'О', 'К'},
            new List<Char>{'Ш', 'У', 'Р', 'А'}];

        uint[][] set = [
            new uint[] { 723482, 8677, 983609 },
            new uint[] { 252564, 9109, 961193 },
            new uint[] { 357630, 8971, 948209 }
        ];

        //LCGwithHC hc = new LCGwithHC(new LCG());
        ulong[] s1 = Utils.SeedToNums(seed1);

        // ulong[] ss = s1;
        // for (int i = 0; i < 10; i++)
        // {
        //     ulong[] d = hc.Next(ss, set);
        //     ss = [d[1], d[2], d[3]];

        //     foreach (var item in converter.ConvertNumbToBlock(d[0]))
        //     {
        //         Console.Write(item);
        //     }
        //     Console.WriteLine();
        // }

        Console.WriteLine();
        //Console.WriteLine(hc.oneWayFuncSBlockTritimus("ВАСЯ", "АААА", 5, origAlphabet, 1));

        Console.Read();
    }

}
