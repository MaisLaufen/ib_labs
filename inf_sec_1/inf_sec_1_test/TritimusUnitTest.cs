using tritimus;

namespace inf_sec_1_test
{
    public class TritimusUnitTest
    {
        readonly string origAlphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_";
        const int SHIFT = 8;
        [Fact]
        public void GetModifiedAlphabet_K1()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            const string KEY = "ДИНОЗАВР_ЗАУРОПОД";
            const string OUT = "ДИНОЗАВР_УПБГЕЖЙКЛМСТФХЦЧШЩЫЬЭЮЯ";
            var res = tritimus.getModifiedAlphabet(KEY);
            Assert.Equal(OUT, new string(res.ToArray()));
        }
        [Fact]
        public void GetModifiedAlphabet_K2()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            const string KEY = "ГАМЕЛЬНСКИЙ_АНТИКВАР";
            const string OUT = "ГАМЕЛЬНСКИЙ_ТВРБДЖЗОПУФХЦЧШЩЫЭЮЯ";
            var res = tritimus.getModifiedAlphabet(KEY);
            Assert.Equal(OUT, new string(res.ToArray()));
        }
        [Fact]
        public void GetModifiedAlphabet_K3()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);
            const string KEY = "ГАРРИ_ПОТЕР_И_ФИЛОСОФСИКЙ_КАМЕНЬ";
            const string OUT = "ГАРИ_ПОТЕФЛСКЙМНЬБВДЖЗУХЦЧШЩЫЭЮЯ";
            var res = tritimus.getModifiedAlphabet(KEY);
            Assert.Equal(OUT, new string(res.ToArray()));
        }

        [Fact]
        public void EncryptTheWord_GolovnoyOfficeWithChernosotency()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);

            const string IN = "ГОЛОВНОЙ_ОФИС";
            const string K1 = "ЧЕРНОСОТЕНЦЫ";
            const string OUT = "МГЬГЛВГШЦГ_ХД";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.encryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
        }
        [Fact]
        public void EncryptTheWord_GolovnoyOfficeWithABVGD()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);

            const string IN = "ГОЛОВНОЙ_ОФИС";
            const string K1 = "АБВГД";
            const string OUT = "ЛЦУЦКХЦСЗЦЭРЩ";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.encryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
        }

        [Fact]
        public void EncryptTheWord_PoldenWithO()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);

            const string IN = "ПОЛДЕНЬ";
            const string K1 = "О";
            const string OUT = "ЧЗФМНЦВ";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.encryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
        }

        [Fact]
        public void EncryptTheWord_PoldenWithVersal()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);

            const string IN = "ПОЛДЕНЬ";
            const string K1 = "ВЕРСАЛЬ";
            const string OUT = "ЩШЙОДЧК";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.encryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
        }
        [Fact]
        public void EncryptTheWord_KramolaWithO()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);

            const string IN = "КРАМОЛА";
            const string K1 = "О";
            const string OUT = "УШИХЗФИ";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.encryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
        }
        [Fact]
        public void EncryptTheWord_KramolaWithVersal()
        {
            var tritimus = new Tritimus(origAlphabet, SHIFT);

            const string IN = "КРАМОЛА";
            const string K1 = "ВЕРСАЛЬ";
            const string OUT = "ХЖИЦШЙИ";
            var alph = tritimus.getModifiedAlphabet(K1);
            var res = tritimus.encryptTheWord(IN, alph);
            Assert.Equal(OUT, res);
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
    }
}
