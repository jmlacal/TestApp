using TestApp;
using TestApp.Consts;
using TestApp.Helpers;

Console.WriteLine(Const.InitMsg);
string readLine = Console.ReadLine();

while (readLine.ToUpper() != "N")
{
    bool successfulRead = FileManagerHelper.ReadFile(readLine, out Matrix? matrix, out string error);

    if (successfulRead)
    {
        matrix.ShowMatrix();
        matrix.ShowMaxString();
    }
    else
    {
        Console.WriteLine("\n" + Const.ErrMsg + error);
    }

    Console.WriteLine("\n\n" + Const.InitMsg);
    readLine = Console.ReadLine();
}
