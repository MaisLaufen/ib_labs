using alphabet;
namespace inf_sec_1_test
{
    public class AlphabetOperationTests
    {
        [Fact]
        public void GetBinaryCode_O_ShouldBeEqual_15()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getBinaryCode('Î');
            Assert.Equal((uint)(15), result);
        }
        [Fact]
        public void GetBinaryCode_J_ShouldBeEqual_7()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getBinaryCode('Æ');
            Assert.Equal((uint)(7), result);
        }
        [Fact]
        public void GetChar_7_ShouldBeEqual_J ()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getChar(7);
            Assert.Equal('Æ', result);
        }
        [Fact]
        public void GetChar_14_ShouldBeEqual_H()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getChar(14);
            Assert.Equal('Í', result);
        }
        [Fact]
        public void GetSum_YA_Plus_J_ShouldBeEqual_E()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getSum('ß','Æ');
            Assert.Equal('E', result);
        }
        [Fact]
        public void GetSum_E_Plus_J_ShouldBeEqual_M()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getSum('Å', 'Æ');
            Assert.Equal('Ì', result);
        }
        [Fact]
        public void GetDiff_E_Minus_J_ShouldBeEqual_YA()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getDiff('Å', 'Æ');
            Assert.Equal('ß', result);
        }
        [Fact]
        public void GetDiff_E_Minus_M_ShouldBeEqual_J()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getDiff('Å', 'Ì');
            Assert.Equal('Æ', result);
        }
    }
}