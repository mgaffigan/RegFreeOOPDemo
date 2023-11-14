
using Esatto.Win32.Com;
using System.Windows.Forms;


using var res = new ClassObjectRegistration(Guid.Parse(Constants.CLSID_RemoteObject),
    ComInterop.CreateClassFactoryFor(() => new RemoteObject()),
    CLSCTX.LOCAL_SERVER, REGCLS.MULTIPLEUSE);

Console.WriteLine("Started");
Application.Run();

internal class RemoteObject : IRemoteObject
{
    public RemoteObject()
    {
    }

    public void Print(string thing)
    {
        Console.WriteLine(thing);
    }

    public void RegisterCallback(IRemoteObject o)
    {
        Console.WriteLine("Callback registered");

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

