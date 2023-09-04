namespace TestApp.Exceptions
{
    public class NullCharacterException : Exception
    {
        public NullCharacterException() : base("Error: Se encontro al menos un posición de la matriz sin valor asignado.")
        {
        }
    }
}
