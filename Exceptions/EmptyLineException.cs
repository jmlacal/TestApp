namespace TestApp.Exceptions
{
    public class EmptyLineException : Exception
    {
        public EmptyLineException() : base("Error: se encontraron filas sin caracteres")
        {
        }
    }
}
