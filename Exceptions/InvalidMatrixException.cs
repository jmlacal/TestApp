namespace TestApp.Exceptions
{
    public class InvalidMatrixException : Exception
    {
        public InvalidMatrixException() : base("Error: Todas las lineas de la matriz no tienen la misma longitud.")
        {
        }
    }
}
