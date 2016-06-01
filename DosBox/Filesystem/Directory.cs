// <copyright file="Directory.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System.Collections.Generic;
using System.Linq;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the behavior of concrete directories.
    /// Composite-Pattern: Composite<br/>
    /// <br/>
    /// Responsibilities:<br/>
    /// <li/> defines behavior for components (directories) having children. 
    /// <li/> stores child components (files and subdirectories). 
    /// <li/> implements child-related operations in the Component interface. These are:
    /// <blockquote>
    /// - getContent()<br/>
    /// - add(Directory), add(File)<br/>
    /// - getNumberOfFiles(), getNumberOfDirectories()<br/>
    /// </blockquote>
    /// </summary>
    public class Directory : FileSystemItem
    {
        private readonly List<FileSystemItem> content;

        /// <summary>
        /// Initializes a new instance of the <see cref="Directory"/> class.
        /// </summary>
        /// <param name="name">Name of the new directory. May not contain any ':', '/', ',', ' ' or '\' in the name.</param>
        public Directory(string name)
            : base(name, null)
        {
            content = new List<FileSystemItem>();
        }

        /// <summary>
        /// An array list with the content of this directory (files and subdirectories) is returned.
        /// The returned list may not be changed, only read access is allowed.
        /// Returns the content of the item.
        /// </summary>
        /// <returns>the list of contained files and directories if isDirectory() == true - null if isDirectory() == false</returns>
        public override List<FileSystemItem> Content
        {
            get { return content; }
        }

        /// <summary>
        /// Returns that this item is a directory.
        /// </summary>
        /// <returns>Returns always true.</returns>
        public override bool IsDirectory()
        {
            return true;
        }

        /// <summary>
        /// Adds a new subdirectory to this directory.
        /// If the directory to add is already part of another directory structure,
        /// it is removed from there.
        /// </summary>
        /// <param name="subDir">Object of the directory to be added.</param>
        public void Add(Directory subDir)
        {
            content.Add(subDir);
            if (subDir.Parent != null)
            {
                subDir.Parent.content.Remove(subDir);
            }

            subDir.Parent = this;
        }

        /// <summary>
        /// Adds a new file to this directory.
        /// If the file to add is already part of another directory structure,
        /// it is removed from there.
        /// </summary>
        /// <param name="file">Object of the file to be added.</param>
        public void Add(File file)
        {
            content.Add(file);
            if (file.Parent != null)
            {
                file.Parent.content.Remove(file);
            }

            file.Parent = this;
        }

        /// <summary>
        /// Removes a directory or a file from current directory.
        /// Sets the parent of the removed item to null, if contained in this directory.
        /// Note: If you need to remove the entire content, you cannot use
        /// an iterator since you change the list, the iterator is enumerating. 
        /// Use this code instead:
        /// while(root.getContent().size() > 0) 
        /// {
        ///   root.remove(root.getContent().get(0));
        /// }
        /// </summary>
        /// <param name="item">
        /// Directory or file to be removed from this directory. 
        /// If item is not part of this directory, nothing happens.
        /// </param>
        public void remove(FileSystemItem item)
        {
            if (content.Contains(item))
            {
                item.Parent = null;
                content.Remove(item);
            }
        }

        /// <summary>
        /// Returns the number of files in this directory. Does not count number of files in the subdirectories.
        /// </summary>
        /// <returns>Returns the number of files contained by this item</returns>
        public override int GetNumberOfFiles()
        {
            return content.Count(item => !item.IsDirectory());
        }

        /// <summary>
        /// Returns the number of subdirectories in this directory. Does not count number of subdirectories in the subdirectories.
        /// </summary>
        /// <returns>Returns the number of directories contained by this item.</returns>
        public override int GetNumberOfDirectories()
        {
            return content.Count(item => item.IsDirectory());
        }

        /// <summary>
        /// Returns 0 since directories have no size.
        /// </summary>
        /// <returns>Returns 0.</returns>
        public override int GetSize()
        {
            return 0;
        }
    }
}