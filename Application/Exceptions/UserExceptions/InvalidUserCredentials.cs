using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserExceptions
{
    public class InvalidUserCredentials(string message) : Exception(message)
    {
    }
}
