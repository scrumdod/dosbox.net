// <copyright file="Drive.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;
using System.Collections.Generic;
using System.Text;
using DosBox.Interfaces;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the access to the composition.
    /// Composite-Pattern: Client<br/>
    /// <br/>
    /// Responsibilities:<br/>
    /// <li/> manipulates objects in the composition through the Component interface.<br/>
    /// <li/> owns the root directory which is the top of the directory tree.<br/>
    /// <li/> knows the current directory on which most of the commands must be performed.<br/>
    /// </summary>
    public class Drive : IDrive
    {
        private readonly string driveLetter;
        private readonly Directory rootDir;
        private Directory currentDir;

        /// <summary>
        /// Initializes a new instance of the <see cref="Drive"/> class. Creates a new drive and a root directory.
        /// </summary>
        /// <param name="driveLetter">
        /// Name of the drive. May only contain a single uppercase letter.
        /// If longer name given, only the first character is taken.
        /// </param>
        public Drive(string driveLetter)
        {
            this.driveLetter = driveLetter.Substring(0, 1);
            this.driveLetter = this.driveLetter.ToUpper();
            Label = string.Empty;
            rootDir = new Directory(this.driveLetter + ":");
            currentDir = rootDir;
        }

        #region IDrive Members

        /// <summary>
        /// Gets the root directory.
        /// </summary>
        /// <value>
        ///   Returns the object of the root directory.
        /// </value>
        public Directory RootDirectory
        {
            get { return rootDir; }
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <value>
        ///   Returns the object of the current directory.
        /// </value>
        public Directory CurrentDirectory
        {
            get { return currentDir; }
        }

        /// <summary>
        /// Changes the current directory. The given directory must be part of the drive's directory structure,
        /// otherwise the current directory remains unchanged.
        /// </summary>
        /// <param name="dir">Directory which should become the current directory</param>
        /// <returns>Returns true if directory found, false if directory does not exist.</returns>
        public bool ChangeCurrentDirectory(Directory dir)
        {
            if (GetItemFromPath(dir.Path) == dir)
            {
                currentDir = dir;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the name of the drive.
        /// </summary>
        /// <returns>Returns the drive name with a ending ':'. E.g. "C:".</returns>
        public string DriveName
        {
            get { return driveLetter + ":"; }
        }

        /// <summary>
        /// Gets the prompt.
        /// </summary>
        /// <returns>Returns the DOS-Prompt, drive name with an ending '>' and a space. E.g. "C:> "</returns>
        public string Prompt
        {
            get { return currentDir.Path + "> "; }
        }

        /// <summary>
        /// Changes the drive label of the drive.
        /// </summary>
        /// <value>The new drive label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Returns the object of a given path name.<br/>
        /// <br/>
        /// Example:<br/>
        /// getItemFromPath("C:\\temp\\aFile.txt");<br/>
        /// Returns the FileSystemItem-object which abstracts aFile.txt in the temp directory.<br/>
        /// <br/>
        /// Remarks:<br/>
        /// <li/> Always use "\\" for backslashes since the backslash is used as escape character for Java strings.
        /// <li/> This operation works for relative paths (temp\\aFile.txt) too. The lookup starts at the current directory.
        /// <li/> This operation works for forward slashes '/' too.
        /// <li/> ".." and "." are supported too.
        /// <br/>
        /// </summary>
        /// <param name="givenItemPath">Path for which the item shall be returned.</param>
        /// <returns>FileSystemObject or null if no path found.</returns>
        public FileSystemItem GetItemFromPath(string givenItemPath)
        {
            // Remove any "/" with "\"
            string givenItemPathPatched = givenItemPath.Replace('/', '\\');

            // Remove ending "\"
            givenItemPathPatched = givenItemPathPatched.Trim();
            if (givenItemPathPatched[givenItemPathPatched.Length - 1] == '\\'
                && givenItemPathPatched.Length >= 2)
            {
                givenItemPathPatched = givenItemPathPatched.Substring(0, givenItemPathPatched.Length - 1);
            }

            // Test for special paths
            if (givenItemPathPatched.CompareTo("\\") == 0)
            {
                return RootDirectory;
            }

            if (givenItemPathPatched.CompareTo("..") == 0)
            {
                Directory parent = CurrentDirectory.Parent ?? RootDirectory;
                return parent;
            }

            if (givenItemPathPatched.CompareTo(".") == 0)
            {
                return CurrentDirectory;
            }

            // Check for .\
            if (givenItemPathPatched.Length >= 2)
            {
                if (givenItemPathPatched.Substring(0, 2).CompareTo(".\\") == 0)
                {
                    givenItemPathPatched = givenItemPathPatched.Substring(2, givenItemPathPatched.Length - 2);
                }
            }

            // Check for ..\
            if (givenItemPathPatched.Length >= 3)
            {
                if (givenItemPathPatched.Substring(0, 3).CompareTo("..\\") == 0)
                {
                    var temp = new StringBuilder();
                    temp.Append(CurrentDirectory.Parent.Path);
                    temp.Append("\\");
                    temp.Append(givenItemPathPatched.Substring(3, givenItemPathPatched.Length - 3));
                    givenItemPathPatched = temp.ToString();
                }
            }

            // Add drive name if path starts with "\"
            if (givenItemPathPatched[0] == '\\')
            {
                givenItemPathPatched = driveLetter + ":" + givenItemPathPatched;
            }

            // Make absolute path from relative paths
            if (givenItemPathPatched.Length == 1 || givenItemPathPatched[1] != ':')
            {
                givenItemPathPatched = CurrentDirectory + "\\" + givenItemPathPatched;
            }

            // Find more complex paths recursive
            if (givenItemPathPatched.CompareTo(rootDir.Path) == 0)
            {
                return rootDir;
            }

            return GetItemFromDirectory(givenItemPathPatched, rootDir);
        }

        /// <summary>
        /// Stores a directory structure persistently.
        /// Is called when "exit" is called.
        /// Hint: Stores the object stream in a fixed file
        /// </summary>
        public void Save()
        {
            // Not yet implemented
        }

        /// <summary>
        /// Creates a directory structure from an object stream.Is called at startup of the application.
        /// </summary>
        public void Restore()
        {
            // Not yet implemented
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// Same as getDriveName()
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return DriveName;
        }

        /// <summary>
        /// Helper for getItemFromPath()
        /// </summary>
        private FileSystemItem GetItemFromDirectory(string givenItemName, Directory directoryToLookup)
        {
            List<FileSystemItem> content = directoryToLookup.Content;

            foreach (FileSystemItem item in content)
            {
                string pathName = item.Path;
                if (pathName.Equals(givenItemName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }

                if (item.IsDirectory())
                {
                    FileSystemItem retVal = GetItemFromDirectory(givenItemName, (Directory) item);
                    if (retVal != null)
                    {
                        return retVal;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Builds up a directory structure from the given path on a real drive.
        /// Subdirectories become directories and subdirectories
        /// Files in that directory and the subdirectories become files, content is set to
        /// full path, filename and size of that file.
        /// <br/>
        /// Example:<br/>
        /// C:\temp<br/>
        /// +-- MyFile1.txt (size 112000 Bytes)<br/>
        /// +-- MyFile2.txt (50000)<br/>
        /// +-- SubDir1 (Dir)<br/>
        /// ....+-- AnExecutable.exe (1234000)<br/>
        /// ....+-- ConfigFiles (Dir)<br/>
        /// <br/>
        /// Results in<br/>
        /// <li/> All files and subdirectories of the root directory deleted
        /// <li/> Current directory set to root directory
        /// <li/> File MyFile1.txt added to root directory with content "C:\temp\MyFile1.txt, size 112000 Bytes"
        /// <li/> File MyFile2.txt added to root directory with content "C:\temp\MyFile2.txt, size 50000 Bytes"
        /// <li/> Directory SubDir1 added to root directory
        /// <li/> File AnExecutable.exe added to SubDir1 with content "C:\temp\SubDir1\AnExecutable.exe, size 1234000 Bytes"
        /// <li/> Directory ConfigFiles added to SubDir1
        /// <br/>
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateFromRealDirectory(string path)
        {
            // Not yet implemented
        }
    }
}