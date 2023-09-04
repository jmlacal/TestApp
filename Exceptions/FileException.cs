namespace TestApp.Exceptions
{
    public class ExcesiveCaractersException : Exception
    {
        public ExcesiveCaractersException() : base("Error: No se puede ingresar mas de un caracter en cada posición")
        {
        }
    }
}
