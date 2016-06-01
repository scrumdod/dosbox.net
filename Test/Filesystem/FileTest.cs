// <copyright file="FileTest.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using System;
using DosBox.Filesystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Filesystem
{
    [TestClass]
    public class FileTest
    {
        private int fileLength;
        private File testfile;

        [TestInitialize]
        public void setUp()
        {
            testfile = new File("test", "test content");
            fileLength = (new File("test", "test content")).GetSize();
        }

        [TestMethod]
        public void testConstructor()
        {
            string name = "hello.txt";
            string content = "This is the content";

            testfile = new File(name, content);
            Assert.IsTrue(testfile.Name.CompareTo(name) == 0);
            Assert.IsTrue(testfile.FileContent.CompareTo(content) == 0);
        }

        [TestMethod]
        public void testForDirectory()
        {
            Assert.IsTrue(testfile.IsDirectory() == false);
        }

        [TestMethod]
        public void testRename()
        {
            testfile.Name = "NewName";
            Assert.IsTrue(testfile.Name.CompareTo("NewName") == 0);
        }

        [TestMethod]
        public void testRenameWithIllegalNames()
        {
            string defaultName = "default";
            testfile.Name = defaultName;
            Assert.IsTrue(testfile.Name.CompareTo(defaultName) == 0);

            try
            {
                testfile.Name = "Illegal\\Name";
                Assert.Fail(); // must throw an exception
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(testfile.Name.CompareTo(defaultName) == 0);
            }
        }

        [TestMethod]
        public void testFileSize()
        {
            Assert.AreEqual(fileLength, testfile.GetSize());
        }
    }
}