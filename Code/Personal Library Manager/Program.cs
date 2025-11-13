using System;
using System.Collections.Generic;

public static class UIHelper
{
    public static void DisplayOptions(List<string> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Console.Write($" {options[i]}");
            if (i < options.Count - 1)
                Console.Write(" |");
        }
        Console.WriteLine();
    }
}

public class Book
{
    private string Title;
    private string Pages;
    private string Author;

    public Book(string title, string pages, string author)
    {
        Title = title;
        Pages = pages;
        Author = author;
    }

    public void DisplayInfo()
    {
        int lineLength = $" Book Title: {Title}".Length;
        string line = $"+{new string('-', lineLength)}+";

        Console.WriteLine(line);
        Console.WriteLine($" Book Title: {Title}");
        Console.WriteLine(line);
        Console.WriteLine($" Pages: {Pages}");
        Console.WriteLine($" Author: {Author}");
        Console.WriteLine();
    }

    public (string Title, string Pages, string Author) BookDetails()
    {
        return (Title, Pages, Author);
    }

    public void ResetBookDetails(string? title, string? pages, string? author)
    {
        if (!string.IsNullOrEmpty(title))
        {
            Title = title;
        }
        if (!string.IsNullOrEmpty(pages))
        {
            Pages = pages;
        }
        if (!string.IsNullOrEmpty(author))
        {
            Author = author;
        }
    }
}

public class Library
{
    private List<Book> books = new();

    private (bool Success, List<Book>? matches, string? Info) FindBooks(string? Title, string? Pages = null, string? Author = null)
    {
        if (string.IsNullOrEmpty(Title))
            return (false, null, "You must provide a title.");

        List<Book> matches = new();

        // Find all matching books
        foreach (Book book in books)
        {
            (string btitle, string bpages, string bauthor) = book.BookDetails();

            bool match = btitle == Title &&
                         (string.IsNullOrEmpty(Pages) || bpages == Pages) &&
                         (string.IsNullOrEmpty(Author) || bauthor == Author);

            if (match)
                matches.Add(book);
        }
        return (true, matches, null);
    }

    public void AddBook(string title, string pages, string author)
    {
        books.Add(new Book(title, pages, author));
    }

    public (bool Success, string? Info, List<Book>? Matches) RemoveBook(string? Title = null, string? Pages = null, string? Author = null)
    {
        (bool Success, List<Book>? matches, string? info) = FindBooks(Title, Pages, Author);

        // Handle results
        if (!Success)
        {
            return (false, info, matches);
        } else
        {
            return (true, info, matches);
        }
    }

    public (bool Success, string? Info, List<Book>? Matches) EditBook(string? Title = null, string? Pages = null, string? Author = null)
    {
        (bool Success, List<Book>? matches, string? info) = FindBooks(Title, Pages, Author);

        if (!Success || matches == null)
        {
            return (Success, info, matches);
        } else if (matches.Count > 1)
        {
            return (Success, "Book Options", matches);
        } else
        {
            Book book = matches[0];
            book.ResetBookDetails(Title, Pages, Author);
            return (Success, null, matches);
        }
    }

    public void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.\n");
            return;
        }

        foreach (Book book in books)
            book.DisplayInfo();
    }
}

class MyApp
{
    static void Main()
    {
        Console.Clear();

        // Base Vars
        bool running = true;
        List<string> options = new()
        {
            "Manage Books", "View Books", "Settings", "Exit"
        };

        // Instances
        Library library = new();
        library.AddBook("Test 1", "123", "Jesse K.");
        library.AddBook("Test 2", "123", "Jesse K.");
        library.AddBook("Test 3", "123", "Jesse K.");

        // Main Loop
        while (running)
        {
            UIHelper.DisplayOptions(options);

            // Get User Input
            Console.Write("Select an option > ");

            string? userInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInput))
            {
                if (userInput.ToLower().StartsWith("e"))
                {
                    running = false;
                    Console.Clear();
                    continue;
                }
                else if (userInput.ToLower().StartsWith("m"))
                {
                    // Manage Books => Add, Remove, Edit
                    ManageBooks();
                }
                else if (userInput.ToLower().StartsWith("v"))
                {
                    // View Books
                    library.DisplayBooks();
                }
                else if (userInput.ToLower().StartsWith("s"))
                {
                    // Settings
                }

            }
            else
            {
                Console.WriteLine("You must type something...");
            }

            Console.Write("Press Enter to continue > ");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
    private static void ManageBooks()
    {
        bool running = true;
        List<string> options =
        [
            "Add Book", "Remove Book", "Edit Book", "Exit"
        ];

        while (running)
        {
            UIHelper.DisplayOptions(options);

            Console.Write("Select an option > ");
            string? userInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInput))
            {
                if (userInput.ToLower().StartsWith("e"))
                {
                    running = false;
                    Console.Clear();
                    continue;
                } else if (userInput.ToLower().StartsWith("a"))
                {
                    // Add Book
                } else if (userInput.ToLower().StartsWith("r"))
                {
                    // Remove Book
                } else if (userInput.ToLower().StartsWith("e"))
                {
                    // Edit Book
                }
            }
            else
            {
                Console.WriteLine("You must type something in...");
            }
            Console.Write("Press Enter to continue > ");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
