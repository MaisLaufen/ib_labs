using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inf_sec
{
    interface IConverter
    {
        ulong ConvertBlockToNumb(List<Char> block);
        List<Char> ConvertNumbToBlock(long number);
        uint[] ConverNumbToBinaryArray(ulong number);
        ulong ConverBinaryArrayToNumb(uint[] binArray);
    }

    interface ILCG
    {
        uint next();
    }

    interface ILCGwithHC
    {
        public string next();
        public string oneWayFuncSBlockTritimus(string block, string constant, uint roundAmount);
        public uint CountUnityBits();
    }

    interface ILCGWrapper
    {
        public uint[] GenerateCodes();
    }

    interface ILCG_Modified
    {
        public bool SeedCheck(List<Char> seed);
        public string InitLCGShift();
    }
}
