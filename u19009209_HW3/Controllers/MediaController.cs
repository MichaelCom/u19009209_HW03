using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u19009209_HW3.Models;
using System.IO;

namespace u19009209_HW3.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file, FormCollection collection)
        {

            string value = Convert.ToString(collection["optradio"]);

            if(value == "Document")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Documents"),file.FileName));
            }
            else if(value == "Image")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Images"), file.FileName));
            }
            else
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Videos"), file.FileName));
            }

            return RedirectToAction("Home");
        }

        public ActionResult Files()
        {
            List<FileModel> files = new List<FileModel>();

            string[] Documents = Directory.GetFiles(Server.MapPath("~/Content/Media/Documents"));
            string[] Images = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            string[] Videos = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            foreach (var file in Documents)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "doc";
                files.Add(locatedFile);
            }
            foreach (var file in Images)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "img";
                files.Add(locatedFile);
            }
            foreach (var file in Videos)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "vid";
                files.Add(locatedFile);
            }

            return View(files);
        }

        public ActionResult Images()
        {
            List<FileModel> Images = new List<FileModel>();
            string[] Imagelocations = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            foreach (var file in Imagelocations)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "img";
                Images.Add(locatedFile);
            }
            return View(Images);
        }

        public ActionResult Videos()
        {
            List<FileModel> Videos = new List<FileModel>();
            string[] Videoslocation = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));
            foreach (var file in Videoslocation)
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                locatedFile.FileType = "vid";
                Videos.Add(locatedFile);
            }
            return View(Videos);
        }

        public ActionResult AboutMe()
        {
            return View();
        }

        public FileResult DownloadFile(string fileName, string fileType)
        {
            byte[] bytes = null;

            if (fileType == "doc")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Documents/") + fileName);
            }
            else if (fileType == "vid")
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Videos/") + fileName);
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Images/") + fileName);
            }
            return File(bytes, "application/octet-stream", fileName);
        }

        public ActionResult DeleteFile(string fileName, string fileType)
        {
            string filelocation = null;

            if (fileType == "doc")
            {
                filelocation = Server.MapPath("~/Content/Media/Documents/") + fileName;
            }
            else if (fileType == "vid")
            {
                filelocation = Server.MapPath("~/Content/Media/Videos/") + fileName;
            }
            else
            {
                filelocation = Server.MapPath("~/Content/Media/Images/") + fileName;
            }

            System.IO.File.Delete(filelocation);
            return RedirectToAction("Home");
        }
    }
}