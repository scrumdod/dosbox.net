// <copyright file="TestOutput.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using System.Text;
using DosBox.Interfaces;

namespace DosBoxTest.Command.Framework
{
    /// <summary>
    /// Implements Outputter interface for testing purpose.<br/>
    /// Offers:
    /// <li/> buffering output for later investigation
    /// <li/> simulating reading characters from console
    /// </summary>
    public class TestOutput : IOutputter
    {
        private char characterThatIsRead = (char) 0;
        private StringBuilder output;

        public TestOutput()
        {
            output = new StringBuilder();
        }

        #region IOutputter Members

        public void NewLine()
        {
            output.Append("\n");
        }

        public void Print(string text)
        {
            output.Append(text);
        }

        public void PrintLine(string line)
        {
            output.Append(line);
            NewLine();
        }

        /// <summary>
        /// Simulates reading a character from console.
        /// Returns 0 if setCharacterThatIsRead() is not called previously.
        /// <br/>
        /// Usage in Unit Tests:<br/>
        /// TestOutput testOutput.setCharacterThatIsRead('Y');<br/>
        /// this.commandInvoker.ExecuteCommand("copy C:\\WinWord.exe C:\\ProgramFiles\\", testOutput);<br/>
        /// TestCase.assertTrue(testOutput.ToString().toLowerCase().contains("overwrite") == true);<br/>
        /// TestCase.assertTrue(testOutput.characterWasRead() == true);<br/>
        /// </summary>
        public char ReadSingleCharacter()
        {
            return characterThatIsRead;
        }

        #endregion

        /// <summary>
        /// Empties the buffered output. Important to call before starting a new test.
        /// </summary>
        public void empty()
        {
            output = new StringBuilder();
        }

        /// <summary>
        /// Returns the buffered output.
        /// </summary>
        public override string ToString()
        {
            return output.ToString();
        }

        /// <summary>
        /// Checks length of the buffered output
        /// </summary>
        /// <returns>
        ///   <c>true</c> if no character is stored in buffered output; otherwise, <c>false</c>.
        /// </returns>
        public bool isEmpty()
        {
            return output.Length == 0;
        }

        /// <summary>
        /// Sets the character that is read when calling readSingleCharacter().
        /// </summary>
        /// <param name="character">Character that is read.</param>
        public void setCharacterThatIsRead(char character)
        {
            characterThatIsRead = character;
        }
    }
}