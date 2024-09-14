using inf_sec;
namespace inf_sec_test
{
    public class TritimusUnitTest
    {
        readonly string origAlphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_";
        private const int SHIFT = 8;

        [Theory]
        [InlineData("ДИНОЗАВР_ЗАУРОПОД", "ДИНОЗАВР_УПБГЕЖЙКЛМСТФХЦЧШЩЫЬЭЮЯ")]
        [InlineData("ГАМЕЛЬНСКИЙ_АНТИКВАР", "ГАМЕЛЬНСКИЙ_ТВРБДЖЗОПУФХЦЧШЩЫЭЮЯ")]
        [InlineData("ГАРРИ_ПОТЕР_И_ФИЛОСОФСИКЙ_КАМЕНЬ", "ГАРИ_ПОТЕФЛСКЙМНЬБВДЖЗУХЦЧШЩЫЭЮЯ")]
        public void GetModifiedAlphabet(string key, string expected)
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            var res = tritimus.getModifiedAlphabet(key);
            Assert.Equal(expected, new string(res.ToArray()));
        }

        [Theory]
        [InlineData("ГОЛОВНОЙ_ОФИС", "ЧЕРНОСОТЕНЦЫ", "МГЬГЛВГШЦГ_ХД")]
        [InlineData("ГОЛОВНОЙ_ОФИС", "АБВГД", "ЛЦУЦКХЦСЗЦЭРЩ")]
        [InlineData("ПОЛДЕНЬ", "О", "ЧЗФМНЦВ")]
        [InlineData("ПОЛДЕНЬ", "ВЕРСАЛЬ", "ЩШЙОДЧК")]
        [InlineData("КРАМОЛА", "О", "УШИХЗФИ")]
        [InlineData("КРАМОЛА", "ВЕРСАЛЬ", "ХЖИЦШЙИ")]
        public void EncryptTheWord(string input, string key, string expected)
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            var alph = tritimus.getModifiedAlphabet(key);
            var res = tritimus.encryptTheWord(input, alph);
            Assert.Equal(expected, res);
        }

        [Fact]
        public void DecryptTheWord_GolovnoyOffice()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            const string OUT = "ГОЛОВНОЙ_ОФИС";
            const string K1 = "АБВГД";
            const string IN = "ЛЦУЦКХЦСЗЦЭРЩ";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.decryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
        }

        [Theory]
        [InlineData("А", 0, "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_")]
        [InlineData("А", 1, "_АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ")]
        [InlineData("А", 2, "А_БВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ")]
        [InlineData("А", 3, "АБ_ВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ")]
        [InlineData("А", 4, "АБВ_ГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ")]
        public void ShiftTable(string key, int step, string expected)
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            var alph = tritimus.getModifiedAlphabet(key);
            var res = tritimus.shiftTable(alph, step);
            Assert.Equal(expected, new string(res.ToArray()));
        }

        [Fact]
        public void EncryptAndDecryptPolyTritimus()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            const string K = "АББАТ_ТРИТИМУС";
            const string IN = "ОТКРЫТЫЙ_ТЕКСТ";
            var res = tritimus.encryptPolyTritimus(IN, K);
            var res1 = tritimus.decryptPolyTritimus(res, K);
            Assert.Equal(IN, res1);
        }

        [Theory]
        [InlineData("ЗВЕЗДНАЯ_НОЧЬ", "БЛОК", 11, "МФЙУ")]
        [InlineData("ЗВЕЗДНАЯ_НОЧЬ", "БРО", 11, "input_error")]
        [InlineData("ЗВЕЗДНАЯ_НОЧЬ", "МФЙУ", 11, "БЛОК")]
        [InlineData("ЗВЕЗДНАЯ_НОЧЬ", "МФЙУ", 3, "БКОЙ")]
        [InlineData("ЗВЕЗДНАЯ_НОЧЬ", "input_error", 11, "input_error")]
        public void EncryptSBlockTritimus(string key, string input, int jin, string expected)
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            var res = tritimus.encryptSBlockTritimus(input, key, jin);
            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData("ГОРАЦИО", "АТОЛ", 2, "ЬООЫ")]
        [InlineData("ГОРАЦИО", "АТОЛ", 3, "АУВО")]
        [InlineData("ГОРАЦИО", "ЬООЫ", 2, "АТОЛ")]
        [InlineData("ГОРАЦИО", "АУВО", 3, "АТОЛ")]
        [InlineData("ГОРАЦИО", "ЬООЫ", 3, "ЬТ_Л")]
        public void EncryptImproveBlock(string key, string input, int jin, string expected)
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            var res = tritimus.encryptImproveBlock(input, key, jin);
            Assert.Equal(expected, res);
        }

        [Fact]
        public void EncryptSTritimusM()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            const string K = "РОЗА";
            const string OUT = "ЭЖМБ";
            const string IN = "ТОРК";
            var res = tritimus.encryptSTritimusM(IN, K, 0);
            Assert.Equal(OUT, res);
        }
    }
}
