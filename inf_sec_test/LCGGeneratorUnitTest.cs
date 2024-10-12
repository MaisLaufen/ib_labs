using System.Runtime.InteropServices;
using inf_sec.lcg_generator;

namespace inf_sec_test;

public class LCGGeneratorUnitTest
{
    [Theory]
    [InlineData("АБВГ", "34916")]
    [InlineData("_ЯЗЬ", "32028")]
    [InlineData("ЯЯЯЯ", "1048575")]
    public void Block2NumTest(string block, string expected)
    {
        var convertor = new Converter();
        var res = convertor.ConvertBlockToNumb(block.ToCharArray().ToList()).ToString();
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("ЛУЛУ", new uint[] { 723482, 8677, 983609 }, "КМЖТ")]
    [InlineData("ЯВОР", new uint[] { 723482, 8677, 983609 }, "ХЕЖР")]
    [InlineData("ЛУЛУ", new uint[] { 357630, 8971, 948209 }, "НЕШЬ")]
    public void LCGNextTest(string seed, uint[] setIn, string expected)
    {
        var convertor = new Converter();
        var stateIn = convertor.ConvertBlockToNumb(seed.ToCharArray().ToList());
        var lcg = new LCG(stateIn, setIn);
        var res = convertor.ConvertNumbToBlock(lcg.next());
        Assert.Equal(expected, string.Join("", res));
    }

    [Theory]
    [InlineData("ЛУЛУ", new uint[] { 723482, 8677, 983609 }, "КМЖТ ОГЫЕ ЛЭЮА ЦЫЧР Ж_ОР КОЭЖ ЩЛЧЖ ЙШДЛ МЧЮЬ ЧНЫК")]
    [InlineData("ЯВОР", new uint[] { 723482, 8677, 983609 }, "ХЕЖР ЭДАР КББЛ РАГЗ ЕГЛ_ ЫОТЩ ЙГНЧ ВЕЦ_ ШШУЯ ЕЮЫЦ")]
    [InlineData("ЛУЛУ", new uint[] { 357630, 8971, 948209 }, "НЕШЬ ДОЕМ АЦШП СЙЩН ЛХРН _ДДШ ЧЮЮИ ОУВУ ПЛЬУ МФРА")]
    public void LCGNextFullArrayTest(string seed, uint[] setIn, string expected)
    {
        var set = new List<List<char>>();
        var convertor = new Converter();
        var lcg = new LCG(convertor.ConvertBlockToNumb(seed.ToCharArray().ToList()), setIn);
        for (int i = 0; i <= 9; i++)
        {
            var res = convertor.ConvertNumbToBlock(lcg.next());
            set.Add(res);
        }

        Assert.Equal(expected, set.Select(x => string.Join("", x)).Aggregate((x, y) => x + " " + y));
    }

    [Theory]
    [InlineData(1231, 723482, 0, 1231)]
    [InlineData(1231, 723482, 20, 723482)]
    [InlineData(1231, 723482, 10, 1562)]
    public void ComposeNumTest(ulong num1, ulong num2, ulong n, ulong expected)
    {
        var res = Utils.ComposeNum(num1, num2, n);
        Assert.Equal(expected, res);
    }

    [Fact]
    public void HC_LCGNextTest()
    {
        
        string[] seed1 = { "АПЧХ", "Ч_ОК", "ШУРА" };
        string expected = "ТЖЧТ";
        
        var convertor = new Converter();
        ulong[] seedLong = new ulong[seed1.Length];
        for (int i = 0; i < seed1.Length; i++)
        {
            seedLong[i] = convertor.ConvertBlockToNumb(seed1[i].ToCharArray().ToList());
        }

        var coefficients = Utils.getDefaultCoefficients();
        HCLCG hclcg = new(new(seedLong[0], coefficients[0]), new(seedLong[1], coefficients[1]), new(seedLong[2], coefficients[2]));
        
        var tmp = hclcg.next();
        var res = convertor.ConvertNumbToBlock(tmp);
        Assert.Equal(expected, new string(res.ToArray()));
    }
    
    [Theory]
    [InlineData("ВАСЯ", "____", 1, "ЖОЧБ")]
    [InlineData("ВАСЯ", "____", 2, "ЯВЗН")]
    [InlineData("ВАСЯ", "____", 3, "_АТЦ")]
    [InlineData("ВАСЯ", "____", 4, "ИЧПБ")]
    [InlineData("ВАСЯ", "____", 5, "МПЧН")]
    [InlineData("ВАСЯ", "____", 6, "ХМЛВ")]
    public void oneWayFuncSBlockTritimusTest(string block, string const_in, uint n, string expected)
    {
        var res = Utils.oneWayFuncSBlockTritimus(block, const_in, n);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData("ААААААААББББББББ", "ААААЦДЫДББББЮДЗГ")]
    [InlineData("ВВВВГГГГАААААААА", "ВВВВГГГГААААЮДЗГ")]
    [InlineData("АААААААААААААААА", "ААААЦДЫДЖЫЗЦЫЦХД")]
    [InlineData("________________", "____С_ЫДЯЦЯОЩЛСЕ")]
    public void CheckSeedTest(string block, string expected)
    {
        Assert.Equal(expected, Utils.CheckSeed(block) ); 
    }

    [Fact]
    public void LCGWrapperTest()
    {
        List<string> expected = new List<string>()
        {
            "ЬЕШЮШ_ЗЯЧЖ_ВЖЙЕГ",
            "ГУЬЙЭЬДЫЭУ_ЮХЛДТ",
            "ДИЛ_АСЩ_ВЧЯП_ИЕХ",
            "РДАГАЭТУП_ОББЛВК",
            "УЙЗ_ЩШАЛНЙЫХЬРГИ",
            "ЛЬЭШУУСЦБНЗЗЫИАЫ",
            "ГЦЕЖЫШВПГЙМЛСРШД",
            "ЙОИУЛИЙЬОГСВМЙЬС",
            "ПТЫТЗГЛДЕЕФЙЕДТВ"
        };
        
        var defaultCoefficients = Utils.getDefaultCoefficients();
        var lcg = new HCLCGWrapper();
        lcg.Init("АБВГДЕЖЗИЙКЛМНОП", defaultCoefficients);
        List<string> result = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            var res = lcg.GenerateCodes();
            result.Add(res);
        }
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("ААААББББВВВВГГГГ", 1, "ЯУЯЖРЦОДЦФЮМАЧХЭ")]
    [InlineData("ААААББББВВВВГГГГ", 2, "ЮИВПЗЩИРХЖФВЛРПУ")]
    [InlineData("ААААББББВВВВГГГГ", 3, "ЯАЛМБЧПЖЕИКЧУЙШ_")]
    [InlineData("ВВВВГГГГААААББББ", 1, "ТМТЛЦПВЗТЦШФПДОЖ")]
    [InlineData("ВВВВГГГГААААББББ", 2, "СДИБЕСТЛКПЬПУНАЛ")]
    [InlineData("ВВВВГГГГААААББББ", 3, "ЧМТЮЩЧППЫЧЕАРЮЙЖ")]
    [InlineData("ААААААААББББББББ", 1, "ИХЯМУНКТЕЭГЬФЕЦТ")]
    [InlineData("ААААААААББББББББ", 2, "ЦТЮЕОБМЖХХЦИЫХЦХ")]
    [InlineData("ААААААААББББББББ", 3, "ЗМ_ЩСФЕПДИХХСМЮД")]
    public void HCLCGMWrapperTest(string seed, int steps, string expected)
    {
        var hclcgm = new HCLCGMWrapper();
        hclcgm.Init(seed, Utils.getDefaultCoefficients());
        string res = "";
        for (int i = 0; i < steps; i++)
        {
            res = hclcgm.GenerateCodes();
        }
        Assert.Equal(expected, res);
        
    }
}