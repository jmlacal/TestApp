namespace TestApp.Exceptions
{
    public class NullCharacterException : Exception
    {
        public NullCharacterException() : base("Se encontró al menos un posición de la matriz sin valor asignado.")
        {
        }
    }
}
