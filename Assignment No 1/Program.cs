using System;
using System.Collections.Generic;

class Program
{
    static List<Task> tasks = new List<Task>();

    static void Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("\n\n 1.create and add item in list  \n 2.update list at specific index \n 3.delete iteam from list \n 4.show list  \n 5.exit\n");
            Console.Write("Select an option: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateTask();
                    break;
                case 2:
                    ReadTasks();
                    break;
                case 3:
                    UpdateTask();
                    break;
                case 4:
                    DeleteTask();
                    break;
                case 5:
                    Console.WriteLine("Exiting application...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateTask()
    {
        Console.WriteLine("\nHow many tasks do you want to add?");
        int size = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nEnter items:");
        for (int i = 1; i <= size; i++)
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();
            tasks.Add(new Task(title, description));
        }

        Console.WriteLine("\nTasks created successfully.");
    }

    static void ReadTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.WriteLine("\nTask List:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"Title: {task.Title}, Description: {task.Description}");
        }
    }

    static void UpdateTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available to update.");
            return;
        }

        Console.Write("\nEnter task title to update: ");
        string titleToUpdate = Console.ReadLine();
        Task taskToUpdate = tasks.Find(t => t.Title.Equals(titleToUpdate));

        if (taskToUpdate == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        Console.WriteLine("Enter new title (leave blank to keep current): ");
        string newTitle = Console.ReadLine();
        if (!string.IsNullOrEmpty(newTitle))
            taskToUpdate.Title = newTitle;

        Console.WriteLine("Enter new description (leave blank to keep current): ");
        string newDescription = Console.ReadLine();
        if (!string.IsNullOrEmpty(newDescription))
            taskToUpdate.Description = newDescription;

        Console.WriteLine("Task updated successfully.");
    }

    static void DeleteTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available to delete.");
            return;
        }

        Console.Write("\nEnter task title to delete: ");
        string titleToDelete = Console.ReadLine();
        Task taskToDelete = tasks.Find(t => t.Title.Equals(titleToDelete));

        if (taskToDelete == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        tasks.Remove(taskToDelete);
        Console.WriteLine("Task deleted successfully.");
    }
}

class Task
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Task(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
