// <copyright file="CmdMkDirTest.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using DosBox.Command.Library;
using DosBox.Filesystem;
using DosBoxTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Command.Library
{
    [TestClass]
    public class CmdMkDirTest : CmdTest
    {
        [TestInitialize]
        public void setUp()
        {
            base.SetUpBase();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdMkDir("mkdir", drive));
        }

        [TestMethod]
        public void CmdMkDir_CreateNewDirectory_NewDirectoryIsAdded()
        {
            const string testDirName = "test1";
            ExecuteCommand("mkdir " + testDirName);
            Directory testDirectory = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveName, testDirName),
                                                              testDirName);
            Assert.AreSame(drive.RootDirectory, testDirectory.Parent);
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 1, drive.RootDirectory.GetNumberOfDirectories());
            TestHelper.AssertOutputIsEmpty(testOutput);
        }

        [TestMethod]
        public void CmdMkDir_CreateNewDirectory_NewDirectoryIsAddedToCorrectLocation()
        {
            const string testDirName = "test1";
            ExecuteCommand("mkdir " + testDirName);
            Directory testDirectory = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveName, testDirName),
                                                              testDirName);
            Assert.AreSame(drive.RootDirectory, testDirectory.Parent);
        }

        [TestMethod]
        public void CmdMkDir_SingleLetterDirectory_NewDirectoryIsAdded()
        {
            const string testDirName = "a";
            ExecuteCommand("mkdir " + testDirName);
            Directory testDirectory = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveName, testDirName),
                                                              testDirName);
            Assert.AreSame(drive.RootDirectory, testDirectory.Parent);
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 1, drive.RootDirectory.GetNumberOfDirectories());
            TestHelper.AssertOutputIsEmpty(testOutput);
        }

        [TestMethod]
        public void CmdMkDir_NoParameters_ErrorMessagePrinted()
        {
            ExecuteCommand("mkdir");
            Assert.AreEqual(numbersOfDirectoriesBeforeTest, drive.RootDirectory.GetNumberOfDirectories());
            TestHelper.AssertContains("syntax of the command is incorrect", testOutput);
        }

        [TestMethod]
        public void CmdMkDir_SeveralParameters_SeveralNewDirectoriesCreated()
        {
            // given
            const string testDirName1 = "test1";
            const string testDirName2 = "test2";
            const string testDirName3 = "test3";

            // when
            ExecuteCommand("mkdir " + testDirName1 + " " + testDirName2 + " " + testDirName3);

            // then
            Directory directory1 = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveName, testDirName1),
                                                           testDirName1);
            Directory directory2 = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveName, testDirName2),
                                                           testDirName2);
            Directory directory3 = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveName, testDirName3),
                                                           testDirName3);
            Assert.AreSame(directory1.Parent, drive.RootDirectory);
            Assert.AreSame(directory2.Parent, drive.RootDirectory);
            Assert.AreSame(directory3.Parent, drive.RootDirectory);
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 3, drive.RootDirectory.GetNumberOfDirectories());
            TestHelper.AssertOutputIsEmpty(testOutput);
        }
    }
}