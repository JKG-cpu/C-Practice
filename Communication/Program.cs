using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Communication
{
    static async Task Main()
    {
        var psi = new ProcessStartInfo
        {
            FileName = "python3",                 // or "python" on Windows
            Arguments = "communication.py",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = psi };
        process.Start();

        using StreamWriter sw = process.StandardInput;
        using StreamReader sr = process.StandardOutput;

        // Task to continuously read Python output
        var readTask = Task.Run(async () =>
        {
            while (!sr.EndOfStream)
            {
                string? line = await sr.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                    Console.WriteLine($"💬 Python: {line}");
            }
        });

        Console.WriteLine("💬 C#: Type messages to send to Python (type 'exit' to quit):");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null || input.Trim().ToLower() == "exit")
                break;

            await sw.WriteLineAsync(input);
            await sw.FlushAsync();
        }

        // Graceful shutdown: tell Python to stop
        await sw.WriteLineAsync("exit");
        await sw.FlushAsync();

        process.Kill(); // if Python still somehow runs
        await readTask;
        Console.WriteLine("💬 C#: Program exiting.");
    }
}
