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
        private const string AcceptanceRegex = "[0-1]*x[0-1]*";

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

            // Print the current state at the start
            if (this.currentState.Position < 0)
            {
                stringBuilder.Append(State.StateIdentifier);
            }
            
            for (int i = 0; i < this.tape.Count; i++)
            {
                // Print the current state somewhere on the tape content
                if (i == this.currentState.Position)
                {
                    stringBuilder.Append(State.StateIdentifier);
                }

                stringBuilder.Append(this.tape[i]);
            }

            // Print the current state at the end
            if (this.currentState.Position >= this.tape.Count)
            {
                stringBuilder.Append(State.StateIdentifier);
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
            // Append an y to identify when the input ends
            this.AppendY();

            // 1. Skip first binary number
            this.GoRightUntil('x');

            // 2. Read "x", write "x", go left
            this.GoLeft();

            // 3. Multiply (/copy) second number (/ factor) with each
            // digit of the first number (/ factor) with 
            // correct tenner multiplicity
            char readChar = this.GetCharAtPosition();
            do
            {
                this.GoRight('d');
                if (readChar.Equals('0'))
                {
                    // Go right to the end of the input
                    this.AppendZ();
                    this.GoLeftUntil('x');
                }
                else if (readChar.Equals('1'))
                {
                    this.CopySecondFactor();
                }
                this.GoLeftUntil(new[] { '0', '1', ' ' });
                readChar = this.GetCharAtPosition();
            }
            while (!readChar.Equals(' '));

            // 4. Replace "z" and "s" by 0
            // E.g. ...yss110y... ==> ...y11000y...
            this.GoRight();
            this.CorrectTennerPlaceHolders();

            // 5. Remove input
            RemoveInput();
        }

        /// <summary>
        /// Moves the current state one character right.
        /// </summary>
        public void GoRight()
        {
            this.currentState.Position++;
            this.RefreshCurrentStateTapeContent();
        }

        public void GoRight(char newTapeChar)
        {
            ReplaceTapeChar(this.currentState.Position, newTapeChar);
            this.GoRight();
        }

        /// <summary>
        /// Moves (the state) right until a the given character is found.
        /// </summary>
        /// <param name="character">
        /// The character that stops the moving.
        /// </param>
        public void GoRightUntil(char character)
        {
            while (!this.GetCharAtPosition(this.currentState.Position).Equals(character))
            {
                this.GoRight();
            }
        }

        public void GoRightUntil(IEnumerable<char> characters)
        {
            char[] chars = characters.ToArray();
            while (!chars.Contains(this.GetCharAtPosition()))
            {
                this.GoRight();
            }
        }

        /// <summary>
        /// Moves the current state one character left.
        /// </summary>
        public void GoLeft()
        {
            this.currentState.Position--;
            this.RefreshCurrentStateTapeContent();
        }

        public void GoLeft(char newTapeChar)
        {
            ReplaceTapeChar(this.currentState.Position, newTapeChar);
            this.GoLeft();
        }

        /// <summary>
        /// Moves (the state) left until the given character is found.
        /// </summary>
        /// <param name="character">
        /// The character that stops the moving.
        /// </param>
        public void GoLeftUntil(char character)
        {
            while (!this.GetCharAtPosition().Equals(character))
            {
                this.GoLeft();
            }
        }

        /// <summary>
        /// Moves (the state) left until a character of 
        /// the given ones is found.
        /// </summary>
        /// <param name="characters">
        /// A collection containing characters that stops the moving.
        /// </param>
        public void GoLeftUntil(IEnumerable<char> characters)
        {
            char[] chars = characters.ToArray();
            while (!chars.Contains(this.GetCharAtPosition()))
            {
                this.GoLeft();
            }
        }

        private void ReplaceTapeChar(char newTapeChar)
        {
            ReplaceTapeChar(this.currentState.Position, newTapeChar);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="position"></param>
        /// <param name="newTapeChar">
        /// The new tape char.
        /// </param>
        private void ReplaceTapeChar(int position, char newTapeChar)
        {
            // If a char is "deleted" / whitespace set at 
            // the start or end of the tape, 
            // decrease the size of the list
            if (position <= 0 && newTapeChar.Equals(' '))
            {
                for (int i = 0; i < this.tape.Count - 1; i++)
                {
                    this.tape[i] = this.tape[i + 1];
                }

                this.RemoveLastTapeChar();
            }
            else if (position >= this.tape.Count)
            {
                if (newTapeChar.Equals(' '))
                {
                    this.RemoveLastTapeChar();
                }
                else
                {
                    this.tape.Add(newTapeChar);
                }
            }
            else
            {
                this.tape[position] = newTapeChar;
            }
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
            // When the current state has reached the end of the input
            // read an empty space ' '.
            if (position < 0 || position >= this.tape.Count)
            {
                return ' ';
            }

            return this.tape[position];
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void RefreshCurrentStateTapeContent()
        {
            this.currentState.TapeChar = this.GetCharAtPosition(this.currentState.Position);
        }

        /// <summary>
        /// Appends an 'y' at the end of the tape content and 
        /// returns (current state) to the start of the tape content.
        /// </summary>
        private void AppendY()
        {
            this.GoRightUntil(' ');
            this.GoLeft('y');
            this.GoLeftUntil(' ');
        }

        private void AppendZ()
        {
            this.GoRightUntil(new[] { 'z', ' ' });
            char readChar = this.GetCharAtPosition();
            while (readChar.Equals('z'))
            {
                this.GoRight('z');
                readChar = this.GetCharAtPosition();
            }
            this.ReplaceTapeChar('z');
        }

        private void CopySecondFactor()
        {
            // Go right until the 'x' and skip it
            this.GoRightUntil('x');
            this.GoRight();

            // Use do-while because the second factor is always at minimum 0
            // given by the accepted language of the TM
            char readChar = this.GetCharAtPosition();
            do
            {
                this.GoRight(readChar.Equals('0') ? 'a' : 'b');
                this.GoRightUntil(' ');
                this.GoLeft(readChar);
                this.GoLeftUntil(new[] { 'a', 'b' });
                this.GoRight();
                readChar = this.GetCharAtPosition();
            }
            while (readChar.Equals('0') || readChar.Equals('1'));

            // Move to the end and mark it with an "y"
            // then return to next left "y"
            this.GoRightUntil(' ');
            this.GoLeft('y');
            this.GoLeftUntil('y');

            this.Append10Multiplicity();
            this.ResetSecondFactor();
        }

        private void Append10Multiplicity()
        {
            this.GoRight();
            this.GoRightUntil(new[] { 'z', 's', 'y' });
            char readChar = this.GetCharAtPosition();
            if (readChar.Equals('z'))
            {
                this.GoRight('s');
                this.GoRightUntil(' ');
                this.GoLeft('z');

                // Go left to the y after next
                this.GoLeftUntil('y');
                this.GoLeft();
                this.GoLeftUntil('y');
                this.Append10Multiplicity();
            }
            else if (readChar.Equals('s'))
            {
                // Skip read "s" and search for next "z"
                this.Append10Multiplicity();
            }
            else
            {
                this.GoRightUntil(' ');
                this.ReplaceTapeChar('z');
            }
        }

        private void ResetSecondFactor()
        {
            this.GoLeftUntil(new[] { 'a', 'b' });
            char readChar = this.GetCharAtPosition();
            while (readChar.Equals('a') || readChar.Equals('b'))
            {
                this.GoLeft(readChar.Equals('a') ? '0' : '1');
                readChar = this.GetCharAtPosition();
            }
        }

        /// <summary>
        /// Replaces "z" and "s" by 0 and moves it to the end 
        /// of the corresponding number each copied number. 
        /// </summary>
        /// <example>...yss110y... ==> ...y11000y...</example>
        private void CorrectTennerPlaceHolders()
        {
            // Read the first character after the y
            // If it is an s (tenner multiplier), move the number one left and 
            // append an 0.
            this.GoRightUntil(new[] { 'y', ' ' });
            char readChar = this.GetCharAtPosition();
            if (readChar.Equals('y'))
            {
                this.GoRight();
                readChar = this.GetCharAtPosition();
                while (readChar.Equals('s'))
                {
                    this.GoRightUntil('y');
                    this.GoLeft();
                    readChar = this.GetCharAtPosition();
                    this.GoLeft('0');
                    while (readChar.Equals('0') || readChar.Equals('1'))
                    {
                        char newReadChar = this.GetCharAtPosition();
                        this.GoLeft(readChar);
                        readChar = newReadChar;
                    }
                    readChar = this.GetCharAtPosition();
                }

                this.CorrectTennerPlaceHolders();
            }

            this.GoLeft();
            readChar = this.GetCharAtPosition();
            while (readChar.Equals('z'))
            {
                this.GoLeft(' ');
                readChar = this.GetCharAtPosition();
            }
        }
        
        private void RemoveInput()
        {
            GoLeftUntil(' ');
            this.GoRight();
            char readChar = this.GetCharAtPosition();
            while (!readChar.Equals('y'))
            {
                this.GoRight(' ');
                readChar = this.GetCharAtPosition();
            }
            this.GoRight(' ');
        }

        private void AddNumbers()
        {
            
        }

        private void RemoveLastTapeChar()
        {
            this.tape.RemoveAt(this.tape.Count - 1);

            // Decrease current position because we "moved" the tape
            this.currentState.Position--;
        }
    }
}
