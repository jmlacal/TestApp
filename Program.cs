using TestApp;
using TestApp.Helpers;

Console.WriteLine("Ingresar ruta de archivo txt para importar la matriz a procesar: ('N' para salir)");
string lineaIngresada = Console.ReadLine();

while (lineaIngresada.ToUpper() != "N")
{
    bool lecturaExitosa = FileManagerHelper.ReadFile(lineaIngresada, out Matrix? matrix, out string errorLectura);

    if (lecturaExitosa)
    {
        matrix.ShowMaxString();
    }
    else
    {
        Console.WriteLine("\n" + errorLectura);
    }

    Console.WriteLine("\nIngresar ruta de archivo txt para importar la matriz a procesar: ('N' para salir)");
    lineaIngresada = Console.ReadLine();
}
