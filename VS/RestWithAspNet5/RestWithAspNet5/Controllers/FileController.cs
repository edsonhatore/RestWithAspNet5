using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5.Business;
using RestWithAspNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class FileController : ControllerBase
    {

        private readonly IFileBusiness _iFileBusiness;

        public FileController(IFileBusiness iFileBusiness)
        {
            _iFileBusiness = iFileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
            {
            FileDetailVO detail = await _iFileBusiness.SaveFiletoDisk(file);

            return new OkObjectResult(detail); 

            }

        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyeFiles([FromForm] List<IFormFile> files)
        {
            List < FileDetailVO> details = await _iFileBusiness.SaveFilestoDisk(files);

            return new OkObjectResult(details);

        }


        [HttpGet("donwloadFile/{filename}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-strem")]
        public async Task<IActionResult> GetfileAsync(string  filename)
        {
            byte[] buffer =  _iFileBusiness.GetFile(filename);

            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(filename).Replace(".", "")}";

            }
            HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
            await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            return new ContentResult();

        }

    }
}
