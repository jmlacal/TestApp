using TestApp.Enums;
using TestApp.Exceptions;

namespace TestApp
{
    public class Matrix
    {
        private List<StringMatrix> maxStrings;
        private readonly string[] value;

        private Matrix(string[] value)
        {
            this.value = value;
            CalculateMaxString();
        }

        public static Matrix CreateMatrix(List<string> value)
        {
            Validate(value);
            return new Matrix(value.ToArray());
        }

        private static void Validate(List<string> value)
        {
            int lengthFirstLine = 0;

            foreach (string line in value)
            {
                if (lengthFirstLine == 0)
                {
                    lengthFirstLine = line.Length;
                }
                else if (lengthFirstLine != line.Length)
                {
                    throw new InvalidMatrixException();
                }
            }
        }

        private void AnalyzeValue(char value, ref StringMatrix currentString, ref StringMatrix maxString)
        {
            if (currentString.Character == value)
            {
                currentString.Sum++;
            }
            else
            {
                currentString = new StringMatrix(value, 1, currentString.Type);
            }

            if (currentString.Sum > maxString.Sum)
            {
                maxString = currentString;
            }

            return;
        }

        private StringMatrix SearchHorizontalsStrings()
        {
            StringMatrix currentString;
            StringMatrix maxString = new StringMatrix(',', 0, StringTypeEnum.Horizontal);

            for (int i = 0; i < value.Length; i++)
            {
                currentString = new StringMatrix(',', 0, StringTypeEnum.Horizontal);

                for (int j = 0; j < value[0].Length; j++)
                {
                    char valor = value[i][j];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                }
            }

            return maxString;
        }

        private StringMatrix SearchVerticalsStrings()
        {
            StringMatrix currentString;
            StringMatrix maxString = new StringMatrix(',', 0, StringTypeEnum.Vertical);

            for (int j = 0; j < value[0].Length; j++)
            {
                currentString = new StringMatrix(',', 0, StringTypeEnum.Vertical);

                for (int i = 0; i < value.Length; i++)
                {
                    char valor = value[i][j];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                }
            }

            return maxString;
        }

        private StringMatrix SearchDiagonalsString()
        {
            StringMatrix currentString;
            StringMatrix maxString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

            for (int i = 0; i < value.Length; i++)
            {
                currentString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

                int j = 0;
                for (int iAux = i; iAux >= 0 && j < value[iAux].Length; iAux--)
                {
                    char valor = value[iAux][j];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                    j++;
                }
            }

            for (int j = value[0].Length - 1; j > 0; j--)
            {
                int i = value.Length - 1;
                currentString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

                for (int jAux = j; i >= 0 && jAux < value[i].Length; jAux++)
                {
                    char valor = value[i][jAux];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                    i--;
                }
            }

            return maxString;
        }

        private StringMatrix SearchInversalDiagonalStrings()
        {
            StringMatrix currentString;
            StringMatrix maxString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

            for (int i = 0; i < value.Length; i++)
            {
                currentString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

                int j = value[0].Length - 1;

                for (int iAux = i; iAux >= 0 && j > 0; iAux--)
                {
                    char valor = value[iAux][j];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                    j--;
                }
            }

            for (int j = 0; j < value[0].Length - 1; j++)
            {
                int i = value.Length - 1;
                currentString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

                for (int jAux = j; i >= 0 && jAux >= 0; jAux--)
                {
                    char valor = value[i][jAux];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                    i--;
                }
            }

            return maxString;
        }

        public void CalculateMaxString()
        {
            List<Task<StringMatrix>> tasks = new List<Task<StringMatrix>>();
            
            tasks.Add(new Task<StringMatrix>(() =>
            {
                return SearchHorizontalsStrings();
            }));

            tasks.Add(new Task<StringMatrix>(() =>
            {
                return SearchVerticalsStrings();
            }));

            tasks.Add(new Task<StringMatrix>(() => 
            {
                return SearchDiagonalsString();
            }));

            tasks.Add(new Task<StringMatrix>(() =>
            {
                return SearchInversalDiagonalStrings();
            }));

            foreach (Task<StringMatrix> task in tasks)
            {
                task.Start();
            }

            foreach (Task<StringMatrix> task in tasks)
            {
                task.Wait();
            }

            List<StringMatrix> cadenasMaximas = new List<StringMatrix>();

            foreach (Task<StringMatrix> task in tasks)
            {
                cadenasMaximas.Add(task.Result);
            }

            foreach (StringMatrix cadena in cadenasMaximas)
            {
                if (maxStrings == null || cadena.Sum > maxStrings.First().Sum)
                {
                    maxStrings = new List<StringMatrix>
                    {
                        cadena
                    };
                }
                else if (cadena.Sum == maxStrings.First().Sum)
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
                StringMatrix maxString = maxStrings[i];

                if (i > 0)
                {
                    stringToShow = stringToShow + " y \"";
                }

                for (int j = 1; j <= maxString.Sum; j++)
                {
                    if (j < maxStrings[0].Sum)
                    {
                        stringToShow = stringToShow + maxString.Character + ", ";
                    }
                    else
                    {
                        stringToShow = stringToShow + maxString.Character + " (" + maxString.Type + ")\"";
                    }
                }
            }

            Console.WriteLine("\nCadena/s de caracteres adyacentes mas larga/s: " + stringToShow);            
        }
    }
}
