using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public static class ImageMapper
    {
        public static byte[] ImageToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(ms);

                byte[] result = ms.ToArray();  

                return result;
            }

            
        }
    }
}
