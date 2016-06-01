// -----------------------------------------------------------------------
// <copyright file="TestHelper.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using DosBox.Filesystem;
using DosBox.Interfaces;
using DosBoxTest.Command.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Helpers
{
    public static class TestHelper
    {
        public static void AssertOutputIsEmpty(TestOutput testOutput)
        {
            Assert.IsNotNull(testOutput);
            Assert.AreEqual(0, testOutput.ToString().Length);
        }

        public static File GetFile(IDrive drive, string filePath, string fileName)
        {
            FileSystemItem fileSystemItem = drive.GetItemFromPath(filePath + "\\" + fileName);
            Assert.IsNotNull(fileSystemItem);
            Assert.AreEqual(fileName, fileSystemItem.Name);
            Assert.IsFalse(fileSystemItem.IsDirectory());
            return (File) fileSystemItem;
        }

        public static Directory GetDirectory(IDrive drive, string directoryPath, string directoryName)
        {
            FileSystemItem fileSystemItem = drive.GetItemFromPath(directoryPath);
            Assert.IsNotNull(fileSystemItem);
            Assert.AreEqual(directoryName, fileSystemItem.Name);
            Assert.IsTrue(fileSystemItem.IsDirectory());
            return (Directory) fileSystemItem;
        }

        public static void AssertContains(string expectedToContain, string actualShouldContain)
        {
            Assert.IsTrue(actualShouldContain.Contains(expectedToContain));
        }

        public static void AssertContains(string expectedToContain, IOutputter actualShouldContain)
        {
            AssertContains(expectedToContain, actualShouldContain.ToString());
        }

        public static void AssertCurrentDirectoryIs(IDrive drive, Directory expectedDirectory)
        {
            Assert.AreSame(expectedDirectory, drive.CurrentDirectory);
            Assert.AreEqual(expectedDirectory.Path, drive.CurrentDirectory.Path);
        }
    }
}