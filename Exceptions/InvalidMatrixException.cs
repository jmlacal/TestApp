namespace TestApp.Exceptions
{
    public class InvalidMatrixException : Exception
    {
        public InvalidMatrixException() : base("Las filas de la matriz no tienen la misma longitud.")
        {
        }
    }
}
