namespace inf_sec.lcg_generator;

public class HCLCGWrapper(): ILCGWrapper
{
    protected HCLCG[] generators = new HCLCG[4];
    private Converter _converter = new Converter();
    
    public virtual void Init(string seed, uint[][] coefficients)
    {
        if (seed.Length != 16 || coefficients.Length != 3) throw new ArgumentException("invalid_input");
        ulong[] seeds;
        for (int i = 0; i < 4; i++)
        {
            var tmp = Utils.MakeSeeds(seed.Substring(i * 4, 4));
            seeds = Utils.SeedToNums(tmp);
            generators[i] = new(new(seeds[0], coefficients[0]), new(seeds[1], coefficients[1]), new(seeds[2], coefficients[2]));
        }
    }

    public string GenerateCodes()
    {
        string res = "";
        for (int j = 0; j < 4; j++)
        {
            int sign = 1;
            int tmp = 0;
            for (int i = 0; i < 4; i++, sign *= -1)
                tmp = (sign * (int)generators[i].next() + 1048576 + tmp) % + 1048576;
            
            res += string.Join("", _converter.ConvertNumbToBlock((ulong)tmp));
        }

        return res;
    }
    
}