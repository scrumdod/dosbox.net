// <copyright file="CmdCd.cs" company="Zuehlke Technology Group">
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
    /// Command to change current directory.
    /// Example for a command with optional parameters.<br/>
    /// <br/>
    /// Pattern: Command, Concrete Command
    /// </summary>
    public class CmdCd : DosCommand
    {
        private const string SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED = "The system cannot find the path specified";
        private string newDirectoryName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdCd"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="drive">The drive.</param>
        public CmdCd(string name, IDrive drive)
            : base(name, drive)
        {
        }

        /// <summary>
        /// Only returns true if number of parameters is either 0 or 1.<br/>
        /// 0: outputs the current directory.<br/>
        /// 1: changes current directory to the given directory.
        /// </summary>
        /// <param name="number">Number of parameters passed by the caller.</param>
        /// <returns>
        /// <li/> true if number of parameters is within expected range
        /// <li/> false otherwise
        /// </returns>
        protected override bool CheckNumberOfParameters(int number)
        {
            return number == 0 || number == 1;
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameters().Count > 0)
            {
                newDirectoryName = GetParameters()[0];
            }

            return true;
        }

        public override void Execute(IOutputter outputter)
        {
            bool retVal = false;

            // cd without parameters
            if (newDirectoryName == null)
            {
                outputter.PrintLine(Drive.CurrentDirectory.Path);
                return;
            }

            // cd with parameters: Check if passed directory is valid before change to this directory
            FileSystemItem newDir = Drive.GetItemFromPath(newDirectoryName);
            if (newDir == null)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
                return;
            }

            if (newDir.IsDirectory() == false)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
                return;
            }

            if (Drive.GetItemFromPath(newDir.Path) != newDir)
            {
                outputter.PrintLine("Path not in drive " + Drive.DriveName);
                return;
            }

            if (newDir.IsDirectory())
            {
                retVal = Drive.ChangeCurrentDirectory((Directory) newDir);
            }

            if (retVal == false)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
            }
        }
    }
}