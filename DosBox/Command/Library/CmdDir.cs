// <copyright file="CmdDir.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdDir : DosCommand
    {
        private Directory dirToPrint;

        public CmdDir(string name, IDrive drive)
            : base(name, drive)
        {
        }

        protected override bool CheckNumberOfParameters(int number)
        {
            return number == 0 || number == 1;
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameters().Count > 0)
            {
                string dirPath = GetParameters()[0];
                FileSystemItem fs = Drive.GetItemFromPath(dirPath);
                if (fs == null)
                {
                    outputter.PrintLine("File Not Found");
                    return false;
                }

                if (fs.IsDirectory() == false)
                {
                    dirToPrint = fs.Parent;
                }
                else
                {
                    dirToPrint = (Directory) fs;
                }
            }
            else
            {
                dirToPrint = Drive.CurrentDirectory;
            }

            return true;
        }

        public override void Execute(IOutputter outputter)
        {
            List<FileSystemItem> content = dirToPrint.Content;

            outputter.PrintLine("Directory of " + dirToPrint.Path);
            outputter.NewLine();

            foreach (FileSystemItem item in content)
            {
                if (item.IsDirectory())
                {
                    outputter.Print("<DIR>");
                }
                else
                {
                    outputter.Print("" + item.GetSize());
                }

                outputter.Print("\t" + item.Name);
                outputter.NewLine();
            }

            outputter.PrintLine("\t" + dirToPrint.GetNumberOfFiles() + " File(s)");
            outputter.PrintLine("\t" + dirToPrint.GetNumberOfDirectories() + " Dir(s)");
        }
    }
}