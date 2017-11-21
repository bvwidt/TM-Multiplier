using System;

namespace TM_Multiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiplier m = new Multiplier("0x10001", Mode.Step);
            m.PrintTapeWithState();
            m.Calculate();
            m.PrintTapeWithState();
            Console.ReadLine();
        }
    }
}
