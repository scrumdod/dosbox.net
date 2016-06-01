// <copyright file="TestCommand.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBoxTest.Invoker
{
    public class TestCommand : DosCommand
    {
        public bool checkNumberOfParametersReturnValue = true;
        public bool checkParameterValuesReturnValue = true;
        public bool executed;
        public int numberOfParamsPassed = -1;

        public TestCommand(string cmdName, IDrive drive)
            : base(cmdName, drive)
        {
        }

        public List<string> getParams()
        {
            return GetParameters();
        }

        public override void Execute(IOutputter IOutputter)
        {
            executed = true;
        }

        protected override bool CheckNumberOfParameters(int number)
        {
            numberOfParamsPassed = number;
            return checkNumberOfParametersReturnValue;
        }

        protected override bool CheckParameterValues(IOutputter IOutputter)
        {
            return checkParameterValuesReturnValue;
        }
    }
}