// <copyright file="Configurator.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using DosBox.Command.Library;
using DosBox.Console;
using DosBox.Filesystem;
using DosBox.Interfaces;
using DosBox.Invoker;

namespace DosBox.Configuration
{
    /// <summary>
    /// Configures the system.
    /// </summary>
    public class Configurator
    {
        /// <summary>
        /// Configurates the system for a console application.
        /// </summary>
        public void ConfigurateSystem()
        {
            // Create file system with initial root directory
            // and read any persistent information.
            IDrive drive = new Drive("C");
            drive.Restore();

            // Create all commands and invoker
            var factory = new CommandFactory(drive);
            var commandInvoker = new CommandInvoker();
            commandInvoker.SetCommands(factory.CommandList);
            IExecuteCommand invoker = commandInvoker;

            // Setup console for input and output
            var console = new ConsoleEx(invoker, drive);

            // Start console
            console.ProcessInput();
        }
    }
}