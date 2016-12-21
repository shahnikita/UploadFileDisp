using Aspose.Pdf;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UploadFileDisp.Lib
{
    public class ResumeParser
    {
        public string Converter(string path, string extension)
        {
            string reuturnString = string.Empty;
            // Save the document to a MemoryStream object.
            MemoryStream stream = new MemoryStream();


            switch (extension)
            {
                case ".pdf":
           
                    Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(path);
                    pdfDoc.Save(HttpContext.Current.Server.MapPath("Input/input.html"), Aspose.Pdf.SaveFormat.Html);
                    break;
                case ".doc":
                case ".docx":
                case ".txt":
                case ".rtf":
                    {
                       
                        //// Load in the document
                        Aspose.Words.Document doc = new Aspose.Words.Document(path);
                        doc.Save(HttpContext.Current.Server.MapPath("Input/input.html"), Aspose.Words.SaveFormat.Html);
                        break;
                    }
            }

            reuturnString = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("Input/input.html"));
            return reuturnString;
        }


    }
}