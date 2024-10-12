using inf_sec.tritimus_encode;

namespace inf_sec.lcg_generator;

public class Converter : IConverter
{
        public ulong ConvertBlockToNumb(List<Char> block)
        {
            if (block.Count != 4) throw new Exception("input_error");

            AlphabetOperations op = new AlphabetOperations();

            ulong output = 0;
            ulong pos = 1;
            uint[] temp = op.textToArray(new string(block.ToArray()));
            for (int i = 3; i >= 0; i--)
            {
                output += pos * temp[i];
                pos *= 32;
            }

            return output;
        }

        public List<Char> ConvertNumbToBlock(ulong number)
        {
            AlphabetOperations op = new AlphabetOperations();
            uint[] temp = new uint[4];

            for (int i = 0; i <= 3; i++)
            {
                temp[3-i] = (uint)number % 32;
                number /= 32;
            }

            return op.arrayToText(temp).ToList();
        }

        public uint[] ConverNumbToBinaryArray(ulong number)
        {
            uint[] output = new uint[20];
            for (int i = 0; i < 20; i++)
            {
                output[19 - i] = (uint)(number % 2);
                number /= 2;
            }
            return output;
        }

        public ulong ConverBinaryArrayToNumb(uint[] binArray)
        {
            uint output = 0;
            for (int i = 0; i < 20; i++)
            {
                output = 2 * output + binArray[i];
            }
            return output;
        }
}