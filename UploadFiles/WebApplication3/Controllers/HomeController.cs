using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplication3
{

    public class HomeController : Controller
    {
        public static UFiles FileList = new UFiles();
        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {

            long size = 0;
            // full path to file in temp location
            var filePath = "";

            if (files != null)
            {
                size = files.Sum(f => f.Length);
                FileList.Size = size;
                FileList.Count = files.Count;
                foreach (var formFile in files)
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Documents\\" + formFile.FileName);
                    FileList.FileName.Add(filePath);
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
            }

            // process uploaded files


            return View("Index");
            //return Ok(new { count = FileList.Count });
        }

    }
}
