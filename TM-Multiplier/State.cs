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
        /// Used for the overridden ToString() method. 
        /// {0} is the place holder for the Position and 
        /// {1} for TapeChar.
        /// </summary>
        private const string ToStringFormat = "[{0}]: {1}";

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

        /// <summary>
        /// Overrides the ToString() method so the position and 
        /// the current char is returned directly.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> in the format "["&lt;Position&gt;"]: "&lt;TapeChar&gt;.
        /// </returns>
        public override string ToString()
        {
            return String.Format(ToStringFormat, this.Position, this.TapeChar);
        }
    }
}
