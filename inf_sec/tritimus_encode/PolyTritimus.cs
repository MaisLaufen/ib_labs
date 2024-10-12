namespace inf_sec.tritimus_encode {
  public class PolyTritimus : Tritimus, IPolyTritimus
    {
        int _alphLen = 0;

        public PolyTritimus(string originAlphabet, int shift) : base(originAlphabet, shift)
        {
            _alphLen = originAlphabet.Length;
        }

        public string encryptPolyTritimus(string word, string key)
        {
            int wordLen = word.Length;
            char[] encryptedWord = new char[wordLen];
            List<char> modifAlph = getModifiedAlphabet(key);

            for (int i = 0; i < wordLen; i++)
            {
                encryptedWord[i] = encryptTheChar(word[i], modifAlph);
                modifAlph = shiftTable(modifAlph, ((i + 1) % wordLen) % _alphLen);
            }

            return new string(encryptedWord);
        }

        public string decryptPolyTritimus(string word, string key)
        {
            int wordLen = word.Length;
            char[] decryptedWord = new char[wordLen];
            List<char> modifAlph = getModifiedAlphabet(key);

            for (int i = 0; i < wordLen; i++)
            {
                decryptedWord[i] = decryptTheChar(word[i], modifAlph);
                modifAlph = shiftTable(modifAlph, ((i + 1) % wordLen) % _alphLen);
            }

            return new string(decryptedWord);
        }
    }
}