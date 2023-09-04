using TestApp;
using TestApp.Helpers;

Console.WriteLine("Ingresar ruta de archivo txt para importar la matriz a procesar: ('N' para salir)");
string readLine = Console.ReadLine();

while (readLine.ToUpper() != "N")
{
    bool successfulRead = FileManagerHelper.ReadFile(readLine, out Matrix? matrix, out string error);

    if (successfulRead)
    {
        matrix.ShowMaxString();
    }
    else
    {
        Console.WriteLine("\n" + error);
    }

    Console.WriteLine("\nIngresar ruta de archivo txt para importar la matriz a procesar: ('N' para salir)");
    readLine = Console.ReadLine();
}
