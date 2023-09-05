namespace TestApp.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() : base("El archivo no contiene filas de caracteres.")
        {
        }
    }
}
