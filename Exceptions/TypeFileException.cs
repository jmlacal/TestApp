namespace TestApp.Exceptions
{
    public class TypeFileException : Exception
    {
        public TypeFileException() : base("Solo pueden importarse archivos txt")
        {
        }
    }
}
