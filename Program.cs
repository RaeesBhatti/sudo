using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;

public class Sudo
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("no program specified");
            return;
        };

        string[] processArguments = new string[args.Length];
        for (int i = 1; i < args.Length; i++)
        {
            processArguments[i] = args[i];
        }

        SecureString pass = new SecureString();

        do
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            // Backspace Should Not Work
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                pass.AppendChar(key.KeyChar);
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        } while (true);

        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop - 1);

        ProcessStartInfo processInfo = new ProcessStartInfo {
            FileName = args[0],
            Domain = Environment.MachineName,
            UserName = System.Environment.UserName,
            Password = pass,
            Arguments = String.Join(" ", processArguments),
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
        };
        Process process = Process.Start(processInfo);
        process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
        process.ErrorDataReceived += (sender, e) => Console.Error.WriteLine(e.Data);
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();
    }
}
