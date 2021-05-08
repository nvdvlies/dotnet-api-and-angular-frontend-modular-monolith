﻿using Demo.Scaffold.Tool.Changes;
using Demo.Scaffold.Tool.Interfaces;
using Demo.Scaffold.Tool.Models;
using Demo.Scaffold.Tool.Scaffolders.InputCollectors;
using Demo.Scaffold.Tool.Scaffolders.OutputCollectors;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Demo.Scaffold.Tool.Scaffolders
{
    internal class ScaffolderService
    {
        private ScaffolderContext _context { get; set; }

        private List<IInputCollector> _inputCollectors = new List<IInputCollector>()
        {
            new SolutionDirectoryInputCollector(),
            new ScaffoldTypeInputCollector()
        };

        private List<IOutputCollector> _outputCollectors = new List<IOutputCollector>()
        {
            new ScaffolderTypeOutputCollector(),
        };

        private readonly AppSettings _appSettings;

        public ScaffolderService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Run()
        {
            _context = new ScaffolderContext(_appSettings);

            foreach (var inputCollector in _inputCollectors)
            {
                inputCollector.CollectInput(_context);
            }

            var changes = _outputCollectors
                .Select(x => x.CollectChanges(_context))
                .SelectMany(x => x)
                .Where(x => x != null)
                .ToList();

            if (ConfirmChanges(changes))
            {
                ApplyChanges(changes);
                Console.WriteLine("Done.");

                if (AnsiConsole.Confirm("Do you want to scaffold more?", false))
                {
                    Run();
                }
            }

            SaveUserAppSettings();
        }

        private bool ConfirmChanges(IEnumerable<IChange> changes)
        {
            Console.WriteLine();
            Console.WriteLine("Pending changes:");
            Console.WriteLine();
            foreach (var description in changes.Select(x => x.Description).Distinct().OrderBy(x => x))
            {
                Console.WriteLine($" - {description.Replace(_appSettings.PathToSolutionRootDirectory, string.Empty)}");
            }
            Console.WriteLine();
            return AnsiConsole.Confirm("Apply pending changes?"); 
        }

        private void ApplyChanges(IEnumerable<IChange> changes)
        {
            foreach (var change in changes)
            {
                change.Apply();
            }
        }

        private void SaveUserAppSettings()
        {
            var fullPathAndFileName = Path.Combine(Directory.GetCurrentDirectory(), Constants.UserSettingsFileName);
            var serializedAppSettings = JsonSerializer.Serialize(_context.AppSettings, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(fullPathAndFileName, serializedAppSettings);
        }
    }
}
