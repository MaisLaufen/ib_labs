using inf_sec;

namespace inf_sec_test
{
    public class AlphabetOperationTests
    {
        [Fact]
        public void GetBinaryCode_O_ShouldBeEqual_15()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getBinaryCode('О');
            Assert.Equal((uint)(15), result);
        }
        [Fact]
        public void GetBinaryCode_J_ShouldBeEqual_7()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getBinaryCode('Ж');
            Assert.Equal((uint)(7), result);
        }
        [Fact]
        public void GetChar_7_ShouldBeEqual_J ()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getChar(7);
            Assert.Equal('Ж', result);
        }
        [Fact]
        public void GetChar_14_ShouldBeEqual_H()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getChar(14);
            Assert.Equal('Н', result);
        }
        [Fact]
        public void GetSum_YA_Plus_J_ShouldBeEqual_E()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getSum('Я','Ж');
            Assert.Equal('Е', result);
        }
        [Fact]
        public void GetSum_E_Plus_J_ShouldBeEqual_M()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getSum('Е', 'Ж');
            Assert.Equal('М', result);
        }
        [Fact]
        public void GetDiff_E_Minus_J_ShouldBeEqual_YA()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getDiff('Е', 'Ж');
            Assert.Equal('Я', result);
        }
        [Fact]
        public void GetDiff_E_Minus_M_ShouldBeEqual_J()
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getDiff('Е', 'М');
            Assert.Equal('Ж', result);
        }
    }
}