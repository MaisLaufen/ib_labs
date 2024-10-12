namespace inf_sec.lcg_generator;

public class HCLCGMWrapper: HCLCGWrapper
{
    public override void Init(string seed, uint[][] coefficients)
    {
        base.Init(Utils.CheckSeed(seed), coefficients);
        for (int i = 1; i < 4; i++)
        {
            if (i > 0)
                for (int j = 0; j <= i; j++)
                    generators[i].next();
        }
    }
}