// <copyright file="DriveTest.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using DosBox.Filesystem;
using DosBox.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Filesystem
{
    [TestClass]
    public class DriveTest : FileSystemTestCase
    {
        [TestInitialize]
        public override void setUp()
        {
            base.setUp();
        }

        [TestMethod]
        public void testConstructor()
        {
            string driveName = "d";

            IDrive testdrive = new Drive(driveName);
            Assert.AreSame(testdrive.RootDirectory, testdrive.CurrentDirectory);
            Assert.IsTrue(testdrive.CurrentDirectory.Name.CompareTo("D:") == 0);
            Assert.IsTrue(testdrive.RootDirectory.Name.CompareTo("D:") == 0);
            Assert.IsTrue(testdrive.DriveName.CompareTo("D:") == 0);

            testdrive = new Drive("Hello");
            Assert.IsTrue(testdrive.DriveName.CompareTo("H:") == 0);
        }

        [TestMethod]
        public void testCurrentDirectory()
        {
            Assert.IsTrue(drive.CurrentDirectory.Name.CompareTo("C:") == 0);

            var subDir = new Directory("subDir");
            drive.RootDirectory.Add(subDir);
            drive.ChangeCurrentDirectory(subDir);
            Assert.AreSame(drive.CurrentDirectory, subDir);
        }

        [TestMethod]
        public void testGetItemFromPathWithAbsolutePaths()
        {
            string testpath;

            testpath = rootDir.Path;
            Assert.AreSame(drive.GetItemFromPath(testpath), rootDir);
            testpath = subDir1.Path;
            Assert.AreSame(drive.GetItemFromPath(testpath), subDir1);
            testpath = subDir2.Path;
            testpath = testpath.Replace('\\', '/');
            Assert.AreSame(drive.GetItemFromPath(testpath), subDir2);

            testpath = file2InDir1.Path;
            Assert.AreSame(drive.GetItemFromPath(testpath), file2InDir1);
            testpath = fileInRoot1.Path;
            Assert.AreSame(drive.GetItemFromPath(testpath), fileInRoot1);

            testpath = "g:\\gaga\\gugus";
            Assert.IsTrue(drive.GetItemFromPath(testpath) == null);

            testpath = "\\" + subDir1.Name;
            Assert.AreSame(drive.GetItemFromPath(testpath), subDir1);

            Assert.AreSame(drive.GetItemFromPath("C:\\subDir1"), subDir1);
            Assert.AreSame(drive.GetItemFromPath("c:\\subDir1"), subDir1);
            Assert.AreSame(drive.GetItemFromPath("c:/subDir1"), subDir1);
        }

        [TestMethod]
        public void testGetItemFromPathWithRelativePaths()
        {
            string subSubDirName = "SubSubDir1";
            var subSubDir1 = new Directory(subSubDirName);
            subDir1.Add(subSubDir1);

            drive.ChangeCurrentDirectory(subDir1);
            Assert.AreSame(drive.GetItemFromPath(subSubDirName), subSubDir1);
        }

        [TestMethod]
        public void testGetItemFromPathWithSpecialPaths()
        {
            // Path "\"
            Assert.AreSame(drive.GetItemFromPath("\\"), drive.RootDirectory);

            // Path ".."
            drive.ChangeCurrentDirectory(subDir2);
            Assert.AreSame(drive.GetItemFromPath(".."), subDir2.Parent);
            drive.ChangeCurrentDirectory(rootDir);
            Assert.AreSame(drive.GetItemFromPath(".."), rootDir);

            // Path "."
            drive.ChangeCurrentDirectory(subDir1);
            Assert.AreSame(drive.GetItemFromPath("."), subDir1);

            // Path ".\"
            drive.ChangeCurrentDirectory(subDir1);
            Assert.AreSame(drive.GetItemFromPath(".\\"), subDir1);

            // Path ".\subDir2"
            drive.ChangeCurrentDirectory(rootDir);
            Assert.AreSame(drive.GetItemFromPath(".\\subDir2"), subDir2);

            // Path "..\subDir1"
            drive.ChangeCurrentDirectory(subDir2);
            Assert.AreSame(drive.GetItemFromPath("..\\subDir1"), subDir1);

            // Path ".\..\subDir1"
            drive.ChangeCurrentDirectory(subDir2);
            Assert.AreSame(drive.GetItemFromPath(".\\..\\subDir1"), subDir1);
        }

        [TestMethod]
        public void testSingleCharacterDirectories()
        {
            Assert.IsTrue(rootDir.GetNumberOfDirectories() == 2);

            var newDir = new Directory("N");
            rootDir.Add(newDir);
            Assert.IsTrue(rootDir.GetNumberOfDirectories() == 3);

            FileSystemItem item = drive.GetItemFromPath(rootDir.Path + "\\N");
            Assert.IsNotNull(item);
            Assert.IsTrue(item.IsDirectory());
            Assert.AreSame(item, newDir);
        }
    }
}