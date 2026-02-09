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
        Console.WriteLine("\n--- TODO MENU ---");
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.Write("Select an option: ");
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
        List<string> options = new() { "Add Task", "View Tasks", "Quit" };
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