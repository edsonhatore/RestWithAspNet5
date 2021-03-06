using Microsoft.AspNetCore.Http;
using RestWithAspNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Business
{
   public  interface IFileBusiness
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailVO>SaveFiletoDisk(IFormFile file);
        public Task<List<FileDetailVO>>SaveFilestoDisk(List<IFormFile> file);
     

    }

}
