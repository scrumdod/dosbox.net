// <copyright file="Program.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary>

using System;
using DosBox.Configuration;

namespace DosBox
{
    /// <summary>
    /// Helper class to start the program
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// Prevents a default instance of the Program class from being created.
        /// </summary>
        private Program()
        {
        }

        /// <summary>
        /// Main method. Starts the DosBox application.
        /// </summary>
        public static void Main()
        {
            var config = new Configurator();
            config.ConfigurateSystem();

            String message = "hello client1!";
        }
    }
}