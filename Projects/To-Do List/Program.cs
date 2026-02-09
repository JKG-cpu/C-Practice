public class UIHelper
{
    public void WrongAnswer()
    {
        Console.WriteLine("That is not a valid option.");
        Console.Write("Press any key to continue. ");
        Console.ReadKey(false);
    }
}

public class TaskItem
{
    public string? Name { get; set; }
    public string? Info { get; set; }
}

public static class ToDoListManager
{
    private static List<TaskItem> taskList = new List<TaskItem>();

    public static void DisplayTasks()
    {
        if (taskList.Count == 0) Console.WriteLine("No tasks made.");
        
        foreach (var task in taskList)
        {
            Console.WriteLine($"Task: {task.Name} | Info: {task.Info}");
        }
    }

    public static void AddTask(string taskname, string info)
    {
        taskList.Add(new TaskItem { Name = taskname, Info = info });
    }
}

public class Program
{
    private UIHelper uIHelper = new();

    public static void Main()
    {
        bool running = true;

        while (running)
        {
            int taskAdded = AddTask("Test Task 1", "This is a test task");
            
            if (taskAdded == 1)
            {
                Console.WriteLine("The task was added.");
            } else
            {
                Console.WriteLine("Failed to add task.");
            }
            
            ToDoListManager.DisplayTasks();
            running = false;
        }
    }

    public static int AddTask(string? taskname, string? info)
    {
        if (string.IsNullOrEmpty(taskname))
        {
            return 0;
        }

        if (string.IsNullOrEmpty(info))
        {
            info = "";
        }

        ToDoListManager.AddTask(taskname, info);
        return 1;
    }
}