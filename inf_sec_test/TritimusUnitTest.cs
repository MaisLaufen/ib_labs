using inf_sec;
using inf_sec.tritimus_encode;

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
            var tritimus = new PolyTritimus(origAlphabet, SHIFT);
            const string K = "АББАТ_ТРИТИМУС";
            const string IN = "ОТКРЫТЫЙ_ТЕКСТ";
            var res = tritimus.encryptPolyTritimus(IN, K);
            var res1 = tritimus.decryptPolyTritimus(res, K);
            Assert.Equal(IN, res1);
        }

        [Theory]
        //[InlineData("ЗВЁЗДНАЯ_НОЧЬ", "БЛОК", 11, "МФЙУ")]
        [InlineData("ЗВЁЗДНАЯ_НОЧЬ", "БРО", 11, "input_error")]
        public void EncryptSBlockTritimus(string key, string input, int jin, string expected)
        {
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);
            var res = tritimus.encryptSBlockTritimus(input, key, jin);
            Assert.Equal(expected, res);
        }
        
        [Theory]
        //[InlineData("ЗВЁЗДНАЯ_НОЧЬ", "МФЙУ", 11, "БЛОК")]
        [InlineData("ЗВЁЗДНАЯ_НОЧЬ", "МФЙУ", 3, "БКОЙ")]    
        [InlineData("ЗВЁЗДНАЯ_НОЧЬ", "input_error", 11, "input_error")]
        public void DecryptSBlockTritimus(string key, string input, int jin, string expected)
        {
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);
            var res = tritimus.decryptSBlockTritimus(input, key, jin);
            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData("ЗВЁЗДНАЯ_НОЧЬ", "БЛОК", 3 )]
        [InlineData("ФЫВЦУ", "БЛОК", 5 )]
        public void EncryptDecryptSBlockTritimus(string key, string input, int jin)
        {
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);
            var encrypt = tritimus.encryptSBlockTritimus(input, key, jin);
            var decrypt = tritimus.decryptSBlockTritimus(encrypt, key, jin);
            Assert.Equal(input, decrypt);
        }

        [Theory]
        [InlineData("ГОРАЦИО", "АТОЛ", 2, "ЬООЫ")]
        [InlineData("ГОРАЦИО", "АТОЛ", 3, "АУВО")]
        public void EncryptImproveBlock(string key, string input, int jin, string expected)
        {
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);
            var res = tritimus.encryptImproveBlock(input, key, jin);
            Assert.Equal(expected, res);
        }
        
        [Theory]
        [InlineData("ГОРАЦИО", "ЬООЫ", 2, "АТОЛ")]
        [InlineData("ГОРАЦИО", "АУВО", 3, "АТОЛ")]
        [InlineData("ГОРАЦИО", "ЬООЫ", 3, "ЬТ_Л")]
        public void DecryptImproveBlock(string key, string input, int jin, string expected)
        {
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);
            var res = tritimus.decryptImproveBlock(input, key, jin);
            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData("ТОРК", "РОЗА", "ЭЖМБ")]
        [InlineData("РОКТ", "РОЗА", "ЭЖЬЦ")]
        public void EncryptSTritimusM(string input, string k, string expected)
        {
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);
            var res = tritimus.encryptSTritimusM(input, k, 0);
            Assert.Equal(expected, res);
        }


        [Fact]
        public void generalTest()
        {
            var block_in1 = "КРОТ";
            var block_in2 = "КРУТ";
            var block_in3 = "ТОРК";
            var block_in4 = "РОКТ";
            var block_in5 = "ГРОТ";

            var key1 = "РОЗА";
            var key2 = "ЯДРО";
            
            var tritimus = new SBlockTritimus(origAlphabet, SHIFT);

            // Последовательность операций
            var tst1 = tritimus.encryptSBlockTritimus(block_in1, key1, 0);
            var tst12 = tritimus.encryptSBlockTritimus(tst1, key2, 0);
            var tst122 = tritimus.decryptSBlockTritimus(tst12, key2, 0);
            var tst1221 = tritimus.decryptSBlockTritimus(tst122, key1, 0);
            Assert.Equal(block_in1, tst1221);
            
            var tst121 = tritimus.decryptSBlockTritimus(tst12, key1, 0);
            var tst1212 = tritimus.decryptSBlockTritimus(tst121, key2, 0);
            Assert.Equal("КШЯС", tst1212);

            // Последовательность операций (с доп операцией)
            var tste1 = tritimus.encryptSTritimusM(block_in1, key1, 0);
            var tste12 = tritimus.encryptSTritimusM(tste1, key2, 0);
            var tste122 = tritimus.decryptSTritimusM(tste12, key2, 0);
            var tste1221 = tritimus.decryptSTritimusM(tste122, key1, 0);
            Assert.Equal(block_in1, tste1221); // dont pass
            
            var tste121 = tritimus.decryptSTritimusM(tste12, key1, 0);
            var tste1212 = tritimus.decryptSTritimusM(tste121, key2, 0);
            Assert.Equal("РХДС", tste1212); // dont pass
            
            // Перестановка в блоке
            tst1 = tritimus.encryptSBlockTritimus(block_in1, key1, 0);
            var tst2 = tritimus.encryptSBlockTritimus(block_in3, key1, 0);
            var tst3 = tritimus.encryptSBlockTritimus(block_in4, key1, 0);
            Assert.Equal("ФЕЖЫ", tst1);
            Assert.Equal("ЫЖЕФ", tst2);
            Assert.Equal("ЕЖФЫ", tst3);
            
            // Перестановка в блоке (с доп операцией)
            tste1 = tritimus.encryptSTritimusM(block_in1, key1, 0);
            var tste2 = tritimus.encryptSTritimusM(block_in3, key1, 0);
            var tste3 = tritimus.encryptSTritimusM(block_in4, key1, 0);
            Assert.Equal("ЭЕМЗ", tste1);
            Assert.Equal("ЭЖМБ", tste2);
            Assert.Equal("ЭЖЬЦ", tste3);
            
            // Замена символа в блоке
            tst1 = tritimus.encryptSBlockTritimus(block_in1, key1, 0);
            tst2 = tritimus.encryptSBlockTritimus(block_in2, key1, 0);
            tst3 = tritimus.encryptSBlockTritimus(block_in5, key1, 0);
            Assert.Equal("ФЕЖЫ", tst1);
            Assert.Equal("ФЕЬЫ", tst2);
            Assert.Equal("МЕЖЫ", tst3);
            
            /* В методичке скипнуто (код из цезаря в блоке тритимуса)  */
            //tste1 = tritimus.encryptSTritimusM(block_in1, key1, 0);
            //tste2 = tritimus.encryptSTritimusM(block_in2, key1, 0);
            //tste3 = tritimus.encryptSTritimusM(block_in5, key1, 0);
            //Assert.Equal("ЭЕМЗ", tste1);
            //Assert.Equal("СЕБЭ", tste2);
            //Assert.Equal("ФЕМЗ", tste3);
        }
    }
}
