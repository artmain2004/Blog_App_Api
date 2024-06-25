using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request
{
    public class PostCreateRequest
    {

        public Guid UserId { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }



    }
}
