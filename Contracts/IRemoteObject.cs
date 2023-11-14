using System.Runtime.InteropServices;

public static class Constants
{
    public const string 
        CLSID_RemoteObject = "87E71E12-0D33-44F7-B1C2-4827F4FA180D";
}

[ComVisible(true), Guid("E27C3BDB-ACEF-44F9-8568-481D44681C04"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IRemoteObject
{
    void Print(string thing);
    void RegisterCallback(IRemoteObject o);
}
