using System.Diagnostics;
using System.Text.Json;

public class PythonResponse
{
    public string Password { get; set; } = string.Empty;
}

public class Program
{
    public static string GetSecurePassword(int length)
    {
        ProcessStartInfo start = new()
        {
            FileName = "python",
            Arguments = $"generator.py {length}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process? process = Process.Start(start);

        if (process == null)
        {
            return "Error: Could not start the Python process";
        }

        using StreamReader reader = process.StandardOutput;
        string jsonResult = reader.ReadToEnd();

        var response = JsonSerializer.Deserialize<PythonResponse>(jsonResult);

        return response?.Password ?? "Error: No password generated";
    }

    public static void Main()
    {
        string password = GetSecurePassword(100);
        Console.WriteLine(password);
    }
}