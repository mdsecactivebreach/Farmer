using System;
using System.Collections.Generic;
using System.IO;

namespace Farmer
{
    class Config
    {
        public static int timer = 0;
        public static int port = 8080;
        public static string output;
        public static StreamWriter sw;
        public static string key = "farmer";
        public static bool encrypt = false;
        public static string banner = @"
  █████▒▄▄▄       ██▀███   ███▄ ▄███▓▓█████  ██▀███  
▓██   ▒▒████▄    ▓██ ▒ ██▒▓██▒▀█▀ ██▒▓█   ▀ ▓██ ▒ ██▒
▒████ ░▒██  ▀█▄  ▓██ ░▄█ ▒▓██    ▓██░▒███   ▓██ ░▄█ ▒
░▓█▒  ░░██▄▄▄▄██ ▒██▀▀█▄  ▒██    ▒██ ▒▓█  ▄ ▒██▀▀█▄  
░▒█░    ▓█   ▓██▒░██▓ ▒██▒▒██▒   ░██▒░▒████▒░██▓ ▒██▒
 ▒ ░    ▒▒   ▓▒█░░ ▒▓ ░▒▓░░ ▒░   ░  ░░░ ▒░ ░░ ▒▓ ░▒▓░
 ░       ▒   ▒▒ ░  ░▒ ░ ▒░░  ░      ░ ░ ░  ░  ░▒ ░ ▒░
 ░ ░     ░   ▒     ░░   ░ ░      ░      ░     ░░   ░ 
             ░  ░   ░            ░      ░  ░   ░     
";
    }
}
