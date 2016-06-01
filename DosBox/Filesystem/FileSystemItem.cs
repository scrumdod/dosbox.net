// <copyright file="FileSystemItem.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;
using System.Collections.Generic;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class abstracts File and Directory.<br/>
    /// Composite-Pattern: Component<br/>
    /// <br/>
    /// Responsibilities:<br/>
    /// <li/> declares the common interface for objects in the composition. This is:
    /// <blockquote>
    ///    - get/setName(), tostring()<br/>
    ///    - Path<br/>
    ///    - isDirectory()<br/>
    ///    - getNumberOfFiles(), getNumberOfDirectories()<br/>
    ///    - getSize()
    /// </blockquote>    
    /// <li/> implements default behavior for the interface common to all classes, as appropriate. 
    /// <li/> declares an interface for accessing and managing its child components. This is
    /// <blockquote>
    ///    - getContent()
    /// </blockquote>
    /// <li/> defines an interface for accessing a component's parent in the recursive structure,
    ///   and implements it if that's appropriate. This is
    /// <blockquote>  
    ///    - getParent()
    /// </blockquote> 
    /// </summary>
    public abstract class FileSystemItem
    {
        private const string ILLEGAL_ARGUMENT_TEXT =
            "Error: A file or directory name may not contain '/', '\', ',', ' ' or ':'";

        private string name;
        private Directory parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemItem"/> class.
        /// </summary>
        /// <param name="name">The name of current the item.</param>
        /// <param name="parent">The parent instance of the current item.</param>
        protected FileSystemItem(string name, Directory parent)
        {
            if (CheckName(name) == false)
            {
                throw new ArgumentException(ILLEGAL_ARGUMENT_TEXT);
            }

            this.name = name;
            this.parent = parent;
        }

        /// <summary>
        /// Gets or sets the name.
        /// New name of the item. May not contain any ':', '/', ',', ' ' or '\' in the name. 
        /// Otherwise, the name is not changed and an ArgumentException is thrown. 
        /// </summary>
        /// <value>The name of the item.</value>
        public string Name
        {
            get { return name; }

            set
            {
                if (CheckName(value) == false)
                {
                    throw new ArgumentException(ILLEGAL_ARGUMENT_TEXT);
                }

                name = value;
            }
        }

        /// <summary>
        /// Gets the full path of the item.
        /// </summary>
        /// <returns>Full path, e.g. "C:\thisdir\thatdir\file.txt"</returns>
        public string Path
        {
            get
            {
                string path;
                if (parent != null)
                {
                    path = parent.Path + "\\" + name;
                }
                else
                {
                    // For root directory
                    path = name;
                }

                return path;
            }
        }

        /// <summary>
        /// Returns the content of the item.
        /// </summary>
        /// <returns>
        /// - the list of contained files and directories if isDirectory() == true
        /// - null if isDirectory() == false
        /// </returns>
        public abstract List<FileSystemItem> Content { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public Directory Parent
        {
            get { return parent; }
            internal set { parent = value; }
        }

        /// <summary>
        /// Checks whether a file or directory name contains illegal characters.
        /// </summary>
        /// <param name="nameToCheck">Name to check.</param>
        /// <returns>
        /// - true name is valid
        /// - false name contains at least one illegal character.
        /// </returns>
        protected bool CheckName(string nameToCheck)
        {
            return !nameToCheck.Contains("\\") && !nameToCheck.Contains("/") && !nameToCheck.Contains(",") &&
                   !nameToCheck.Contains(" ");
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the full path of the item.
        /// See Path
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// Determines whether this instance is directory.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is directory; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsDirectory();

        /// <summary>
        /// Returns the number of files contained by this item.
        /// </summary>
        /// <returns>
        /// number of contained files if isDirectory() == true, 0 if isDirectory() == false</returns>
        public abstract int GetNumberOfFiles();

        /// <summary>
        /// Returns the number of directories contained by this item
        /// </summary>
        /// <returns>
        /// number of contained directories if isDirectory() == true, 0 if isDirectory() == false</returns>
        public abstract int GetNumberOfDirectories();

        /// <summary>
        /// Returns the size of the item.
        /// </summary>
        /// <returns>the size in bytes of the file (string length of the content) if isDirectory() == false, 0 if isDirectory() == true</returns>
        public abstract int GetSize();
    }
}