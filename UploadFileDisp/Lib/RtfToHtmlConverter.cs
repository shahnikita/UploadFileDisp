using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using System.Windows.Forms;


namespace UploadFileDisp.Lib
{
    public static class RtfToHtmlConverter
    {
        private const string FlowDocumentFormat = "<FlowDocument>{0}</FlowDocument>";

        public static string ConvertRtfToHtml(string rtfText)
        {
            var xamlText = string.Format(FlowDocumentFormat, ConvertRtfToXaml(rtfText));

            return HtmlFromXamlConverter.ConvertXamlToHtml(rtfText, false);
        }

        private static string ConvertRtfToXaml(string rtfText)
        {
            var richTextBox = new RichTextBox();
            if (string.IsNullOrEmpty(rtfText)) return "";

            

            //Create a MemoryStream of the Rtf content

            using (var rtfMemoryStream = new MemoryStream())
            {
                using (var rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                {
                    rtfStreamWriter.Write(rtfText);
                    rtfStreamWriter.Flush();
                    rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                    //Load the MemoryStream into TextRange ranging from start to end of RichTextBox.
                  //  textRange.Load(rtfMemoryStream, DataFormats.Rtf);
                }
            }

            using (var rtfMemoryStream = new MemoryStream())
            {

                
                //textRange.Save(rtfMemoryStream, DataFormats.Html);
                rtfMemoryStream.Seek(0, SeekOrigin.Begin);
                using (var rtfStreamReader = new StreamReader(rtfMemoryStream))
                {
                    return rtfStreamReader.ReadToEnd();
                }
            }

        }




    }
}