using Microsoft.AspNetCore.Http;
using RestWithAspNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {

        private readonly string basePath;
        private readonly IHttpContextAccessor context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            this.context = context;
            basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";

        }

        public byte[] GetFile(string filename)
        {
            var filepath = basePath + filename;
            return File.ReadAllBytes(filepath);
        }

        public async  Task<List<FileDetailVO>> SaveFilestoDisk(List<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>(0);

            foreach (var file in files)
            {
                list.Add(await SaveFiletoDisk(file));
            }

            return list;

        }
        public async Task<FileDetailVO> SaveFiletoDisk(IFormFile file)
        {
            FileDetailVO FileDetail = new FileDetailVO();

            var filetype = Path.GetExtension(file.FileName);
            var baseUrl = context.HttpContext.Request.Host;
            if (filetype.ToLower()==".pdf" || filetype.ToLower() == ".jpg"|| filetype.ToLower() == ".png"|| 
                filetype.ToLower() == ".jpeg" )
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length>0)
                {
                    var destination = Path.Combine(basePath, "", docName);
                    FileDetail.DocumentName = docName;
                    FileDetail.DocType = filetype;

                    FileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + FileDetail.DocumentName);

                    using var strem = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(strem);
                }

            }
            return FileDetail;

        }
    }
}
