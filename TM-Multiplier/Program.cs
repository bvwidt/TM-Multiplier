using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
