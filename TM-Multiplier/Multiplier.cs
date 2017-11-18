namespace TM_Multiplier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Multiplier
    {
        /// <summary>
        /// The regular expression that the given word must satisfy.
        /// </summary>
        private const string AcceptanceRegex = "[0-1]+x[0-1]+";

        /// <summary>
        /// Because a one-tape turing machine (TM) has equal capacity as
        /// </summary>
        private IEnumerable<IEnumerable<char>> tapes;

        /// <summary>
        /// TODO
        /// </summary>
        private State currentState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Multiplier"/> class 
        /// and writes each digit (and the x) on the tape.
        /// </summary>
        /// <param name="tapeContent">
        /// A string containing two binary numbers 
        /// separated by an x which should be multiplied.
        /// </param>
        public Multiplier(string tapeContent)
        {
            FillTape(tapeContent);
            this.currentState = new State(0, this.GetTapeAtPosition(0));
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void PrintAllTapesWithState()
        {
            int maxTapeLength = this.tapes.Max(t => t.Count());

            foreach (IEnumerable<char> t in this.tapes)
            {
                char[] tape = t.ToArray();
                for (int i = 0; i < maxTapeLength; i++)
                {
                    if (i == this.currentState.Position)
                    {
                        Console.Write(State.StateIdentifier);
                    }

                    Console.Write(tape[i]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Checks if the given tape content / input accepted by 
        /// the language of the TM.
        /// </summary>
        /// <param name="tapeContent">
        /// The text which should be written on the type.
        /// </param>
        /// <returns>
        /// True if the given string is accepted, otherwise false.
        /// </returns>
        public bool CheckTapeContent(string tapeContent)
        {
            bool isAccepted;
            try
            {
                isAccepted = Regex.IsMatch(tapeContent, AcceptanceRegex);
            }
            catch
            {
                isAccepted = false;
            }

            return isAccepted;
        }

        /// <summary>
        /// Calculates the input on the first tape and 
        /// writes the result back on the first tape.
        /// </summary>
        public void Calculate()
        {
            
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void GoRight()
        {
            
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="tapeContent"></param>
        private void FillTape(string tapeContent)
        {
            List<char> firstTape = new List<char>();
            foreach (char character in tapeContent)
            {
                firstTape.Add(character);
            }

            // Add the input as the first tape
            this.tapes = new List<IEnumerable<char>>(new[] { new List<char>(firstTape) });
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        private IEnumerable<char> GetTapeAtPosition(int position)
        {
            var currentTapeContent = new List<char>();

            foreach (IEnumerable<char> tape in this.tapes)
            {
                try
                {
                    currentTapeContent.Add(tape.ToArray()[position]);
                }
                catch (IndexOutOfRangeException)
                {
                    // When in another tape nothing is found at the given
                    // position, it means that it is empty.
                    // An underscore is used to show that.
                    currentTapeContent.Add('_');
                }
            }

            return currentTapeContent;
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void GoRightUntilX()
        {
            while (true)
            {
                
            }
            this.GoRight();
        }

        /// <summary>
        /// Returns the content (first) tape.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> containing the content of the first tape.
        /// </returns>
        public string GetTapeContent()
        {
            return new String(this.tapes.FirstOrDefault()?.ToArray());
        }
    }
}
