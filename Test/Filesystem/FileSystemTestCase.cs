// <copyright file="FileSystemTestCase.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBoxTest.Filesystem
{
    public abstract class FileSystemTestCase
    {
        protected IDrive drive;
        protected File file1InDir1;
        protected File file2InDir1;
        protected File fileInRoot1;
        protected File fileInRoot2;
        protected Directory rootDir;
        protected Directory subDir1;
        protected Directory subDir2;

        public virtual void setUp()
        {
            drive = new Drive("C");
            rootDir = drive.RootDirectory;
            fileInRoot1 = new File("FileInRoot1", "");
            rootDir.Add(fileInRoot1);
            fileInRoot2 = new File("FileInRoot2", "");
            rootDir.Add(fileInRoot2);

            subDir1 = new Directory("subDir1");
            rootDir.Add(subDir1);
            file1InDir1 = new File("File1InDir1", "");
            subDir1.Add(file1InDir1);
            file2InDir1 = new File("File2InDir1", "");
            subDir1.Add(file2InDir1);

            subDir2 = new Directory("subDir2");
            rootDir.Add(subDir2);
        }
    }
}