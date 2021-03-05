using System;
using System.Collections.Generic;

namespace Crop
{
    class Config
    {
        public static List<string> folders = new List<string>();
        public static string targetPath;
        public static string targetIcon;
        public static string targetFilename;
        public static string targetLocation;
        public static string banner = @"
 ▄████▄   ██▀███   ▒█████   ██▓███  
▒██▀ ▀█  ▓██ ▒ ██▒▒██▒  ██▒▓██░  ██▒
▒▓█    ▄ ▓██ ░▄█ ▒▒██░  ██▒▓██░ ██▓▒
▒▓▓▄ ▄██▒▒██▀▀█▄  ▒██   ██░▒██▄█▓▒ ▒
▒ ▓███▀ ░░██▓ ▒██▒░ ████▓▒░▒██▒ ░  ░
░ ░▒ ▒  ░░ ▒▓ ░▒▓░░ ▒░▒░▒░ ▒▓▒░ ░  ░
  ░  ▒     ░▒ ░ ▒░  ░ ▒ ▒░ ░▒ ░     
░          ░░   ░ ░ ░ ░ ▒  ░░       
░ ░         ░         ░ ░           
░                                  
";



        public static void WalkDirectoryTree(string root)
        {
            Stack<string> dirs = new Stack<string>();

            Console.WriteLine("[*] Walking directory tree for: " + root);
            if (!System.IO.Directory.Exists(root))
            {
                Console.WriteLine("[!] Error, folder does not exist");
                return;
            }
            dirs.Push(root);
            folders.Add(root);

            while (dirs.Count > 0)
            {

                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable
                // to ignore the exception and continue enumerating the remaining files and
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The
                // choice of which exceptions to catch depends entirely on the specific task
                // you are intending to perform and also on how much you know with certainty
                // about the systems on which this code will run.
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                {
                    dirs.Push(str);
                    folders.Add(str);
                }
            }
        }
    }
}
