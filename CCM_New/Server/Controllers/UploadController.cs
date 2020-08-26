using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Syncfusion.Blazor.CircularGauge;

namespace CCM_New.Server.Controllers
{
    
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IHostingEnvironment hostingEnv;

        public UploadController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost][Route("api/upload/file")]
        public UploadResponse uploadFile([FromBody] UploadRequest req)
        {

            string sciezkaDoZapisu = System.IO.Path.Combine(this.hostingEnv.ContentRootPath, "Documents", req.id);



           if ( !System.IO.Directory.Exists(sciezkaDoZapisu))
                 System.IO.Directory.CreateDirectory(sciezkaDoZapisu); 

            var pl = System.IO.Path.Combine(this.hostingEnv.ContentRootPath, "Documents",req.id, $"{req.filename}");
            var plzwrot = System.IO.Path.Combine(this.hostingEnv.ContentRootPath, "Documents", req.id,  $"{ req.filename}");
            System.IO.File.WriteAllBytes(pl, req.file);

            return new UploadResponse
            {
                // responseMessage = $"plik: {req.id} miał {req.file.Count()} bajtów i zostął zapisany w: {pl} Pawel jest genialny"
                responseMessage = plzwrot
            };
        }

        [HttpGet]
        [Route("api/download/file")]
        public ActionResult DownloadFile(String fileName, string idKatalog)
        {



            var pl = System.IO.Path.Combine(this.hostingEnv.ContentRootPath, "Documents",idKatalog , fileName);
           
            var stream = System.IO.File.OpenRead(pl);
            return File(stream, "application/octet-stream", fileName);
         
        }

        [HttpGet]
        [Route("api/Delete/file")]
        public ActionResult DeleteFile(String fileName, string idKatalog)
        {

            var pl = System.IO.Path.Combine(this.hostingEnv.ContentRootPath, "Documents", idKatalog, fileName);

            System.IO.File.Delete(pl);
            return new EmptyResult();

        }



        public class UploadRequest
        {
            public string id { get; set; }
            public string filename { get; set; }
            public byte[] file { get; set; }
        }

        public class UploadResponse
        {
            public string responseMessage { get; set; }
        }
    }
    
}
