namespace TM_Multiplier
{
    using System;
    using System.Text;

    /// <summary>
    /// Turing machine providing simple methods that every turing machine has. 
    /// Plus simplifications of some operations 
    /// without extending the turing machine.
    /// </summary>
    public class TuringMachine
    {
        /// <summary>
        /// The character that is used to display an empty tape cell.
        /// </summary>
        protected const char WhitespaceCharacter = '-';

        /// <summary>
        /// The encoding that is used for the output in the console. 
        /// Default is Encoding.Default (ANSI).
        /// </summary>
        protected static readonly Encoding ConsoleOutputEncoding = Encoding.Default;
    }
}
