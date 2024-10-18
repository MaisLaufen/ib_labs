namespace inf_sec.tritimus_encode {
    
  public class Tritimus : IEncoder
      {
          protected string _originalAlhnabet;
          protected int _alphabetLength;
          protected int _shift;

          public Tritimus(string originAlphabet, int shift)
          {
              _originalAlhnabet = originAlphabet;
              _alphabetLength = originAlphabet.Length;
              _shift = shift;
          }

          public List<char> getModifiedAlphabet(string key)
          {
              string result = new string(key.Where(c => _originalAlhnabet.Contains(c)).ToArray());
              List<char> newAlph = (result + _originalAlhnabet).Distinct().ToList();
              return newAlph;
          }

          public char encryptTheChar(char letter, List<char> alphabet)
          {
              int index = alphabet.IndexOf(letter);
              index = (index + _shift) % _alphabetLength;
              return alphabet[index];
          }

          public char decryptTheChar(char encryptedChar, List<char> alphabet)
          {
              int index = alphabet.IndexOf(encryptedChar);
              index = (index - _shift) % _alphabetLength;
              if (index < 0) index += _alphabetLength;
              return alphabet[index];
          }

          public string encryptTheWord(string word, List<char> alphabet)
          {
              char[] encryptedWord = new char[word.Length];
              for (int i = 0; i < word.Length; i++)
              {
                  encryptedWord[i] = encryptTheChar(word[i], alphabet);
              }

              return new string(encryptedWord);
          }

          public string decryptTheWord(string word, List<char> alphabet)
          {
              char[] decryptedWord = new char[word.Length];
              for (int i = 0; i < word.Length; i++)
              {
                  decryptedWord[i] = decryptTheChar(word[i], alphabet);
              }

              return new string(decryptedWord);
          }

          public List<char> shiftTable(List<char> alphabet, int k)
          {
              if (k <= 0) return alphabet;
              string alpString = String.Join("", alphabet);

              var s = alpString[^1];
              var head = alpString.Substring(0, k - 1);
              var tale = alpString.Substring(k - 1, 32 - k);

              return String.Concat(head, s, tale).ToList();
          }
      }
}