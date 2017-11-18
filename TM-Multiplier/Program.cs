using System;

namespace TM_Multiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiplier m = new Multiplier("111x10");
            m.PrintTapeWithState();
            m.Calculate();
            m.PrintTapeWithState();
            Console.ReadLine();
        }
    }
}
