namespace TM_Multiplier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
        private List<char> tape;

        /// <summary>
        /// TODO
        /// </summary>
        private readonly State currentState;

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
            // Write the input to the tape
            this.tape = new List<char>();
            foreach (char character in tapeContent)
            {
                this.tape.Add(character);
            }

            // Set the current state at the beginning of the input on the tape
            this.currentState = new State(0, this.GetCharAtPosition(0));
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void PrintTapeWithState()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < this.tape.Count; i++)
            {
                if (i == this.currentState.Position)
                {
                    stringBuilder.Append(State.StateIdentifier);
                }

                stringBuilder.Append(this.tape[i]);
            }

            Console.WriteLine(stringBuilder);
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
            // 1. Skip first binary number
            this.GoRightUntilX();

            // 2. Read "x", write "x", go left
            this.GoLeft();

            // TODO Check for unexpected values
            int readChar = Int32.Parse(this.GetCharAtPosition(0).ToString());
            ReplaceTapeChar(this.currentState.Position, 'd');
            if (readChar == 0)
            {
                // Go right and skip "x"
                this.GoRightUntilX();
                this.GoRight();
                
            }
            else if (readChar == 1)
            {
                // Go right and skip "x"
                this.GoRightUntilX();
                this.GoRight();

                this.CopyNumberToEmptyTape();
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void GoRight()
        {
            this.currentState.Position++;
            this.RefreshCurrentStateTapeContent();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="position"></param>
        /// <param name="newTapeChar">
        /// The new tape char.
        /// </param>
        public void ReplaceTapeChar(int position, char newTapeChar)
        {
            this.tape[position] = newTapeChar;
            this.GoRight();
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void GoLeft()
        {
            this.currentState.Position--;
            this.RefreshCurrentStateTapeContent();
        }

        /// <summary>
        /// Returns the content (first) tape.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> containing the content of the first tape.
        /// </returns>
        public string GetTapeContent()
        {
            return new String(this.tape.ToArray());
        }

        /// <summary>
        /// TODO Using current state
        /// </summary>
        /// <param name="tapeNumber">
        /// The tape number.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        private char GetCharAtPosition()
        {
            return this.GetCharAtPosition(this.currentState.Position);
        }

        /// <summary>
        /// Gets the char at a given position (0-indexed) of a given tape.
        /// </summary>
        /// <param name="position">
        /// The position on the tape whose char should be returned.
        /// </param>
        /// <param name="tapeNumber">
        /// The number / index of the tape that should be checked.
        /// </param>
        /// <returns>
        /// A <see cref="char"/> of the tape.
        /// </returns>
        /// <example>
        /// Returns the char at the position two of the first tape.
        /// </example>
        private char GetCharAtPosition(int position)
        {
            return this.tape[position];
        }

        private void RefreshCurrentStateTapeContent()
        {
            this.currentState.TapeChar = this.GetCharAtPosition(this.currentState.Position);
        }

        /// <summary>
        /// Moves (the state) right until a "x"-character is found 
        /// in the first tape.
        /// </summary>
        private void GoRightUntilX()
        {
            while (!this.GetCharAtPosition(this.currentState.Position).Equals('x'))
            {
                this.GoRight();
            }
        }

        private void GoLeftUntilDigit()
        {
            // TODO Read "d" until the next decimal is reached to read
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private void CopyNumberToEmptyTape()
        {
            
        }
    }
}
