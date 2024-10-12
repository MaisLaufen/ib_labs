    using inf_sec.lcg_generator;
    using inf_sec.tritimus_encode;

    namespace inf_sec
{
    

    // public class LCGWrapper
    // {
    //     LCG[] generators = new LCG[4];
    //     uint[] seeds = new uint[4];
    //     BinOperations bOp = new BinOperations();

    //     public void CheckSeed(string blockIn,  osfun)
    //     {
    //         string C = "ОТВЕТСТВЕННЫЙ_ПОДХОД";
    //         string[] T = new string[4];
    //         for (int i = 0; i < 4; i++)
    //         {
    //             T[i] = C.Substring(i * 4, 4);
    //         }
    //         for (int j = 0; j < 3; j++)
    //         {
    //             for (int i = j+1; i < 4; i++)
    //             {
    //                 if (T[i] == T[j])
    //                 {
    //                     T[i] = osfun
    //                 }
    //             }
    //         }
    //     }

    //     public void Wrap_CHCLCGM_Next(bool flag, uint stateIn, uint seedIn, Func<uint> osfun, uint[][] setIn)
    //     {
    //          int[], string output;
    //         string stream = "";
    //         uint check = 0;
    //         string seed = "";
    //         List<ulong[]> state = new List<ulong[]>();
    //         LCGwithHC hcLCG = new LCGwithHC(new LCG());
    //         Converter converter = new Converter();

    //         if (flag)
    //         {
    //             for (int i = 0; i < 4; i++)
    //             {
    //                 state.Add(bOp.SeedToNums()); 
    //             }
    //         }
    //         else if (!flag)
    //         {

    //         }
    //         for (int j = 0; j < 4; j++)
    //         {
    //             long tmp = 0;
    //             int sign = 0;
    //             for (int i = 0; i < 4; i++)
    //             {
    //                 ulong[] T = hcLCG.Next(state[i], setIn);
    //                 state[i] = T[1];
    //                 tmp = (1048576 + sign * (uint)T[0] + tmp) % 1048576;
    //                 sign *= -1;
    //             }
    //             stream += converter.ConvertNumbToBlock(tmp);
    //         }
    //         output.Append([stream, state]);
    //     }

    //     //public uint[] GenerateCodes()
    //     //{
            
    //     //}
    // }

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
