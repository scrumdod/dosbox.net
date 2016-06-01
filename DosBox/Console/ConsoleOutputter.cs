// <copyright file="ConsoleOutput.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System.IO;
using DosBox.Interfaces;

namespace DosBox.Console
{
    /// <summary>
    /// Implements the outputter interface that all text is sent to
    /// the console (System.out).
    /// </summary>
    public class ConsoleOutputter : IOutputter
    {
        #region IOutputter Members

        /// <summary>
        /// Outputs a newline.
        /// </summary>
        public void NewLine()
        {
            System.Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Outputs the string. Does not add a newline at the end.
        /// </summary>
        /// <param name="text">text string to output.</param>
        public void Print(string text)
        {
            System.Console.Write(text);
        }

        /// <summary>
        /// Outputs the string and adds a newline at the end.
        /// If a newline is already passed, two newlines are output.
        /// </summary>
        /// <param name="line">string to output. No ending newline character required.</param>
        public void PrintLine(string line)
        {
            System.Console.WriteLine(line);
        }

        /// <summary>
        /// Reads a single character from the channel.
        /// May be used to ask the user Yes/No questions.<br/>
        /// Note: This function does not return until user entered a character.
        /// It may be that calling this function causes a deadlock!
        /// </summary>
        /// <returns>character read</returns>
        public char ReadSingleCharacter()
        {
            int input = 0;
            int readChar = 0;

            try
            {
                while (input != '\n')
                {
                    // do not consider \r and \n
                    if (input != '\n' && input != '\r')
                    {
                        readChar = input;
                    }

                    input = System.Console.Read();
                }
            }
            catch (IOException)
            {
                // TODO e.printStackTrace();
                readChar = 0;
            }

            return (char) readChar;
        }

        #endregion
    }
}