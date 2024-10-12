namespace inf_sec.lcg_generator;

public class LCG(ulong seed, uint[] coefficients) : ILCG
{
    private ulong _state = seed;

    public ulong next()
    {
        _state = (coefficients[0] * _state + coefficients[1]) % coefficients[2];
        return _state;
    }
}