using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var response = await _service.GetList();
            return Ok(response);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(ProductDTO filter)
        {
            return Ok(await _service.GetList(filter));
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile formFile, string productId)
        {
            var response = new Response<string>();
            try
            {
                string Filepath = GetFilepath(productId);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + "\\" + productId + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await formFile.CopyToAsync(stream);
                    response.isSuccess = true;
                    response.message = "upload successfully";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPut("MultiUploadImage")]
        public async Task<IActionResult> MultiUploadImage(IFormFileCollection filecollection, string productId)
        {
            var response = new Response<string>();
            int passcount = 0; int errorcount = 0;
            try
            {
                string Filepath = GetFilepath(productId);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                foreach (var file in filecollection)
                {
                    string imagepath = Filepath + "\\" + file.FileName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        passcount++;

                    }
                }
            }
            catch (Exception ex)
            {
                errorcount++;
                response.message = ex.Message;
            }
            response.isSuccess = true;
            response.message = passcount + " Files uploaded &" + errorcount + " files failed";

            return Ok(response);
        }

        [HttpGet("GetImage/{productId}")]
        public async Task<IActionResult> GetImage(string productId)
        {
            string Imageurl = string.Empty;
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilepath(productId);
                string imagepath = Filepath + "\\" + productId + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    Imageurl = hosturl + "/Upload/product/" + productId + "/" + productId + ".png";
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
            }
            return Ok(Imageurl);

        }

        [HttpGet("GetMultiImage")]
        public async Task<IActionResult> GetMultiImage(string productId)
        {
            List<string> Imageurl = new List<string>();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilepath(productId);

                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string filename = fileInfo.Name;
                        string imagepath = Filepath + "\\" + filename;
                        if (System.IO.File.Exists(imagepath))
                        {
                            string _Imageurl = hosturl + "/Upload/product/" + productId + "/" + filename;
                            Imageurl.Add(_Imageurl);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return Ok(Imageurl);
        }

        [HttpGet("DownloadImage")]
        public async Task<IActionResult> DownloadImage(string productId)
        {
            try
            {
                string Filepath = GetFilepath(productId);
                string imagepath = Filepath + "\\" + productId + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    MemoryStream stream = new MemoryStream();
                    using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                    stream.Position = 0;
                    return File(stream, "image/png", productId + ".png");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("RemoveImage")]
        public async Task<IActionResult> RemoveImage(string productId)
        {
            try
            {
                string Filepath = GetFilepath(productId);
                string imagepath = Filepath + "\\" + productId + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                    return Ok("pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("MultiRemoveImage")]
        public async Task<IActionResult> MultiRemoveImage(string productId)
        {
            try
            {
                string Filepath = GetFilepath(productId);
                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        fileInfo.Delete();
                    }
                    return Ok("pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [NonAction]
        private string GetFilepath(string productId)
        {
            return this._environment.WebRootPath + "\\Upload\\product\\" + productId;
        }

    }
}
