public class UIHelper
{
    public void WrongAnswer()
    {
        Console.WriteLine("\nThat is not a valid option.");
        Console.Write("Press any key to continue. ");
        Console.ReadKey(true);
    }

    public void DisplayOptions(List<string> options)
    {
        Console.WriteLine("--- Options ---");
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
    }
}

public class TaskItem
{
    public string Name { get; set; } = string.Empty;
    public string Info { get; set; } = string.Empty;
}

public static class ToDoListManager
{
    private static List<TaskItem> taskList = new List<TaskItem>();

    public static void DisplayTasks()
    {
        Console.WriteLine("--- TASKS ---");
        if (taskList.Count == 0) 
        {
            Console.WriteLine("No tasks made.");
        }
        else 
        {
            foreach (var task in taskList)
            {
                Console.WriteLine($"Task: {task.Name} | Info: {task.Info}");
            }
        }
        Console.WriteLine("------------------");
    }

    public static void AddTask(string taskname, string info)
    {
        taskList.Add(new TaskItem { Name = taskname, Info = info });
    }
}

public class Program
{
    private static UIHelper uIHelper = new();

    public static void Main()
    {
        bool running = true;
        List<string> options = ["Add Task", "View Tasks", "Remove Task", "Quit"];

        while (running)
        {
            Console.Clear();

            // Display options
            uIHelper.DisplayOptions(options);
            Console.Write("Enter in 1, 2, 3, 4 > ");

            // Get options
            string? userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                uIHelper.WrongAnswer();
                continue;
            }

            // Execute option
            switch (userInput)
            {
                case "1":
                    GetTaskDetails();
                    break;

                case "2":
                    ToDoListManager.DisplayTasks();
                    Console.Write("\nPress any key to continue. ");
                    Console.ReadKey(false);
                    break;

                case "3":
                    break;

                case "4":
                    running = false;
                    break;

                default:
                    uIHelper.WrongAnswer();
                    break;
            }
        }

        Console.Clear();
    }

    public static void GetTaskDetails()
    {
        bool running = true;

        bool taskName = false;
        string TaskName = "";

        bool taskInfo = false;
        string TaskInfo = "";

        while (running)
        {
            if (!taskName)
            {
                continue;
            }

            if (!taskInfo)
            {
                continue;
            }

            AddTask(TaskName, TaskInfo);
            running = false;
        }
    }

    public static int AddTask(string? taskname, string? info)
    {
        if (string.IsNullOrWhiteSpace(taskname))
        {
            return 0;
        }

        ToDoListManager.AddTask(taskname, info ?? "");
        return 1;
    }
}