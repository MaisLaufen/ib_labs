namespace inf_sec.tritimus_encode
{
    //class Symbol
    //{
    //    public char symbol;
    //    public string bits;
    //    public Symbol(char symbol, string bits)
    //    {
    //        this.symbol = symbol;
    //        this.bits = bits;
    //    }
    //}

    public class AlphabetOperations : IAlphabetOperations
    {
        static string alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_";
        Tritimus tritimus = new Tritimus(alphabet, 8);

        public string getAlphabet()
        {
            return alphabet;
        }
        
        public uint getBinaryCode(char letter) 
        {
            for (uint i = 0; i < alphabet.Length; i++)
            {
                if (i == 31) return 0;
                if (alphabet[(int)i] == letter) return (i+1);
            }
            return 0;
        }

        public char getChar(uint binaryCode)
        {
            if (binaryCode == 0) return '_';
            return alphabet[(int)binaryCode-1];
        }
        
        public char getSum(char x, char y)
        {
            int xIndex = alphabet.IndexOf(x);
            int yIndex = alphabet.IndexOf(y);
            int sum = (xIndex + 1) + (yIndex + 1);
            int index = sum % alphabet.Length;
            return alphabet[index-1];
        }

        public char getDiff(char x, char y)
        {
            int xIndex = alphabet.IndexOf(x);
            int yIndex = alphabet.IndexOf(y);
            int diff = (xIndex - yIndex);
            if (diff < 0) return alphabet[alphabet.Length + diff - 1];
            return alphabet[xIndex - diff + 1];
        }

        public uint[] textToArray(string txtIn)
        {
            uint[] output = new uint[txtIn.Length];
            for (int i = 0; i < txtIn.Length; i++)
            {
                output[i] = getBinaryCode(txtIn[i]);
            }
            return output;
        }

        public string arrayToText(uint[] arrIn)
        {
            char[] output = new char[arrIn.Length];
            for (int i = 0; i < arrIn.Length; i++)
            {
                output[i] = getChar(arrIn[i]);
            }
            return new string(output);
        }
    }
}