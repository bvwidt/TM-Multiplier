using System;

namespace TM_Multiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiplier m = new Multiplier("0x1011", Mode.Result);
            m.PrintTapeWithState();
            m.Calculate();
            Console.ReadLine();
        }
    }
}
