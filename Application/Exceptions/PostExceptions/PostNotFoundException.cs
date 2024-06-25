using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.PostExceptions
{
    public class PostNotFoundException(string message) : Exception(message)
    {

    }
}
