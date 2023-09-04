namespace TestApp.Exceptions
{
    public class InvalidMatrixException : Exception
    {
        public InvalidMatrixException() : base("Error: Las filas de la matriz no tienen la misma longitud.")
        {
        }
    }
}
