using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inf_sec
{
    public class BinOperations
    {
        public ulong[] SeedToNums(List<Char>[] seed)
        {
            Converter converter = new Converter();
            List<ulong> result = new List<ulong>();
            foreach (List<Char> item in seed)
            {
                result.Add(converter.ConvertBlockToNumb(item));
            }
            return result.ToArray();
        }

        public uint CountUnityBits(uint numIn)
        {
            uint output = 0;
            uint tmp = 0;
            for (int i = 0; i < 20; i++)
            {
                tmp = numIn % 2;
                numIn /= 2;
                output += tmp;
            }
            return output;
        }

        public ulong ComposeNum(int numIn1, int numIn2, uint contIn)
        {
            ulong output;

            if (contIn > 0 && contIn < 20)
            {
                int[] arr1 = DecToBin(numIn1);
                int[] arr2 = DecToBin(numIn2);
                int[] tmp = new int[20];

                for (int i = 0; i < contIn; i++)
                {
                    tmp[i] = arr1[i];
                }

                for (int i = (int)contIn; i < 20; i++)
                {
                    tmp[i] = arr2[i];
                }
                output = (ulong)BinToDec(tmp);
            }
            else if (contIn == 0)
            {
                output = (ulong)numIn1;
            }
            else
            {
                output = (ulong)numIn2;
            }

            return output;
        }

        public int[] DecToBin(int numIn)
        {
            List<int> output = new List<int>();

            for (int i = 0; i < 20; i++)
            {
                output.Add(numIn % 2);
                numIn /= 2;
            }

            output.Reverse();
            return output.ToArray();
        }

        public int BinToDec(int[] binIn)
        {
            int output = 0;

            for (int i = 0; i < binIn.Length; i++)
            {
                output = 2 * output + binIn[i];
            }

            return output;
        }
    }
    public class Converter : IConverter
    {
        public ulong ConvertBlockToNumb(List<Char> block)
        {
            if (block.Count != 4) return 0;

            AlphabetOperations op = new AlphabetOperations();

            ulong output = 0;
            int pos = 1;
            uint[] temp = op.textToArray(new string(block.ToArray()));
            for (int i = 3; i >= 0; i--)
            {
                output = (ulong)pos * temp[i] + output;
                pos *= 32;
            }

            return output;
        }

        public List<Char> ConvertNumbToBlock(long number)
        {
            AlphabetOperations op = new AlphabetOperations();
            uint[] temp = new uint[4];

            for (int i = 0; i <= 3; i++)
            {
                temp[3-i] = (uint)number % 32;
                number /= 32;
            }

            return op.arrayToText(temp).ToList();
        }

        public uint[] ConverNumbToBinaryArray(ulong number)
        {
            uint[] output = new uint[20];
            for (int i = 0; i < 20; i++)
            {
                output[19 - i] = (uint)(number % 2);
                number /= 2;
            }
            return output;
        }

        public ulong ConverBinaryArrayToNumb(uint[] binArray)
        {
            uint output = 0;
            for (int i = 0; i < 20; i++)
            {
                output = 2 * output + binArray[i];
            }
            return output;
        }
    }

    public class LCG
    {
        Converter converter = new Converter();
        ulong state = 0;

        public uint Next(ulong stateIn, uint[] setIn)
        {
            state = (setIn[0] * stateIn + setIn[1]) % setIn[2];
            return (uint)state;
        }
    }

    public class LCGwithHC
    {
        LCG _lcg;
        uint first = 0;
        uint second = 0;
        uint control = 0;

        public LCGwithHC(LCG lcg)
        {
            _lcg = lcg;
        }

        public ulong[] Next(ulong[] stateIn, uint[][] setIn)
        {
            BinOperations bOp = new BinOperations();
            first = _lcg.Next(stateIn[0], setIn[0]);
            second = _lcg.Next(stateIn[1], setIn[1]);
            control = _lcg.Next(stateIn[2], setIn[2]);

            uint n = bOp.CountUnityBits(control);

            ulong output = 0;
            if (control % 2 == 0)
            {
                output = bOp.ComposeNum((int)first, (int)second, n);
            } else
            {
                output = bOp.ComposeNum((int)second, (int)first, n);
            }
            return [output, first, second, control];
        }

        public string oneWayFuncSBlockTritimus(string block, string constant, uint roundAmount, string alphabet, int shift)
        {
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
    }

    public class LCGWrapper
    {
        LCG[] generators = new LCG[4];
        uint[] seeds = new uint[4];
        BinOperations bOp = new BinOperations();

        public void CheckSeed(string blockIn,  osfun)
        {
            string C = "ОТВЕТСТВЕННЫЙ_ПОДХОД";
            string[] T = new string[4];
            for (int i = 0; i < 4; i++)
            {
                T[i] = C.Substring(i * 4, 4);
            }
            for (int j = 0; j < 3; j++)
            {
                for (int i = j+1; i < 4; i++)
                {
                    if (T[i] == T[j])
                    {
                        T[i] = osfun
                    }
                }
            }
        }

        public void Wrap_CHCLCGM_Next(bool flag, uint stateIn, uint seedIn, Func<uint> osfun, uint[][] setIn)
        {
             int[], string output;
            string stream = "";
            uint check = 0;
            string seed = "";
            List<ulong[]> state = new List<ulong[]>();
            LCGwithHC hcLCG = new LCGwithHC(new LCG());
            Converter converter = new Converter();

            if (flag)
            {
                for (int i = 0; i < 4; i++)
                {
                    state.Add(bOp.SeedToNums()); 
                }
            }
            else if (!flag)
            {

            }
            for (int j = 0; j < 4; j++)
            {
                long tmp = 0;
                int sign = 0;
                for (int i = 0; i < 4; i++)
                {
                    ulong[] T = hcLCG.Next(state[i], setIn);
                    state[i] = T[1];
                    tmp = (1048576 + sign * (uint)T[0] + tmp) % 1048576;
                    sign *= -1;
                }
                stream += converter.ConvertNumbToBlock(tmp);
            }
            output.Append([stream, state]);
        }

        //public uint[] GenerateCodes()
        //{
            
        //}
    }

    //public class LCG_Modified() : LCGwithHC
    //{
    //    public bool SeedCheck(List<Char> seed)
    //    {
    //        string C = 
    //    }

    //    public string InitLCGShift()
    //    {
    //        return;
    //    }
    //}
}
