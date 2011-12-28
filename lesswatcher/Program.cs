using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using dotless.Core;
using lessnet = dotless.Core;

namespace lesswatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory();

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(filePath, "*.less");
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);

            ConvertAtStartup(filePath);
            fileSystemWatcher.EnableRaisingEvents = true;

            while (true)
            {
                Console.Read();
            }
        }

        static void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                ((FileSystemWatcher) sender).EnableRaisingEvents = false;

                Console.WriteLine(LessParser.FileChangeDetect, FilterFileName(e.FullPath));

                if (LessParser.Convert(e.FullPath))
                    Console.WriteLine(LessParser.MsgSuccess);
                else
                    Console.WriteLine(LessParser.MsgFailure, FilterFileName(e.FullPath));
            }
            finally
            {
                ((FileSystemWatcher) sender).EnableRaisingEvents = true;
            }
        }

        static string FilterFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        static void ConvertAtStartup(string filePath)
        {
            foreach (var lessfile in Directory.GetFiles(filePath, "*.less"))
            {
                Console.WriteLine(LessParser.ParseMessage, FilterFileName(lessfile));

                if (LessParser.Convert(lessfile))
                    Console.WriteLine(LessParser.MsgSuccess);
            }
        }
    }

    class LessParser
    {
        public const string ParseMessage = "Parsing file: '{0}'";

        public const string MsgSuccess = "File parsed and converted successfully.";
        public const string MsgFailure = "An error occured while parsing file '{0}'.";

        public const string FileChangeDetect = "File change detected: '{0}'";
        public const string FileEmpty = "No CSS code found to parse or file is empty.";
        public const string FileError = "Error '{0}' on file '{1}'";

        public static bool Convert(string file)
        {
            string outputfilepath = file.Replace(".less", ".lw.css");

            Thread.Sleep(750);
            string parsed;

            try
            {
                parsed = Less.Parse(File.ReadAllText(file));
            }
            catch (Exception exception)
            {
                Console.WriteLine(FileError, exception.Message, Path.GetFileName(file));
                return false;
            }

            if (String.IsNullOrEmpty(parsed))
            {
                Console.WriteLine(FileEmpty);
                return false;
            }
            else
            {
                File.WriteAllText(outputfilepath, parsed);
                return true;
            }
        }
    }
}
