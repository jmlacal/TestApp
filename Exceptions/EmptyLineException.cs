namespace TestApp.Exceptions
{
    public class EmptyLineException : Exception
    {
        public EmptyLineException() : base("Se encontraron fila/s sin caracteres")
        {
        }
    }
}
