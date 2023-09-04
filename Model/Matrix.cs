using TestApp.Enums;

namespace TestApp
{
    public class Matrix
    {
        private Cadena maximaCadena = new Cadena(',', 0, StringTypeEnum.Vertical);
        private string[] value;

        public Matrix(string[] value)
        {
            this.value = value;
        }

        private void analizarValor(char valor, ref Cadena actualCadena, ref Cadena maximaCadena)
        {
            if (actualCadena.Caracter == valor)
            {
                actualCadena.Repeticiones++;
            }
            else
            {
                actualCadena = new Cadena(valor, 1, actualCadena.Type);
            }

            if (actualCadena.Repeticiones > maximaCadena.Repeticiones)
            {
                maximaCadena = actualCadena;
            }

            return;
        }

        private void SearchHorizontalsStrings()
        {
            Cadena actualCadena;
            Cadena maximaCadena = new Cadena(',', 0, StringTypeEnum.Horizontal);

            for (int i = 0; i < value.Length; i++)
            {
                actualCadena = new Cadena(',', 0, StringTypeEnum.Horizontal);

                for (int j = 0; j < value[0].Length; j++)
                {
                    char valor = value[i][j];
                    analizarValor(valor, ref actualCadena, ref maximaCadena);
                }
            }

            Console.WriteLine(maximaCadena.Caracter + ":" + maximaCadena.Repeticiones);
        }

        private void SearchVerticalsStrings()
        {
            Cadena actualCadena;
            Cadena maximaCadena = new Cadena(',', 0, StringTypeEnum.Vertical);

            for (int j = 0; j < value[0].Length; j++)
            {
                actualCadena = new Cadena(',', 0, StringTypeEnum.Vertical);

                for (int i = 0; i < value.Length; i++)
                {
                    char valor = value[i][j];
                    analizarValor(valor, ref actualCadena, ref maximaCadena);
                }
            }

            Console.WriteLine(maximaCadena.Caracter + ":" + maximaCadena.Repeticiones);
        }

        private void SearchDiagonalsString()
        {
            Cadena actualCadena;
            Cadena maximaCadena = new Cadena(',', 0, StringTypeEnum.Diagonal);

            for (int i = 0; i < value.Length; i++)
            {
                actualCadena = new Cadena(',', 0, StringTypeEnum.Diagonal);

                int j = 0;
                for (int iAux = i; iAux >= 0 && j < value[iAux].Length; iAux--)
                {
                    char valor = value[iAux][j];
                    analizarValor(valor, ref actualCadena, ref maximaCadena);
                    j++;
                }
            }

            for (int j = value[0].Length - 1; j > 0; j--)
            {
                int i = value.Length - 1;
                actualCadena = new Cadena(',', 0, StringTypeEnum.Diagonal);

                for (int jAux = j; i >= 0 && jAux < value[i].Length; jAux++)
                {
                    char valor = value[i][jAux];
                    analizarValor(valor, ref actualCadena, ref maximaCadena);
                    i--;
                }
            }

            Console.WriteLine(maximaCadena.Caracter + ":" + maximaCadena.Repeticiones);
        }

        private void SearchInversalDiagonalStrings()
        {
            Cadena actualCadena;
            Cadena maximaCadena = new Cadena(',', 0, StringTypeEnum.Diagonal);

            for (int i = 0; i < value.Length; i++)
            {
                actualCadena = new Cadena(',', 0, StringTypeEnum.Diagonal);

                int j = value[0].Length - 1;

                for (int iAux = i; iAux >= 0 && j > 0; iAux--)
                {
                    char valor = value[iAux][j];
                    analizarValor(valor, ref actualCadena, ref maximaCadena);
                    j--;
                }
            }

            for (int j = 0; j < value[0].Length - 1; j++)
            {
                int i = value.Length - 1;
                actualCadena = new Cadena(',', 0, StringTypeEnum.Diagonal);

                for (int jAux = j; i >= 0 && jAux >= 0; jAux--)
                {
                    char valor = value[i][jAux];
                    analizarValor(valor, ref actualCadena, ref maximaCadena);
                    i--;
                }
            }

            Console.WriteLine(maximaCadena.Caracter + ":" + maximaCadena.Repeticiones);
        }

        public void searchMaxString()
        {
            SearchHorizontalsStrings();
            SearchVerticalsStrings();
            SearchDiagonalsString();
            SearchInversalDiagonalStrings();
        }

        public void ShowMaxString()
        {
            string stringToShow = "";

            for (int i = 1; i < maximaCadena.Repeticiones; i++)
            {
                if (i < maximaCadena.Repeticiones)
                {
                    stringToShow = stringToShow + maximaCadena.Caracter + ", ";
                }
                else
                {
                    stringToShow = stringToShow + maximaCadena.Caracter + "(" + maximaCadena.Type + ")";
                }
            }

            Console.WriteLine("\nLa cadena de caracteres adyacentes mas larga es : " + stringToShow);
        }
    }
}
