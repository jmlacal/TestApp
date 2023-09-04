namespace TestApp.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() : base("Error: El archivo no contiene cadenas de caracteres.")
        {
        }
    }
}
