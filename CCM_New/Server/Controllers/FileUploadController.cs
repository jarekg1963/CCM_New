using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace CCM_New.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class FileUploadController : ControllerBase
    {

        private IHostingEnvironment hostingEnv;

        public FileUploadController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost("[action]")]
        public void Save(IList<IFormFile> UploadFiles)
        {
            long size = 0;
            try
            {
                foreach (var file in UploadFiles)
                {
                    var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');
                    filename = hostingEnv.ContentRootPath + @"\Documents\" + $@"\{filename}";
                    size += (int)file.Length;
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    Response.Clear();
                    Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = filename;
                }
               
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
        }
        [HttpPost("[action]")]
        public void Remove(IList<IFormFile> UploadFiles)
        {
            try
            {
                var filename = hostingEnv.ContentRootPath + $@"\{UploadFiles[0].FileName}";
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File removed successfully";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
        }

    }
}
