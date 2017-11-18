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
        /// <param name="tapeContent">TODO</param>
        public State(int position, IEnumerable<char> tapeContent)
        {
            this.Position = position;
            this.TapeContent = tapeContent;
        }

        /// <summary>
        /// Gets or sets the position of the state in the (overall) tape.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the tape content at the given position. 
        /// It contains a char for each tape that has content at the position.
        /// </summary>
        public IEnumerable<char> TapeContent { get; set; }
    }
}
