namespace inf_sec.tritimus_encode
{
    public class SBlockTritimus : PolyTritimus, ISBlockTritimus
    {
        int _alphLen = 0;

        public SBlockTritimus(string originAlphabet, int shift) : base(originAlphabet, shift)
        {
            _alphLen = originAlphabet.Length;
        }

        public string encryptSBlockTritimus(string blockIn, string keyIn, int jIn)
        {
            if (blockIn.Length != 4)
            {
                return "input_error";
            }

            List<char> keyTable = getModifiedAlphabet(keyIn);
            int jm = (jIn) % _alphLen;
            if (jm > 0)
            {
                for (int j = 1; j < jm; j++)
                {
                    keyTable = shiftTable(keyTable, j);
                }
            }

            string output = "";
            for (int i = 0; i < 4; i++)
            {
                char tmp = blockIn[i];
                int t = (jIn + i) % 32;
                keyTable = shiftTable(keyTable, t);
                int pos = keyTable.IndexOf(tmp);
                char csym = keyTable[(pos + 8) % 32];
                output += csym;
            }

            return output;
        }

        public string decryptSBlockTritimus(string blockIn, string keyIn, int jIn)
        {
            if (blockIn.Length != 4)
            {
                return "input_error";
            }

            List<char> keyTable = getModifiedAlphabet(keyIn);
            int jm = (jIn) % _alphLen;
            if (jm > 0)
            {
                for (int j = 1; j < jm; j++)
                {
                    keyTable = shiftTable(keyTable, j);
                }
            }

            string output = "";
            for (int i = 0; i < 4; i++)
            {
                char tmp = blockIn[i];
                int t = (jIn + i) % 32;
                keyTable = shiftTable(keyTable, t);
                int pos = keyTable.IndexOf(tmp);
                char csym = keyTable[(32 + pos - 8) % 32];
                output += csym;
            }

            return output;
        }

        public string encryptImproveBlock(string blockIn, string keyIn, int jIn)
        {
            string t = keyIn;
            while (jIn > t.Length - 4)
            {
                t += t;
            }

            AlphabetOperations op = new AlphabetOperations();

            string key = t.Substring(jIn, 4);

            uint[] k = op.textToArray(key);
            uint[] b = op.textToArray(blockIn);

            uint q = (k[0] + k[1] + k[2] + k[3]) % 4;

            for (uint i = 0; i <= 2; i++)
            {
                uint j = (q + i + 1) % 4;
                uint l = (q + i) % 4;
                b[j] = (b[j] + b[l]) % 32;
            }

            return op.arrayToText(b);
        }

        public string encryptSTritimusM(string blockIn, string keyIn, int jIn)
        {
            string tmp = encryptSBlockTritimus(blockIn, keyIn, jIn);
            string output = encryptImproveBlock(tmp, keyIn, jIn);
            return output;
        }

        public string decryptImproveBlock(string blockIn, string keyIn, int jIn)
        {
            string t = keyIn;
            while (jIn > t.Length - 4)
            {
                t += t;
            }

            AlphabetOperations op = new AlphabetOperations();

            string key = t.Substring(jIn, 4);

            uint[] k = op.textToArray(key);
            uint[] b = op.textToArray(blockIn);

            uint q = (k[0] + k[1] + k[2] + k[3]) % 4;

            for (int i = 2; i >= 0; i--)
            {
                uint j = (uint)(q + i + 1) % 4;
                uint l = (uint)(q + i) % 4;
                b[j] = (b[j] - b[l] + 32) % 32;
            }

            return op.arrayToText(b);
        }

        public string decryptSTritimusM(string blockIn, string keyIn, int jIn)
        {
            string tmp = decryptImproveBlock(blockIn, keyIn, jIn);
            return decryptSBlockTritimus(tmp, keyIn, jIn);
        }
    }
}