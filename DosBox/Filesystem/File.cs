// <copyright file="File.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System.Collections.Generic;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the behavior of concrete files. 
    /// Composite-Pattern: Leaf<br/>
    /// <br/>
    /// Responsibilities:<br/>
    /// <li/> represents leaf objects in the composition. A leaf has no children. 
    /// <li/> defines behavior for primitive objects in the composition. 
    /// </summary>
    public class File : FileSystemItem
    {
        private readonly string fileContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        /// <param name="name">A name for the file. Note that file names may not contain '\' '/' ':' ',' ';' and ' '.</param>
        /// <param name="fileContent">
        /// Any string which represents the content of the file.
        /// The content may not contain characters like ',' and ';'.
        /// </param>
        public File(string name, string fileContent)
            : base(name, null)
        {
            this.fileContent = fileContent;
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <returns>Returns file content as string</returns>
        public string FileContent
        {
            get { return fileContent; }
        }

        /// <summary>
        /// Null is returned since a files does not contain any subitems.
        /// </summary>
        /// <returns>Returns always null.</returns>
        public override List<FileSystemItem> Content
        {
            get { return null; }
        }

        /// <summary>
        /// False is returned since a file is not a directory.
        /// </summary>
        /// <returns>Returns always false.</returns>
        public override bool IsDirectory()
        {
            return false;
        }

        /// <summary>
        /// 0 is returned since a files does not contain other files.
        /// </summary>
        /// <returns>Returns always 0.</returns>
        public override int GetNumberOfFiles()
        {
            return 0;
        }

        /// <summary>
        /// 0 is returned since a files does not contain directories.
        /// </summary>
        /// <returns>Returns always 0.</returns>
        public override int GetNumberOfDirectories()
        {
            return 0;
        }

        /// <summary>
        /// Returns the size of the file.
        /// </summary>
        /// <returns>Stringlength of file content string</returns>
        public override int GetSize()
        {
            return fileContent.Length;
        }
    }
}