namespace TestApp.Exceptions
{
    public class FileException : Exception
    {
        public FileException() : base("Error: No se ha podido accerder al archivo en la ruta indicada.")
        {
        }
    }
}
