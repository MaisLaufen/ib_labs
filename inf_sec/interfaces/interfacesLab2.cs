namespace inf_sec
{
    interface IConverter
    {
        ulong ConvertBlockToNumb(List<Char> block);
        List<Char> ConvertNumbToBlock(ulong number);
        uint[] ConverNumbToBinaryArray(ulong number);
        ulong ConverBinaryArrayToNumb(uint[] binArray);
    }

    interface ILCG
    {
        ulong next();
    }

    interface ILCGwithHC
    {
        public ulong next();
    }

    interface ILCGWrapper
    {
        public string GenerateCodes();
        public void Init(string seeds, uint[][] coefficients);
    }

    interface ILCG_Modified
    {
        public bool SeedCheck(List<Char> seed);
        public string InitLCGShift();
    }
}
