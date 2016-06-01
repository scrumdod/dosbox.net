// <copyright file="Path.cs" company="Zuehlke Technology Group">
// Copyright (c) 2010 All Right Reserved
// </copyright>
// <author>Rainer Grau, Daniel Tobler, Roman Niederberger</author>
// <date>2010-01-19</date>
// <summary>Course Agile Software Development</summary> 

using System;

namespace DosBoxTest.Helpers
{
    public class Path
    {
        public static String Combine(String path1, String path2)
        {
            return path1 + "\\" + path2;
        }
    }
}