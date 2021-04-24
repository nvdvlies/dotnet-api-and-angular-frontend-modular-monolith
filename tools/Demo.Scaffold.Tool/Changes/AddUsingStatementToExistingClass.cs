﻿using System;
using System.IO;

namespace Demo.Scaffold.Tool.Changes
{
    internal class AddUsingStatementToExistingClass : BaseChange, IChange
    {
        public AddUsingStatementToExistingClass(string directory, string fileName, string content)
            : base(directory, fileName, content)
        {
        }

        public string Description => $"Modify: {RelativePathFromSolutionDirectory}";

        public void Apply()
        {
            string content = File.ReadAllText(DirectoryAndFileName);
            content = $"{Content}{Environment.NewLine}{content}";
            File.WriteAllText(DirectoryAndFileName, content);
        }
    }
}
