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
        string seed1 = "ФДЛЫАТЫЫВАЩШЦИАЩ";
        string seed2 = "АААААААААААААААА";
        string seed3 = "ЯЧ_ФЩЗЖ_ПИАБ_ЖХЦ";

        var res1 = LCGGeneratorQuiz(seed1);
        var res2 = LCGGeneratorQuiz(seed2);
        var res3 = LCGGeneratorQuiz(seed3);
        
        foreach (var f in res3)
        {
            Console.Write(f + "\n");
        }

        Console.Read();
    }
    
    public static float[] LCGGeneratorQuiz(string seed)
    {
        List<uint> bits = new();
        Converter converter = new();
        float[] arrZeros = new float[80];
        float[] arrOnes = new float[80];
        float[] chance = new float[80];
        var lcg = new HCLCGMWrapper();
        lcg.Init(seed, Utils.getDefaultCoefficients());
        for (int i = 0; i < 10000; i++)
        {
            bits = [];

            var temp = lcg.GenerateCodes();
            var tempList = temp.ToCharArray().ToList();
            for (int j = 0; j < 4; j++)
            {
                var tempLong = converter.ConvertBlockToNumb(tempList.GetRange(j * 4, 4));
                bits.AddRange(((converter.ConverNumbToBinaryArray(tempLong).ToList())));
            }
            for (int k = 0; k < bits.Count; k++)
            {
                if (bits[k] == 0) arrZeros[k]++;
                if (bits[k] == 1) arrOnes[k]++;
            }
        }

        for (int i = 0; i < chance.Length; i++)
        {
            chance[i] = arrZeros[i] / (arrOnes[i] + arrZeros[i]);
        }
        
        return chance;
    }

}


