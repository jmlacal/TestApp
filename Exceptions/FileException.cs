namespace TestApp.Exceptions
{
    public class ExcesiveCaractersException : Exception
    {
        public ExcesiveCaractersException() : base("No se puede ingresar mas de un caracter en cada posición")
        {
        }
    }
}
