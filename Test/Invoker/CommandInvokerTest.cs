// <copyright file="CommandInvokerTest.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using System.Collections.Generic;
using DosBox.Filesystem;
using DosBox.Interfaces;
using DosBox.Invoker;
using DosBoxTest.Command.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Invoker
{
    [TestClass]
    public class CommandInvokerTest
    {
        private CommandInvoker commandInvoker;
        private TestOutput output;
        private TestCommand testcmd;

        [TestInitialize]
        public void setUp()
        {
            IDrive drive = new Drive("C");
            commandInvoker = new CommandInvoker();
            testcmd = new TestCommand("dIR", drive);
            commandInvoker.AddCommand(testcmd);

            output = new TestOutput();
        }

        [TestMethod]
        public void testParseCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName(""), "");
            Assert.AreEqual(commandInvoker.ParseCommandName("dir"), "dir");
            Assert.AreEqual(commandInvoker.ParseCommandName("DIR"), "dir");
            Assert.AreEqual(commandInvoker.ParseCommandName("dir param1"), "dir");
            Assert.AreEqual(commandInvoker.ParseCommandName("dir,param1, param2"), "dir");
            Assert.AreEqual(commandInvoker.ParseCommandName("dir,"), "dir");
            Assert.AreEqual(commandInvoker.ParseCommandName("dir   "), "dir");
            Assert.AreEqual(commandInvoker.ParseCommandName("dir o"), "dir");
        }

        [TestMethod]
        public void testParseParameters()
        {
            List<string> parameters;

            parameters = commandInvoker.ParseCommandParameters("dir");
            Assert.IsTrue(parameters.Count == 0);

            parameters = commandInvoker.ParseCommandParameters("dir /p");
            Assert.IsTrue(parameters.Count == 1);
            Assert.IsTrue(parameters[0].CompareTo("/p") == 0);

            parameters = commandInvoker.ParseCommandParameters("dir /p param2");
            Assert.IsTrue(parameters.Count == 2);
            Assert.IsTrue(parameters[0].CompareTo("/p") == 0);
            Assert.IsTrue(parameters[1].CompareTo("param2") == 0);

            parameters = commandInvoker.ParseCommandParameters("dir    /p");
            Assert.IsTrue(parameters.Count == 1);
            Assert.IsTrue(parameters[0].CompareTo("/p") == 0);

            parameters = commandInvoker.ParseCommandParameters("dir  param1  param2   ");
            Assert.IsTrue(parameters.Count == 2);
            Assert.IsTrue(parameters[0].CompareTo("param1") == 0);
            Assert.IsTrue(parameters[1].CompareTo("param2") == 0);

            parameters = commandInvoker.ParseCommandParameters("d 1 2");
            Assert.IsTrue(parameters.Count == 2);
            Assert.IsTrue(parameters[0].CompareTo("1") == 0);
            Assert.IsTrue(parameters[1].CompareTo("2") == 0);
        }

        [TestMethod]
        public void testCommandExecuteSimple()
        {
            commandInvoker.ExecuteCommand("DIR", output);

            Assert.IsTrue(testcmd.executed);
        }

        [TestMethod]
        public void testCommandExecuteWithLeadingSpace()
        {
            commandInvoker.ExecuteCommand("   DIR", output);

            Assert.IsTrue(testcmd.executed);
        }

        [TestMethod]
        public void testCommandExecuteWithEndingSpace()
        {
            commandInvoker.ExecuteCommand("DIR   ", output);

            Assert.IsTrue(testcmd.executed);
        }

        [TestMethod]
        public void testCommandExecuteWIthDifferentCase()
        {
            commandInvoker.ExecuteCommand("dir", output);

            Assert.IsTrue(testcmd.executed);
        }

        [TestMethod]
        public void testCommandExecuteWithParameters()
        {
            commandInvoker.ExecuteCommand("dir param1 param2", output);

            Assert.IsTrue(testcmd.executed);
            Assert.IsTrue(testcmd.getParams().Count == 2);
            Assert.IsTrue(testcmd.numberOfParamsPassed == 2);
        }

        [TestMethod]
        public void testCommandExecuteWithWrongParameters1()
        {
            testcmd.checkNumberOfParametersReturnValue = false;
            commandInvoker.ExecuteCommand("dir param1 param2", output);

            Assert.IsTrue(testcmd.executed == false);
            Assert.IsTrue(testcmd.getParams().Count == 2);
            Assert.IsTrue(testcmd.numberOfParamsPassed == 2);
            Assert.IsTrue(output.ToString().ToLower().Contains("wrong"));
            Assert.IsTrue(output.ToString().ToLower().Contains("parameter"));
        }
    }
}