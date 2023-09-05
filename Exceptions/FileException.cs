namespace TestApp.Exceptions
{
    public class FileException : Exception
    {
        public FileException() : base("No se puede acceder al archivo en la ruta indicada.")
        {
        }
    }
}
