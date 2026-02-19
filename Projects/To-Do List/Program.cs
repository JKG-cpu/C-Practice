using System.Text.Json;

public class UIHelper
{
    public void WrongAnswer(string message = "\nThat is not a valid option.")
    {
        Console.WriteLine(message);
        Console.Write("Press any key to continue. ");
        Console.ReadKey(true);
        Console.Clear();
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
    // File Name
    private const string FileName = "tasks.json";

    // Saving + Loading
    public static void SaveTasks()
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(taskList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Saving Tasks: {ex}");
        }
    }
    
    public static void LoadTasks()
    {
        try
        {
            if (File.Exists(FileName))
            {
                string jsonString = File.ReadAllText(FileName);
                taskList = JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? [];
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error Loading Tasks: {ex}");
            taskList = [];
        }
    }

    // Displaying Tasks
    public static void DisplayTasks()
    {
        Console.WriteLine("--- TASKS ---\n");

        if (taskList.Count == 0)
        {
            Console.WriteLine("No Tasks made.");
        }
        else
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                var task = taskList[i];
                if (task.Info == "")
                {
                    Console.WriteLine($"{i + 1}. {task.Name}\n    - Info: None Given");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {task.Name}\n    - Info: {task.Info}");
                }
            }
        }

        Console.WriteLine("\n------------------");
    }

    // Adding + Removing Tasks
    public static void AddTask(string taskname, string info)
    {
        taskList.Add(new TaskItem { Name = taskname, Info = info });
    }

    public static void RemoveTask(int index)
    {
        taskList.RemoveAt(index);
    }

    // Checking / Getting Task Info
    public static bool CheckValidTaskNumber(int number)
    {
        bool return_value = false;

        if (taskList.Count - 1 >= number)
        {
            return_value = true;
        }

        return return_value;
    }

    public static int GetAmountOfTasks()
    {
        return taskList.Count;
    }
}

public class Program
{
    private static UIHelper uIHelper = new();

    public static void Main()
    {
        bool running = true;
        List<string> options = ["Add Task", "View Tasks", "Remove Task", "Quit"];

        // Preload Tasks
        ToDoListManager.LoadTasks();

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
                    Console.Clear();
                    GetTaskDetails();
                    break;

                case "2":
                    Console.Clear();
                    ToDoListManager.DisplayTasks();
                    Console.Write("\nPress any key to continue. ");
                    Console.ReadKey(false);
                    break;

                case "3":
                    Console.Clear();
                    RemoveTask();
                    break;

                case "4":
                    running = false;
                    break;

                default:
                    uIHelper.WrongAnswer();
                    break;
            }
        }

        // Save Tasks
        ToDoListManager.SaveTasks();
        Console.Clear();
    }

    public static void GetTaskDetails()
    {
        bool running = true;

        bool taskName = false;
        string TaskName = "";

        bool taskInfo = false;
        string? TaskInfo = "";

        while (running)
        {
            // Get Task name
            if (!taskName)
            {
                Console.Write("Enter in a taskname > ");

                string? UserInput = Console.ReadLine();

                if (string.IsNullOrEmpty(UserInput) || string.IsNullOrWhiteSpace(UserInput))
                {
                    uIHelper.WrongAnswer("Please type something in.");
                    continue;
                }

                TaskName = UserInput;
                taskName = true;
            }

            // Get Task info
            if (!taskInfo)
            {
                Console.Write("Enter in task details > ");
                TaskInfo = Console.ReadLine();
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

    public static void RemoveTask()
    {
        if (ToDoListManager.GetAmountOfTasks() == 0)
        {
            Console.Write("You have made no tasks! Press any key to continue. ");
            Console.ReadKey(false);
            Console.Clear();
            return;
        }

        bool running = true;

        while (running)
        {
            // Fresh Display
            Console.Clear();
            
            if (ToDoListManager.GetAmountOfTasks() == 0)
            {
                Console.Write("You have made no tasks! Press any key to continue. ");
                Console.ReadKey(false);
                Console.Clear();
                return;
            }

            // Display tasks and text
            ToDoListManager.DisplayTasks();

            Console.Write("Enter in a task number you would like to delete > ");

            // Ask for input (number)
            string? UserInput = Console.ReadLine();

            // Check number
            if (string.IsNullOrEmpty(UserInput))
            {
                uIHelper.WrongAnswer("Please enter in a number.");
                continue;
            }

            if (int.TryParse(UserInput, out int number))
            {
                bool valid = ToDoListManager.CheckValidTaskNumber(number - 1);

                if (valid)
                {
                    // Remove Task
                    ToDoListManager.RemoveTask(number - 1);
                    Console.WriteLine("Task removed.");
                }
                else
                {
                    uIHelper.WrongAnswer("Please enter in a valid task number.");
                    continue;
                }
            }
            else
            {
                uIHelper.WrongAnswer("Please enter in a number.");
                continue;
            }

            // Ask if want to quit
            bool quitCheck = true;
            while (quitCheck)
            {
                Console.Write("\nDo you want to exit? (y/n) > ");

                string? userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    uIHelper.WrongAnswer("Please type y or n.");
                    continue;
                }

                switch (userInput.ToUpper())
                {
                    case "Y":
                        quitCheck = false;
                        running = false;
                        break;

                    case "N":
                        quitCheck = false;
                        break;

                    default:
                        uIHelper.WrongAnswer("Please type y or n.");
                        break;
                }
            }
        }
    }
}