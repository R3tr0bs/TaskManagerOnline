using System;

namespace CreateKeyRegedit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trying To Create a Key");
            TMREAddons.RegEdit.MainFolder("HKEY_LOCAL_MACHINE");
            TMREAddons.RegEdit.GetIntoFolder("Hardware");
            bool test = TMREAddons.RegEdit.CreateKey("test", 123);
            Console.WriteLine($"Test:{test}");
            Console.Read();

        }
    }
}
