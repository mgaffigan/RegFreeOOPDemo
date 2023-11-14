// See https://aka.ms/new-console-template for more information
using Esatto.Win32.Com;
using System.Runtime.InteropServices;

internal static class Program
{
    public static void Main(string[] args)
    {
        var child = ComInterop.CreateLocalServer<IRemoteObject>(Guid.Parse(Constants.CLSID_RemoteObject));
        Console.WriteLine("Created");
        child.RegisterCallback(new Callback());
        Console.WriteLine("Registered");

        while (true)
        {
            child.Print(Console.ReadLine() ?? throw new OperationCanceledException());
        }
    }
}

internal class Callback : IRemoteObject
{
    public void Print(string thing)
    {
        Console.WriteLine(thing);
    }

    public void RegisterCallback(IRemoteObject o)
    {
        throw new NotSupportedException();
    }
}