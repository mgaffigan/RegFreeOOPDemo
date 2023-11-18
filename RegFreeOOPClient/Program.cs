// See https://aka.ms/new-console-template for more information
using Esatto.Win32.Com;
using System.Runtime.InteropServices;

internal static class Program
{
    public static ActivationContext ActCtx { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        ActCtx = typeof(IRemoteObject).Assembly.CreateActivationContext();
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        //new Thread(RunBackground).Start();
        //Application.Run();
        RunBackground(null);
    }

    private static void RunBackground(object? obj)
    {
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        using var context = ActCtx.Enter();
        
        var child = ComInterop.CreateLocalServer<IRemoteObject>(Guid.Parse(Constants.CLSID_RemoteObject));
        child.RegisterCallback(new Callback());
        Console.WriteLine("Registered");

        while (true)
        {
            child.Print(Console.ReadLine() ?? throw new OperationCanceledException());
        }
    }
}

internal class Callback : StandardOleMarshalObject, IRemoteObject, ICustomQueryInterface
{
    public CustomQueryInterfaceResult GetInterface(ref Guid iid, out nint ppv)
    {
        if (iid == typeof(IRemoteObject).GUID)
        {
            ppv = Marshal.GetComInterfaceForObject(this, typeof(IRemoteObject), CustomQueryInterfaceMode.Ignore);
            return CustomQueryInterfaceResult.Handled;
        }

        ppv = 0;
        return CustomQueryInterfaceResult.NotHandled;
    }

    public void Print(string thing)
    {
        Console.WriteLine(thing);
    }

    public void RegisterCallback(object o)
    {
        throw new NotSupportedException();
    }
}