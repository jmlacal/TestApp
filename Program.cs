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
        matrix.ShowMaxString();
    }
    else
    {
        Console.WriteLine(Const.ErrMsg + error);
    }

    Console.WriteLine(Const.InitMsg);
    readLine = Console.ReadLine();
}
