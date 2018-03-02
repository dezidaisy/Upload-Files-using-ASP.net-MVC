using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplication3.Controllers
{
    public class UploadFilesController : Controller
    {
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size =  files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath="";
            var filePath2 = "";
            foreach (var formFile in files)
            {
                filePath2 = Path.Combine(Directory.GetCurrentDirectory()+"\\Documents\\" + formFile.FileName);
                if (filePath != "") {
                    filePath = filePath + "," + filePath2;
                }
                else
                {
                    filePath = filePath2;
                }
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                       }
                }
            }

            // process uploaded files
          
          
            return Ok(new { count = files.Count, size, filePath });
        }
    }
}