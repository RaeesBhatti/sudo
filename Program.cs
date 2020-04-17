using System;
using System.ComponentModel;
using System.Diagnostics;

public class Sudo
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("no program specified");
            return;
        } 
        ProcessStartInfo processInfo = new ProcessStartInfo();
        processInfo.Verb = "runas";
        processInfo.FileName = args[0];
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
