// <copyright file="CmdMkDir.cs" company="Zuehlke Technology Group">
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
    public class CmdMkDir : DosCommand
    {
        public CmdMkDir(string name, IDrive drive)
            : base(name, drive)
        {
        }

        protected override bool CheckNumberOfParameters(int number)
        {
            // Commands like "mkdir dir1 dir2 dir3" are allowed too.
            if (number < 1)
                return false;

            return true;
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            foreach (string parameter in GetParameters())
            {
                // Do not allow "mkdir c:\temp\dir1" to keep the command simple
                if (parameter.Contains("\\") || parameter.Contains("/"))
                {
                    outputter.PrintLine("At least one parameter denotes a path rather than a directory name.");
                    return false;
                }
            }

            return true;
        }

        public override void Execute(IOutputter outputter)
        {
            foreach (string newDirName in GetParameters())
            {
                var newDir = new Directory(newDirName);
                Drive.CurrentDirectory.Add(newDir);
            }
        }
    }
}