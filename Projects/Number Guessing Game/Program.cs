public class UIHelper
{
    public void ClearConsole()
    {
        Console.WriteLine("That is not a valid option.");
        Console.Write("Press any key to continue. ");
        Console.ReadKey();
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
        // 0 = Correct, 1 = Greater, -1 = Smaller
        int placement = 0;

        if (userGuess > number)
        {
            placement = 1;
        } else if (userGuess < number)
        {
            placement = -1;
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

        }
    }
}