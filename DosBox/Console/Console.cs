// <copyright file="Console.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;
using System.IO;
using System.Text;
using DosBox.Interfaces;

namespace DosBox.Console
{
    /// <summary>
    /// Implements a console. The user is able to input command strings
    /// and receives the output directly on that console.
    /// Configures the Invoker, the Commands and the Filesystem.
    /// </summary>
    public class ConsoleEx
    {
        private readonly IDrive drive;
        private readonly IExecuteCommand invoker;
        private readonly IOutputter outputter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleEx"/> class.
        /// </summary>
        /// <param name="invoker">reference to the invoker used.</param>
        /// <param name="drive">reference to the drive from which the prompt is taken.</param>
        public ConsoleEx(IExecuteCommand invoker, IDrive drive)
        {
            this.invoker = invoker;
            this.drive = drive;
            outputter = new ConsoleOutputter();
        }

        /// <summary>
        /// Processes input from the console and invokes the invoker until 'exit' is typed.
        /// </summary>
        public void ProcessInput()
        {
            string line = string.Empty;
            outputter.PrintLine("Zühlke Agile Course [Version 19.1.2010]");
            outputter.PrintLine("(C) Copyright 2006-2010 Rainer Grau and Daniel Tobler.");

            while (line.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase) == false)
            {
                int readChar = 0;
                var input = new StringBuilder();

                outputter.NewLine();
                outputter.Print(drive.Prompt);
                try
                {
                    while (readChar != '\n')
                    {
                        readChar = System.Console.Read();
                        input.Append((char) readChar);
                    }

                    line = input.ToString();
                }
                catch (IOException)
                {
                    // do nothing by intention
                }

                invoker.ExecuteCommand(line, outputter);
            }

            outputter.PrintLine("\nGoodbye!");
            drive.Save();
        }
    }
}