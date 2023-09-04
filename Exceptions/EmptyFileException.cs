using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() : base("Error: El archivo no contiene cadenas de caracteres.")
        {
        }
    }
}
