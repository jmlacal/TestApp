using TestApp.Enums;

namespace TestApp
{
    public class StringMatrix
    {
        public char Character { get; set; }
        public int Sum { get; set; }
        public StringTypeEnum Type { get; set; }

        public StringMatrix(char character, int sum, StringTypeEnum type)
        {
            Character = character;
            Sum = sum;
            Type = type;
        }
    }
}
