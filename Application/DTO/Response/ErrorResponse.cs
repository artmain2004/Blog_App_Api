﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response
{
    public class ErrorResponse()
    {
        public int StatusCode { get; set; }

        public required string Message { get; set; }
    }



}
