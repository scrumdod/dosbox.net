// <copyright file="IDrive.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;
using DosBox.Filesystem;

namespace DosBox.Interfaces
{
    public interface IDrive
    {
        /// <summary>
        /// Access to the drive's root directory.
        /// </summary>
        Directory RootDirectory { get; }

        /// <summary>
        /// Access to the currently active directory on this drive.
        /// </summary>
        Directory CurrentDirectory { get; }

        /// <summary>
        /// Drive Lable, as used in command VOL and LABEL
        /// </summary>
        string Label { set; get; }

        /// <summary>
        /// E.g. "C:"
        /// </summary>
        String DriveName { get; }

        /// <summary>
        /// Current Directory + ">"
        /// </summary>
        String Prompt { get; }

        /// <summary>
        /// Changes the currently active directory. New directory must be within this drive,
        /// otherwise the current directory is not changed.
        /// </summary>
        /// <param name="newCurrentDirectory"></param>
        /// <returns></returns>
        bool ChangeCurrentDirectory(Directory newCurrentDirectory);

        /// <summary>
        /// Returns the object of a given path name.
        /// 
        /// Example:
        /// getItemFromPath("C:\\temp\\aFile.txt");
        /// Returns the FileSystemItem-object which abstracts aFile.txt in the temp directory.
        /// 
        /// Remarks:
        /// - Always use "\\" for backslashes since the backslash is used as escape character for Java strings.
        /// - This operation works for relative paths (temp\\aFile.txt) too. The lookup starts at the current directory.
        /// - This operation works for forward slashes '/' too.
        /// - ".." and "." are supported too.
        ///
        /// </summary>
        /// <param name="givenItemPath">Path for which the item shall be returned.</param>
        /// <returns>FileSystemObject or null if no path found.</returns>
        FileSystemItem GetItemFromPath(String givenItemPath);

        /// <summary>
        /// Stores the current directory structure persistently.
        /// </summary>
        void Save();

        /// <summary>
        /// Creates a directory structure from the stored structure. The current directory structure is deleted.
        /// </summary>
        void Restore();
    }
}