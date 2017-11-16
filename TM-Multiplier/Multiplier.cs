using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_Multiplier
{
    public class Multiplier
    {
        private IEnumerable<IEnumerable<int>> tapes;

        public Multiplier(string tapeContent)
        {
            this.tapes = new List<IEnumerable<int>>();
        }

        private bool CheckTapeContent()
        {
            return false;
        }
    }
}
