// <copyright file="TestsForMove.cs" company="Zuehlke Technology Group">
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
    public class TestsForMove : FileSystemTestCase
    {
        [TestInitialize]
        public override void setUp()
        {
            base.setUp();
        }

        [TestMethod]
        public void testFileMove()
        {
            List<FileSystemItem> content;

            // Check Preconditions
            Assert.IsTrue(file1InDir1.Path.CompareTo("C:\\subDir1\\File1InDir1") == 0);
            Assert.AreSame(file1InDir1.Parent, subDir1);
            content = subDir2.Content;
            Assert.IsTrue(content.Contains(file1InDir1) == false);
            content = subDir1.Content;
            Assert.IsTrue(content.Contains(file1InDir1));

            // Do move
            subDir2.Add(file1InDir1);

            // Check Postconditions
            Assert.IsTrue(file1InDir1.Path.CompareTo("C:\\subDir2\\File1InDir1") == 0);
            Assert.AreSame(file1InDir1.Parent, subDir2);
            content = subDir2.Content;
            Assert.IsTrue(content.Contains(file1InDir1));
            content = subDir1.Content;
            Assert.IsTrue(content.Contains(file1InDir1) == false);
        }
    }
}