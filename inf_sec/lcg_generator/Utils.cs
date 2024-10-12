using inf_sec.tritimus_encode;

namespace inf_sec.lcg_generator;

public static class Utils
{
    public static ulong[] SeedToNums(List<Char>[] seed)
    {
        Converter converter = new Converter();
        List<ulong> result = new List<ulong>();
        foreach (List<Char> item in seed)
        {
            result.Add(converter.ConvertBlockToNumb(item));
        }

        return result.ToArray();
    }

    public static ulong CountUnityBits(ulong numIn)
    {
        ulong output = 0;
        ulong tmp = 0;
        for (int i = 0; i < 20; i++)
        {
            tmp = numIn % 2;
            numIn /= 2;
            output += tmp;
        }

        return output;
    }

    public static ulong ComposeNum(ulong numIn1, ulong numIn2, ulong contIn)
    {
        ulong output;

        if (contIn > 0 && contIn < 20)
        {
            ulong[] arr1 = DecToBin(numIn1);
            ulong[] arr2 = DecToBin(numIn2);
            ulong[] tmp = new ulong[20];

            for (ulong i = 0; i < contIn; i++)
            {
                tmp[i] = arr1[i];
            }

            for (int i = (int)contIn; i < 20; i++)
            {
                tmp[i] = arr2[i];
            }

            output = BinToDec(tmp);
        }
        else if (contIn == 0)
        {
            output = numIn1;
        }
        else
        {
            output = numIn2;
        }

        return output;
    }

    public static ulong[] DecToBin(ulong numIn)
    {
        List<ulong> output = new List<ulong>();

        for (int i = 0; i < 20; i++)
        {
            output.Add(numIn % 2);
            numIn /= 2;
        }

        output.Reverse();
        return output.ToArray();
    }

    public static ulong BinToDec(ulong[] binIn)
    {
        ulong output = 0;

        for (int i = 0; i < binIn.Length; i++)
        {
            output = 2 * output + binIn[i];
        }

        return output;
    }

    public static string oneWayFuncSBlockTritimus(string block, string constant, uint roundAmount)
    {
        var alphabet = new AlphabetOperations().getAlphabet();
        SBlockTritimus sTritimus = new SBlockTritimus(alphabet, 1);

        string data = block;
        string key = constant + block;
        for (int i = 0; i < roundAmount; i++)
        {
            data = sTritimus.encryptSTritimusM(data, key, 0);
            key = data + new string(sTritimus.getModifiedAlphabet(key).ToArray());
        }

        return data;
    }

    public static uint[][] getDefaultCoefficients()
    {
        uint[][] coefficients = new uint[3][];
        coefficients[0] = [723482, 8677, 983609];
        coefficients[1] = [252564, 9109, 961193];
        coefficients[2] = [357630, 8971, 948209];
        return coefficients;
    }

    public static List<Char>[] MakeSeeds(string block)
    {
        string[] keys = ["ПЕРВЫЙ_ГЕНЕРАТОР", "ВТОРОЙ_ГЕНЕРАТОР", "ТРЕТИЙ_ГЕНЕРАТОР"];
        return keys.Select(k => oneWayFuncSBlockTritimus(block, k, 10).ToCharArray().ToList()).ToArray();
    }

    public static string CheckSeed(string blockIn)
    {
        string C = "ОТВЕТСТВЕННЫЙ_ПОДХОД";
        string[] T = new string[4];
        for (int i = 0; i < 4; i++)
        {
            T[i] = blockIn.Substring(i * 4, 4);
        }

        for (int j = 0; j < 3; j++)
        {
            for (int i = j + 1; i < 4; i++)
            {
                if (T[i] == T[j])
                {
                    T[i] = oneWayFuncSBlockTritimus(T[j], C, (uint)(j + 2 * i));
                }
            }
        }

        return string.Join("", T);
    }
}