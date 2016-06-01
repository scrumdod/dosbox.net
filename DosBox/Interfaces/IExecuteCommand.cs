// <copyright file="IExecuteCommand.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;

namespace DosBox.Interfaces
{
    public interface IExecuteCommand
    {
        /// Interprets a command string, executes if an appropriate command is found
        /// and returns all output via the outputter interface.
        /// <param name="command">String which is entered, containing the command and the parameters.</param>
        /// <param name="outputter">Implementation of the outputter interface to which the output text is sent.</param>
        void ExecuteCommand(String command, IOutputter outputter);
    }
}