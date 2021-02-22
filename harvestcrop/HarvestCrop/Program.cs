using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestCrop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Config.banner);
            Console.WriteLine("[*] Author: @domchell");
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: harvestcrop.exe <encryptedfile> <key>");
                return;
            }
            string encfile = args[0].Trim();
            Config.CryptoKey = args[1].Trim();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(encfile);

            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(Crypto.DecryptString(line));
            }

            file.Close();
        }
    }
}
