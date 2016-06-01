// <copyright file="CmdMkFileTest.cs" company="Zuehlke Technology Group">
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
    public class CmdMkFileTest : CmdTest
    {
        [TestInitialize]
        public void setUp()
        {
            base.SetUpBase();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdMkFile("mkfile", drive));
        }

        [TestMethod]
        public void CmdMkFile_CreatesNewFile()
        {
            // given
            const string newFileName = "testFile";

            // when
            ExecuteCommand("mkfile " + newFileName);

            // then
            Assert.AreEqual(numbersOfFilesBeforeTest + 1, drive.CurrentDirectory.GetNumberOfFiles());
            TestHelper.AssertOutputIsEmpty(testOutput);
            File createdFile = TestHelper.GetFile(drive, drive.CurrentDirectory.Path, newFileName);
            Assert.IsNotNull(createdFile);
        }

        [TestMethod]
        public void CmdMkFile_WithoutContent_CreatesEmptyFile()
        {
            // given
            const string newFileName = "testFile";

            // when
            ExecuteCommand("mkfile " + newFileName);

            // then
            File createdFile = TestHelper.GetFile(drive, drive.CurrentDirectory.Path, newFileName);
            Assert.AreEqual("", createdFile.FileContent);
        }

        [TestMethod]
        public void CmdMkFile_WithContent_CreatesFileWithContent()
        {
            // given
            const string newFileName = "testFile";
            const string newFileContent = "ThisIsTheContent";

            // when
            ExecuteCommand("mkfile " + newFileName + " " + newFileContent);

            // then
            Assert.AreEqual(numbersOfFilesBeforeTest + 1, drive.CurrentDirectory.GetNumberOfFiles());
            TestHelper.AssertOutputIsEmpty(testOutput);
            File createdFile = TestHelper.GetFile(drive, drive.CurrentDirectory.Path, newFileName);
            Assert.AreEqual(newFileContent, createdFile.FileContent);
        }

        [TestMethod]
        public void CmdMkFile_NoParameters_ReportsError()
        {
            ExecuteCommand("mkfile");
            Assert.AreEqual(numbersOfFilesBeforeTest, drive.CurrentDirectory.GetNumberOfFiles());
            TestHelper.AssertContains("syntax of the command is incorrect", testOutput.ToString());
        }
    }
}