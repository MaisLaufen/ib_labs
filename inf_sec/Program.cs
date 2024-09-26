using System.Collections;

namespace inf_sec;

static class Program
{
    struct Symbol
    {
        public char symbol;
        public BitArray bits;
        public int index;
    }

    static void Main(string[] args)
    {
        string origAlphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ_";
        Tritimus tritimus = new Tritimus(origAlphabet, 8);

        List<char> modAlph = tritimus.getModifiedAlphabet("ПТЕРАДАКТИЛЬ");

        string str1 = tritimus.encryptTheWord("СТРОКА", modAlph);
        string str2 = tritimus.decryptTheWord(str1, modAlph);
        Console.WriteLine("простой Тритимус: \nСТРОКА -> " + str1);
        Console.WriteLine(str1 + " -> " + str2);

        string str3 = tritimus.encryptPolyTritimus("ПИТБУЛЬ", "КЛЮЧ");
        string str4 = tritimus.decryptPolyTritimus(str3, "КЛЮЧ");
        Console.WriteLine("\nполи Тритимус: \nПИТБУЛЬ -> " + str3);
        Console.WriteLine(str3 + " -> " + str4);

        string str5 = tritimus.encryptSBlockTritimus("СПАМ", "КЕНТАВР", 123);
        string str6 = tritimus.decryptSBlockTritimus(str5, "КЕНТАВР", 123);
        Console.WriteLine("\ns-блок Тритимус: \nСПАМ -> " + str5);
        Console.WriteLine(str5 + " -> " + str6);

        string str7 = tritimus.encryptSTritimusM("СЛОТ", "ОБЛАКА", 33);
        string str8 = tritimus.decryptSTritimusM(str7, "ОБЛАКА", 33);
        Console.WriteLine("\nулучшеный Тритимус: \nСЛОТ -> " + str7);
        Console.WriteLine(str7 + " -> " + str8);

        Console.Read();
    }

}
