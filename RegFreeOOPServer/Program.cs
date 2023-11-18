
using Esatto.Win32.Com;
using System.Runtime.InteropServices;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        using var res = new ClassObjectRegistration(Guid.Parse(Constants.CLSID_RemoteObject),
            ComInterop.CreateStaClassFactoryFor(() => new RemoteObject()),
            CLSCTX.LOCAL_SERVER, REGCLS.MULTIPLEUSE);

        Console.WriteLine("Started");
        Application.Run();
    }
}

internal class RemoteObject : StandardOleMarshalObject, IRemoteObject
{
    public RemoteObject()
    {
    }

    public void Print(string thing)
    {
        Console.WriteLine(thing);
    }

    public void RegisterCallback(object o2)
    {
        var o = (IRemoteObject)o2;
        Console.WriteLine("Callback registered");
        o.Print("Callback registered");

        // Yes, I know this is a bad use of thread-pool.  It's just a demo.
        ThreadPool.QueueUserWorkItem(_ =>
        {
            while (true)
            {
                o.Print(Console.ReadLine() ?? throw new OperationCanceledException());
            }
        }, null);
    }
}

