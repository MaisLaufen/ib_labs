using inf_sec;

namespace inf_sec_test
{
    public class AlphabetOperationTests
    {
        [Theory]
        [InlineData('О', 15)]
        [InlineData('Ж', 7)]
        public void GetBinaryCode_ShouldBeEqual(char input, uint expected)
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getBinaryCode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(7, 'Ж')]
        [InlineData(14, 'Н')]
        public void GetChar_ShouldBeEqual(uint input, char expected)
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getChar(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Я', 'Ж', 'Е')]
        [InlineData('Е', 'Ж', 'М')]
        public void GetSum_ShouldBeEqual(char char1, char char2, char expected)
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getSum(char1, char2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Е', 'Ж', 'Я')]
        [InlineData('Е', 'М', 'Ж')]
        public void GetDiff_ShouldBeEqual(char char1, char char2, char expected)
        {
            var alphabet = new AlphabetOperations();
            var result = alphabet.getDiff(char1, char2);
            Assert.Equal(expected, result);
        }
    }
}