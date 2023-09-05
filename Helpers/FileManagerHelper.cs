using TestApp.Exceptions;

namespace TestApp.Helpers
{
    public static class FileManagerHelper
    {
        private static void ValidateEmptyLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new EmptyLineException();
            }
        }

        private static void ValidateTypeFile(string path)
        {
            int pathLenght = path.Length;

            if (path.Substring((pathLenght - 4), 4).ToUpper() != ".TXT")
            {
                throw new TypeFileException();
            }
        }

        private static void ValidateNullCharacterLine(string line)
        {
            if (line.Contains(",,"))
            {
                throw new NullCharacterException();
            }
        }

        private static void ValidateExcesiveCaractersLine(string line)
        {
            if ((line.Count(c => (c == ',')) + 1) != ((line.Length + 1) / 2f))
            {
                throw new ExcesiveCaractersException();
            }
        }

        private static void ValidateEmptyFile(List<string> lineasArchivo)
        {
            if (lineasArchivo.Count() == 0)
            {
                throw new EmptyFileException();
            }
        }

        private static void CleanSpaces(ref string line)
        {
            line = line.Replace(" ", string.Empty);
        }

        private static void DeleteFinalComma(ref string line)
        {
            if (line.EndsWith(','))
            {
                line = line.Remove(line.Length - 1, 1);
            }
        }

        private static void DeleteCommas(ref string line)
        {
            line = line.Replace(",", string.Empty);
        }

        public static bool ReadFile(string path, out Matrix? matrix, out string error)
        {
            string line;
            List<string> lineasArchivo = new List<string>();
            matrix = null;

            try
            {
                ValidateTypeFile(path);

                StreamReader sr = new StreamReader(path);

                try
                {
                    line = sr.ReadLine();

                    while (line != null)
                    {
                        ValidateEmptyLine(line);
                        ValidateNullCharacterLine(line);
                        CleanSpaces(ref line);
                        DeleteFinalComma(ref line);
                        ValidateExcesiveCaractersLine(line);
                        DeleteCommas(ref line);
                        lineasArchivo.Add(line);

                        line = sr.ReadLine();
                    }

                    ValidateEmptyFile(lineasArchivo);
                    matrix = Matrix.CreateMatrix(lineasArchivo);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    sr.Close();
                    return false;
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                if(ex is TypeFileException)
                {
                    error = ex.Message;
                }
                else
                {
                    error = new FileException().Message;
                }
                return false;
            }

            error = string.Empty;

            return true;
        }
    }
}
