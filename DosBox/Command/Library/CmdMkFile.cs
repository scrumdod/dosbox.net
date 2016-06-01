// <copyright file="CmdMkFile.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    /// <summary>
    /// The implementation is not complete:
    /// 2 Unit Tests for this class fail. See Task Card #002.
    /// </summary>
    public class CmdMkFile : DosCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CmdMkFile"/> class.
        /// </summary>
        /// <param name="cmdName">Name on which the command reacts. This name is automatically converted to lower case letters.</param>
        /// <param name="drive">Drive on which the command operates.</param>
        public CmdMkFile(string cmdName, IDrive drive)
            : base(cmdName, drive)
        {
        }

        /// <summary>
        /// <b>Must be overwritten</b> by the concrete commands to implement the execution of the command.
        /// </summary>
        /// <param name="outputter">Must be used to output any text.</param>
        public override void Execute(IOutputter outputter)
        {
            string fileName = GetParameters()[0];
            string fileContent = GetParameters()[1];

            var newFile = new File(fileName, fileContent);
            Drive.CurrentDirectory.Add(newFile);
        }
    }
}