using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_Multiplier
{
    /// <summary>
    /// TODO
    /// </summary>
    public class State
    {
        /// <summary>
        /// Used to display where the state is exactly on the tape currently.
        /// </summary>
        public const string StateIdentifier = "Q";

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="position">TODO</param>
        /// <param name="tapeChar">TODO</param>
        public State(int position, char tapeChar)
        {
            this.Position = position;
            this.TapeChar = tapeChar;
        }

        /// <summary>
        /// Gets or sets the position of the state in the (overall) tape.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the tape character at the given position.
        /// </summary>
        public char TapeChar { get; set; }
    }
}
