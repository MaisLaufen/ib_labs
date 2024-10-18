namespace inf_sec.tritimus_encode {
  public class PolyTritimus : Tritimus, IPolyTritimus
    {
        int _alphabetLength = 0;

        public PolyTritimus(string originAlphabet, int shift) : base(originAlphabet, shift)
        {
            _alphabetLength = originAlphabet.Length;
        }

        public string encryptPolyTritimus(string word, string key)
        {
            char[] encryptedWord = new char[word.Length];
            List<char> modifAlph = getModifiedAlphabet(key);

            for (int i = 0; i < word.Length; i++)
            {
                encryptedWord[i] = encryptTheChar(word[i], modifAlph);
                modifAlph = shiftTable(modifAlph, ((i + 1) % word.Length) % _alphabetLength);
            }

            return new string(encryptedWord);
        }

        public string decryptPolyTritimus(string word, string key)
        {
            char[] decryptedWord = new char[word.Length];
            List<char> modifAlph = getModifiedAlphabet(key);

            for (int i = 0; i < word.Length; i++)
            {
                decryptedWord[i] = decryptTheChar(word[i], modifAlph);
                modifAlph = shiftTable(modifAlph, ((i + 1) % word.Length) % _alphabetLength);
            }

            return new string(decryptedWord);
        }
    }
}