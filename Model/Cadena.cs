using TestApp.Enums;

namespace TestApp
{
    public class Cadena
    {
        public char Caracter { get; set; }
        public int Repeticiones { get; set; }
        public StringTypeEnum Type { get; set; }

        public Cadena(char caracter, int repeticiones, StringTypeEnum type)
        {
            Caracter = caracter;
            Repeticiones = repeticiones;
            Type = type;
        }
    }
}
