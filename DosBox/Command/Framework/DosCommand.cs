// <copyright file="DosCommand.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;
using System.Collections.Generic;
using DosBox.Interfaces;

namespace DosBox.Command.Framework
{
    /// <summary>
    /// Implements the abstract base class for commands.
    /// </summary>
    public abstract class DosCommand
    {
        /// <summary>
        /// Stores the name on which the commands reacts.
        /// </summary>
        private readonly string commandName;

        /// <summary>
        /// Drive on which the command operates. Use getDrive() to obtain the drive from a concrete command.
        /// </summary>
        private readonly IDrive drive;

        /// <summary>
        /// The list of parameters passed to the command. Use getParameters() to obtain this list from a concrete command.
        /// </summary>
        private List<string> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="DosCommand"/> class.
        /// </summary>
        /// <param name="cmdName">Name on which the command reacts. This name is automatically converted to lower case letters.</param>
        /// <param name="drive">Drive on which the command operates.</param>
        protected DosCommand(string cmdName, IDrive drive)
        {
            commandName = cmdName.ToLower();
            this.drive = drive;
        }

        /// <summary>
        /// Gets the drive on which the command shall operate.
        /// </summary>
        /// <returns>The drive.</returns>
        public IDrive Drive
        {
            get { return drive; }
        }

        /// <summary>
        /// Returns true if the passed name and the command name fit.
        /// Used to obtain the concrete command from the list of commands.
        /// </summary>
        /// <param name="commandNameToCompare">name with which the command name shall be compared.</param>
        /// <returns>
        /// <li/> true if names fit
        /// <li/> false otherwise
        /// </returns>
        public bool CompareCmdName(string commandNameToCompare)
        {
            return commandName.CompareTo(commandNameToCompare) == 0;
        }

        /// <summary>
        /// Sets the list of parameters. This operation shall be called by the invoker only.
        /// </summary>
        /// <param name="newParameters">The parameters.</param>
        public void SetParameters(List<string> newParameters)
        {
            parameters = newParameters;
        }

        /// <summary>
        /// <b>Can be overwritten</b> by the concrete commands if the list of parameters is very complex.
        /// ssign the parameters in the parameter-list to appropriate attributes in the
        /// concrete commands.
        /// </summary>
        protected virtual void SetParameters()
        {
        }

        /// <summary>
        /// Returns the list of parameters, e.g. to check parameters by a concrete command.
        /// </summary>
        /// <returns>The list of parameters.</returns>
        protected List<string> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Resets the internal state of a command. Must be called after each execution.
        /// </summary>
        public void Reset()
        {
            parameters = null;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return commandName;
        }

        /// <summary>
        /// Checks the passed parameters. This consists of three steps which are both overrideable by concrete commands.<br/>
        /// 1.) Check the number of parameters.<br/>
        /// 2.) Check the values of all passed parameters<br/>
        /// 3.) Assign the parameters to attributes in the concrete commands.<br/>
        /// <br/>
        /// Template Method Pattern: Template Method.
        /// </summary>
        /// <param name="outputter">The outputter must be used to output any error description.</param>
        /// <returns>
        /// <li/> true  if number and values of the parameters are correct. Execute() may use the parameters afterwards unchecked.
        /// <li/> false if the number is below or above excepted range or if any value is incorrect. An explaining error message must be given
        ///           by the concrete command. 
        /// </returns>
        public bool CheckParameters(IOutputter outputter)
        {
            if (parameters == null)
            {
                throw new Exception("Parameters checked before set!");
            }

            if (CheckNumberOfParameters(parameters.Count) == false)
            {
                outputter.PrintLine("The syntax of the command is incorrect.");
                return false;
            }

            if (CheckParameterValues(outputter) == false)
                return false;

            SetParameters();

            return true;
        }

        /// <summary>
        /// <b>Can be overwritten</b> by the concrete commands if something must be checked.
        /// Checks if the number of parameters is in range.
        /// Do not output anything, an explaining error message is output by the abstract command.<br/>
        /// <br/>
        /// Template Method Pattern: Hook.<br/>
        /// </summary>
        /// <param name="number">Number of parameters passed by the caller.</param>
        /// <returns>
        /// <li/> true if number of parameters is within expected range
        /// <li/> false otherwise
        /// </returns>
        protected virtual bool CheckNumberOfParameters(int number)
        {
            return true;
        }

        /// <summary>
        /// <b>Can be overwritten</b> by the concrete commands if at least the value of one parameter must be checked.
        /// Checks all values of all passed parameters.
        /// An explaining error message must be output by the concrete command.<br/>
        /// <br/>
        /// Template Method Pattern: Hook.<br/>
        /// </summary>
        /// <param name="outputter">The output must be used to output error messages.</param>
        /// <returns>
        /// <li/> true if all values of all parameters passed are correct.
        /// <li/> false if at least one value of one parameter in incorrect.
        /// </returns>
        protected virtual bool CheckParameterValues(IOutputter outputter)
        {
            return true;
        }

        /// <summary>
        /// <b>Must be overwritten</b> by the concrete commands to implement the execution of the command.
        /// </summary>
        /// <param name="outputter">Must be used to output any text.</param>
        public abstract void Execute(IOutputter outputter);
    }
}