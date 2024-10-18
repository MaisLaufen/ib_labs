using inf_sec.tritimus_encode;

namespace inf_sec.lcg_generator;

public class HCLCG(LCG first, LCG second, LCG control) : ILCG
{
    private LCG _first = first;
    private LCG _second = second;
    private LCG _control = control;
    private ulong _state;
    
    public ulong next()
    {
        var num1 = _first.next();
        var num2 = _second.next();
        var control = _control.next();
        
        var n = Utils.CountUnityBits(control);
        _state = control % 2 == 0 ? Utils.ComposeNum(num1, num2, n) : Utils.ComposeNum(num2, num1, n);
        
        return _state;
    }

}