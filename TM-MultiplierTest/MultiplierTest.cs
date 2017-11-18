namespace TM_MultiplierTest
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TM_Multiplier;

    [TestClass]
    public class MultiplierTest
    {
        /// <summary>
        /// Tests 4 different calculations.
        /// </summary>
        [TestMethod]
        public void CalculateTest()
        {
            List<TestCalculation> tests = new List<TestCalculation>(new[]
                {
                    new TestCalculation("10x100", "1000"),          // 2 x 4 = 8
                    new TestCalculation("1101x10001", "11011101"),  // 13 x 17 = 221,
                    new TestCalculation("1x11011‬", "11011"),        // 1 x 27 = 27
                    new TestCalculation("0x10111", "0")             // 0 x 23 = 0,
                });

            foreach (TestCalculation testCalculation in tests)
            {
                Multiplier multiplier = new Multiplier(testCalculation.TapeContent);
                multiplier.Calculate();
                Assert.AreEqual(testCalculation.Output, multiplier.GetTapeContent());
            }
        }

        /// <summary>
        /// A struct containing the input (TapeContent) and 
        /// the expected value (Output).
        /// </summary>
        private struct TestCalculation
        {
            /// <summary>
            /// The input that is written on the tape: 
            /// The calculation (binary number x binary number).
            /// </summary>
            public string TapeContent;

            /// <summary>
            /// The expected tape content 
            /// after the calculation (one binary number).
            /// </summary>
            public string Output;

            /// <summary>
            /// Initializes a new instance of 
            /// the <see cref="TestCalculation"/> struct.
            /// </summary>
            /// <param name="tapeContent"></param>
            /// <param name="output"></param>
            public TestCalculation(string tapeContent, string output)
            {
                this.TapeContent = tapeContent;
                this.Output = output;
            }
        }
    }
}
