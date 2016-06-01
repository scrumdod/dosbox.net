// <copyright file="CmdDirTest.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using DosBox.Command.Library;
using DosBoxTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Command.Library
{
    [TestClass]
    public class CmdDirTest : CmdTest
    {
        [TestInitialize]
        public void setUp()
        {
            base.SetUpBase();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdDir("dir", drive));
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintPathOfCurrentDirectory()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains(rootDir.Path, testOutput);
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintFiles()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains(fileInRoot1.Name, testOutput);
            TestHelper.AssertContains(fileInRoot2.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintDirectories()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains(subDir1.Name, testOutput);
            TestHelper.AssertContains(subDir2.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintsFooter()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains("2 File(s)", testOutput);
            TestHelper.AssertContains("2 Dir(s)", testOutput);
        }

        [TestMethod]
        public void CmdDir_PathAsParameter_PrintGivenPath()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir c:\\subDir1");
            TestHelper.AssertContains(subDir1.Path, testOutput);
        }

        [TestMethod]
        public void CmdDir_PathAsParameter_PrintFilesInGivenPath()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir c:\\subDir1");
            TestHelper.AssertContains(file1InDir1.Name, testOutput);
            TestHelper.AssertContains(file2InDir1.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_PathAsParameter_PrintsFooter()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir c:\\subDir1");
            TestHelper.AssertContains("2 File(s)", testOutput);
            TestHelper.AssertContains("0 Dir(s)", testOutput);
        }
    }
}