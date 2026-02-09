public class UIHelper
{
    public void ClearConsole()
    {
        Console.WriteLine("That is not a valid option.");
        Console.Write("Press any key to continue. ");
        Console.ReadKey(false);
        Console.Clear();
    }
}

public class Program
{
    private static Random random = new();
    private static UIHelper uihelper = new();

    public static void Main()
    {
        Console.Clear();

        bool running = true;

        while (running)
        {
            Console.Write("Would you like to try to guess the number? (y/n) > ");

            string? UserInput = Console.ReadLine();
            if (string.IsNullOrEmpty(UserInput))
            {
                uihelper.ClearConsole();
                continue;
            }

            switch (UserInput.ToUpper())
            {
                case "Y":
                    PlayGame();
                    break;

                case "N":
                    running = false;
                    break;

                default:
                    uihelper.ClearConsole();
                    break;
            }
        }
    }

    private static int GetNumber(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    private static int CheckGuess(int userGuess, int number)
    {
        // 0 = Correct, 1 = Higher, -1 = Lower
        int placement = 0;

        if (userGuess > number)
        {
            placement = -1;
        } else if (userGuess < number)
        {
            placement = 1;
        }

        return placement;
    }

    private static void PlayGame()
    {
        Console.Clear();

        int number = GetNumber(1, 100);
        bool guessed = false;

        while (!guessed)
        {
            Console.Write("Enter a number to guess (1 - 100) > ");

            string? UserGuess = Console.ReadLine();

            if (string.IsNullOrEmpty(UserGuess))
            {
                uihelper.ClearConsole();
                continue;
            }

            try
            {
                int UserInput = int.Parse(UserGuess);

                int difference = CheckGuess(UserInput, number);

                if (difference == 0)
                {
                    Console.WriteLine($"You have guessed correctly! The number was {number}");
                    Console.Write("Press any key. ");
                    Console.ReadKey(false);
                    Console.Clear();
                    guessed = true;
                } else if (difference == 1)
                {
                    Console.WriteLine("Guess Higher!");
                    Console.Write("Press any key. ");
                    Console.ReadKey(false);
                    Console.Clear();
                } else if (difference == -1)
                {
                    Console.WriteLine("Guess Lower!");
                    Console.Write("Press any key. ");
                    Console.ReadKey(false);
                    Console.Clear();
                }

            }
            catch
            {
                uihelper.ClearConsole();
                continue;
            }
        }
    }
}