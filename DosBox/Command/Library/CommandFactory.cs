// <copyright file="CommandFactory.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    /// <summary>
    /// The factory is responsible to create an object of every command supported
    /// and to add it to the list of known commands.
    /// New commands must be added to the list of known commands here.
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// List of known commands.
        /// </summary>
        private readonly List<DosCommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFactory"/> class.
        /// </summary>
        /// <param name="drive">reference to the drive, the commands operate on.</param>
        public CommandFactory(IDrive drive)
        {
            commands = new List<DosCommand>
                           {
                               new CmdDir("dir", drive),
                               new CmdCd("cd", drive),
                               new CmdCd("chdir", drive),
                               new CmdMkDir("mkdir", drive),
                               new CmdMkDir("md", drive),
                               new CmdMkFile("mf", drive),
                               new CmdMkFile("mkfile", drive)

                               // Add commands here
                           };
        }

        /// <summary>
        /// Gets the list of known commands.
        /// Is called at configuration time to transfer the supported commands to the invoker.
        /// </summary>
        /// <returns>List of known commands.</returns>
        public IList<DosCommand> CommandList
        {
            get { return commands; }
        }
    }
}