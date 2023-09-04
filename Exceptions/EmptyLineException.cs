using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Exceptions
{
    public class EmptyLineException : Exception
    {
        public EmptyLineException() : base("Error: se encontraron lineas sin caracteres")
        {
        }
    }
}
