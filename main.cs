using System;
using System.ComponentModel;
using System.Diagnostics;

public class Sudo
{
    public static void Main()
    {
        Console.WriteLine("something");

        ProcessStartInfo processInfo = new ProcessStartInfo();
        processInfo.Verb = "runas";
        processInfo.FileName = "notepad.exe";
        try
        {
            Process.Start(processInfo);
        }
        catch (Win32Exception)
        {
            //Do nothing. Probably the user canceled the UAC window
        }
    }
}
