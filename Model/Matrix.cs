using System.Text;
using TestApp.Enums;
using TestApp.Exceptions;

namespace TestApp
{
    public class Matrix
    {
        public const string EmptyStringsMsg = "\nLa matriz no contiene ninguna cadena caracteres adyacentes iguales.";
        public const string MaxStringsMsg = "\nCadena/s de caracteres adyacentes más larga/s: ";

        private List<StringMatrix> maxStrings = new List<StringMatrix>();
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

        private void SetMaxStrings(in StringMatrix currentString, ref List<StringMatrix> currentMaxString)
        {
            if (currentMaxString.Count == 0 || currentString.Sum > currentMaxString[0].Sum)
            {
                currentMaxString = new List<StringMatrix>
                    {
                        currentString
                    };
            }
            else if (currentString.Sum == currentMaxString[0].Sum)
            {
                currentMaxString.Add(currentString);
            }
        }

        private void AnalyzeValue(char value, ref StringMatrix currentString, ref List<StringMatrix> currentMaxString)
        {
            if (currentString.Character == value)
            {
                currentString.Sum++;
            }
            else
            {
                currentString = new StringMatrix(value, 1, currentString.Type);
            }

            if ((currentMaxString.Count == 0 || currentString != currentMaxString.Last()) && currentString.Sum > 1)
            {
                SetMaxStrings(currentString, ref currentMaxString);
            }
        }

        private List<StringMatrix> SearchHorizontalsStrings()
        {
            StringMatrix currentString;
            List<StringMatrix> maxString = new List<StringMatrix>();

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

        private List<StringMatrix> SearchVerticalsStrings()
        {
            StringMatrix currentString;
            List<StringMatrix> maxString = new List<StringMatrix>();

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

        private List<StringMatrix> SearchDiagonalsString()
        {
            StringMatrix currentString;
            List<StringMatrix> maxString = new List<StringMatrix>();

            for (int i = 1; i < value.Length; i++)
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

            for (int j = value[0].Length - 2; j > 0; j--)
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

        private List<StringMatrix> SearchInversalDiagonalStrings()
        {
            StringMatrix currentString;
            List<StringMatrix> maxString = new List<StringMatrix>();

            for (int i = 1; i < value.Length; i++)
            {
                currentString = new StringMatrix(',', 0, StringTypeEnum.Diagonal);

                int j = value[0].Length - 1;

                for (int iAux = i; iAux >= 0 && j >= 0; iAux--)
                {
                    char valor = value[iAux][j];
                    AnalyzeValue(valor, ref currentString, ref maxString);
                    j--;
                }
            }

            for (int j = 1; j < value[0].Length - 1; j++)
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

        private Task<List<StringMatrix>> SearchMaxStringTask(Func<List<StringMatrix>> searchMaxString)
        {
            return new Task<List<StringMatrix>>(() =>
            {
                return searchMaxString();
            });
        }

        private void CalculateMaxString()
        {
            List<Task<List<StringMatrix>>> tasks = new List<Task<List<StringMatrix>>>();

            tasks.Add(SearchMaxStringTask(SearchHorizontalsStrings));
            tasks.Add(SearchMaxStringTask(SearchVerticalsStrings));
            tasks.Add(SearchMaxStringTask(SearchDiagonalsString));
            tasks.Add(SearchMaxStringTask(SearchInversalDiagonalStrings));

            foreach (Task<List<StringMatrix>> task in tasks)
            {
                task.Start();
            }

            foreach (Task<List<StringMatrix>> task in tasks)
            {
                task.Wait();
            }

            List<StringMatrix> cadenasMaximas = new List<StringMatrix>();

            foreach (Task<List<StringMatrix>> task in tasks)
            {
                cadenasMaximas.AddRange(task.Result);
            }

            foreach (StringMatrix cadena in cadenasMaximas)
            {
                SetMaxStrings(cadena, ref maxStrings);
            }
        }

        public void ShowMatrix()
        {
            if (maxStrings.Count > 0)
            {
                StringBuilder matrixStringBuilder = new StringBuilder("\nMatriz ingresada:\n");
                int lenghtRows = value[0].Length;
                
                for (int i = 0; i < value.Count(); i++)
                {
                    matrixStringBuilder.Append("\n");

                    for(int j = 0; j < lenghtRows; j++)
                    {
                        if (j == (lenghtRows - 1))
                        {
                            matrixStringBuilder.Append(value[i][j]);
                        }
                        else
                        {
                            matrixStringBuilder.Append(value[i][j]).Append(", ");
                        }
                    }
                }

                Console.WriteLine(matrixStringBuilder.ToString());
            }
        }

        public void ShowMaxString()
        {
            if (maxStrings.Count == 0)
            {
                Console.WriteLine(EmptyStringsMsg);

                return;
            }

            StringBuilder maxStringBuilder = new StringBuilder(MaxStringsMsg).Append("\"");

            for (int  i = 0; i < maxStrings.Count(); i++ )
            {
                StringMatrix maxString = maxStrings[i];

                if (i > 0)
                {
                    maxStringBuilder.Append(" y \"");
                }

                for (int j = 1; j <= maxString.Sum; j++)
                {
                    if (j < maxStrings[0].Sum)
                    {
                        maxStringBuilder.Append(maxString.Character).Append(", ");
                    }
                    else
                    {
                        maxStringBuilder.Append(maxString.Character).Append(" (").Append(maxString.Type).Append(")\"");
                    }
                }
            }

            Console.WriteLine(maxStringBuilder.ToString());            
        }
    }
}
