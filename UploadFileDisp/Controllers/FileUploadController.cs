using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UploadFileDisp.Lib;

namespace UploadFileDisp.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /FileUpload/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string path = @"D:\Temp\";

            HttpPostedFileBase photo = Request.Files["photo"];

            if (photo != null)
                photo.SaveAs(path + photo.FileName);


            return View();
        }

        //[HttpPost]
        //public JsonResult ShowFile()
        //{
        //    HttpPostedFileBase file = Request.Files["photo"];
        //    string content = string.Empty;
        //    try
        //    {
        //        if (file != null)
        //        {
        //            //StreamReader streamReader = new StreamReader(file.InputStream);
        //            ////  byte[] binData = b.ReadBytes(file.ContentLength);

        //            //while ((content = streamReader.ReadLine()) != null)
        //            //{
        //            //    builder.Append(content);
        //            //}
        //            //streamReader.Close();

        //            //content = System.Text.Encoding.UTF8.GetString(binData);
        //        }

        //    }
        //    catch (Exception exc)
        //    {

        //        return Json("Uh oh!");
        //    }

        //    return Json(content);
        //}


        [HttpPost]
        public virtual ActionResult UploadFile()
        {
            HttpPostedFileBase myFile = Request.Files["MyFile"];
          
            string message = "File upload failed";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Uploads");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));
                       
                        ResumeParser rp = new ResumeParser();
                        message =rp.Converter(Path.Combine(pathForSaving, myFile.FileName), Path.GetExtension(myFile.FileName));
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }
            return Content(message);
        }

        /// <summary>
        /// Creates the folder if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
    }
}
