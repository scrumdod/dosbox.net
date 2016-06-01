// <copyright file="TestsForCmdDir.cs" company="Zuehlke Technology Group">
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
    public class TestsForCmdDir : FileSystemTestCase
    {
        [TestInitialize]
        public override void setUp()
        {
            base.setUp();
        }

        [TestMethod]
        public void testContent()
        {
            List<FileSystemItem> content;

            content = rootDir.Content;
            Assert.IsTrue(content.Count == 4); // subDir1, subDir2, file1InRoot, file2InRoot
            Assert.IsTrue(content.Contains(subDir1));
            Assert.IsTrue(content.Contains(subDir2));
            Assert.IsTrue(content.Contains(fileInRoot1));
            Assert.IsTrue(content.Contains(fileInRoot2));
            Assert.IsTrue(content.Contains(file1InDir1) == false);

            content = subDir1.Content;
            Assert.IsTrue(content.Count == 2); // file1InDir1, file2InDir1
            Assert.IsTrue(content.Contains(file1InDir1));
            Assert.IsTrue(content.Contains(file2InDir1));
            Assert.IsTrue(content.Contains(fileInRoot2) == false);

            content = subDir2.Content;
            Assert.IsTrue(content.Count == 0);
        }
    }
}