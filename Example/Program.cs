using System;
using System.Diagnostics;
using System.IO;

class Talk {
    static void Main() {
        Console.WriteLine("💬 C#: Starting Python process...");

        var psi = new ProcessStartInfo {
            FileName = "python3",
            Arguments = "talk.py",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process();
        process.StartInfo = psi;
        process.Start();

        StreamWriter sw = process.StandardInput;
        StreamReader sr = process.StandardOutput;

        // Read Python's initial message
        Console.WriteLine("💬 C#: " + sr.ReadLine());

        // Send message to Python
        string message = "Hello Python, this is C#!";
        Console.WriteLine($"💬 C#: Sending message: {message}");
        sw.WriteLine(message);
        sw.Flush();

        // Read all responses from Python
        while (!sr.EndOfStream) {
            string? line = sr.ReadLine();
            if (line != null)
                Console.WriteLine("💬 C#: Got -> " + line);
        }

        process.WaitForExit();
        Console.WriteLine("💬 C#: Done!");
    }
}
