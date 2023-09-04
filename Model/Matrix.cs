using TestApp.Enums;
using TestApp.Exceptions;

namespace TestApp
{
    public class Matrix
    {
        private List<Cadena> maxStrings;
        private readonly string[] value;

        private Matrix(string[] value)
        {
            this.value = value;
            calculateMaxString();
        }

        public static Matrix CreateMatrix(List<string> value)
        {
            Validate(value);
            return new Matrix(value.ToArray());
        }

        private static void Validate(List<string> value)
        {
            int largoPrimerLinea = 0;

            foreach (string line in value)
            {
                if (largoPrimerLinea == 0)
                {
                    largoPrimerLinea = line.Length;
                }
                else if (largoPrimerLinea != line.Length)
                {
                    throw new InvalidMatrixException();
                }
            }
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

        private Cadena SearchHorizontalsStrings()
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

            return maximaCadena;
        }

        private Cadena SearchVerticalsStrings()
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

            return maximaCadena;
        }

        private Cadena SearchDiagonalsString()
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

            return maximaCadena;
        }

        private Cadena SearchInversalDiagonalStrings()
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

            return maximaCadena;
        }

        public void calculateMaxString()
        {
            List<Task<Cadena>> tasks = new List<Task<Cadena>>();
            
            tasks.Add(new Task<Cadena>(() =>
            {
                return SearchHorizontalsStrings();
            }));

            tasks.Add(new Task<Cadena>(() =>
            {
                return SearchVerticalsStrings();
            }));

            tasks.Add(new Task<Cadena>(() => 
            {
                return SearchDiagonalsString();
            }));

            tasks.Add(new Task<Cadena>(() =>
            {
                return SearchInversalDiagonalStrings();
            }));

            foreach (Task<Cadena> task in tasks)
            {
                task.Start();
            }

            foreach (Task<Cadena> task in tasks)
            {
                task.Wait();
            }

            List<Cadena> cadenasMaximas = new List<Cadena>();

            foreach (Task<Cadena> task in tasks)
            {
                cadenasMaximas.Add(task.Result);
            }

            foreach(Cadena cadena in cadenasMaximas)
            {
                if (maxStrings == null || cadena.Repeticiones > maxStrings.First().Repeticiones)
                {
                    maxStrings = new List<Cadena>
                    {
                        cadena
                    };
                }
                else if (cadena.Repeticiones == maxStrings.First().Repeticiones)
                {
                    maxStrings.Add(cadena);
                }
            }
        }

        public void ShowMaxString()
        {
            string stringToShow = "\"";

            for (int  i = 0; i < maxStrings.Count(); i++ )
            {
                Cadena maxString = maxStrings[i];

                if (i > 0)
                {
                    stringToShow = stringToShow + " y \"";
                }

                for (int j = 1; j <= maxString.Repeticiones; j++)
                {
                    if (j < maxStrings[0].Repeticiones)
                    {
                        stringToShow = stringToShow + maxString.Caracter + ", ";
                    }
                    else
                    {
                        stringToShow = stringToShow + maxString.Caracter + " (" + maxString.Type + ")\"";
                    }
                }
            }

            Console.WriteLine("\nCadena/s de caracteres adyacentes mas larga/s: " + stringToShow);            
        }
    }
}
