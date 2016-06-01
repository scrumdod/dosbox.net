// <copyright file="DirectoryTest.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using System.Collections.Generic;
using DosBox.Filesystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Filesystem
{
    [TestClass]
    public class DirectoryTest
    {
        private Directory rootDir;
        private Directory subDir1;
        private Directory subDir2;

        [TestInitialize]
        public void setUp()
        {
            rootDir = new Directory("root");
            subDir1 = new Directory("subDir1");
            rootDir.Add(subDir1);
            subDir2 = new Directory("subDir2");
            rootDir.Add(subDir2);
        }

        [TestMethod]
        public void testConstructor()
        {
            string name = "root";
            var testdir = new Directory(name);
            Assert.IsTrue(testdir.Name.CompareTo(name) == 0);
            Assert.IsTrue(testdir.Content.Count == 0);
            Assert.IsTrue(testdir.Parent == null);
        }

        [TestMethod]
        public void testSubDirectory()
        {
            List<FileSystemItem> content = rootDir.Content;
            Assert.IsTrue(content != null);
            Assert.IsTrue(content.Count == 2);

            FileSystemItem item = content[0];
            Assert.IsTrue(item.IsDirectory());
            Assert.IsTrue(item.Name.CompareTo(subDir1.Name) == 0);

            FileSystemItem parent = item.Parent;
            Assert.IsTrue(parent.IsDirectory());
            Assert.AreSame(parent, rootDir);

            Assert.IsTrue(parent.Parent == null);

            string path = item.Path;
            Assert.IsTrue(path.CompareTo(rootDir.Name + "\\" + subDir1.Name) == 0);
        }

        [TestMethod]
        public void testContainingFiles()
        {
        }

        [TestMethod]
        public void testForDirectory()
        {
            Assert.IsTrue(rootDir.IsDirectory());
            Assert.IsTrue(subDir2.IsDirectory());
        }

        [TestMethod]
        public void testRename()
        {
            subDir1.Name = "NewName";
            Assert.IsTrue(subDir1.Name.CompareTo("NewName") == 0);
        }

        [TestMethod]
        public void testNumberOfFilesAndDirectories()
        {
            // TODO
        }
    }
}