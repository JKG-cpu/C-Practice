using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
    
    public static List<T> GetPage<T>(List<T> list, int pageIndex, int pageSize = 10)
    {
        int start = pageIndex * pageSize;

        if (start >= list.Count)
            return []; // empty page

        int count = Math.Min(pageSize, list.Count - start);

        return list.GetRange(start, count);
    }

    public static void DisplayBookPage(int index, List<Book> books)
    {
        List<Book> page = GetPage(books, index);
        
        foreach (Book book in page)
        {
            book.DisplayInfo();
        }
    }
}

public class Book
{
    private string Title;
    private string Pages;
    private string Author;
    private int ID;

    public Book(string title, string pages, string author, int id)
    {
        Title = title;
        Pages = pages;
        Author = author;
        ID = id;
    }

    public int GetID() => ID;

    public void DisplayInfo()
    {
        int lineLength = $" Book Title: {Title}".Length;
        string line = $"+{new string('-', lineLength)}+";

        Console.WriteLine(line);
        Console.WriteLine($" Book Title: {Title}");
        Console.WriteLine($" Book ID: {ID}");
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

    public void ResetID(int id)
    {
        ID = id;
    }
}

public class Library
{
    private List<Book> books = new();

    private void ResetBookIDs()
    {
        for (int i = 0; i < books.Count; i++)
        {
            Book book = books[i];
            book.ResetID(i + 1);
        }
    }

    public int BooksCount()
    {
        return books.Count;
    }

    public (bool Success, List<Book>? matches, string? Info) FindBooks(string? Title, string? Pages = null, string? Author = null, int? BookID = null)
    {
        if (BookID != null)
        {
            foreach (Book book in books)
            {
                if (book.GetID() == BookID)
                {
                    return (true, [book], null);
                }
            }
        }

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
        int nextID = books.Count + 1;
        books.Add(new Book(title, pages, author, nextID));
    }

    public (bool Success, string? Info, List<Book>? Matches) RemoveBook(string? Title = null, string? Pages = null, string? Author = null)
    {
        (bool Success, List<Book>? matches, string? info) = FindBooks(Title, Pages, Author);

        // Handle results
        if (!Success || matches == null)
        {
            return (false, info, matches);
        }
        else
        {
            if (matches.Count > 1)
            {
                return (false, "To Many Books", matches);
            }
            books.Remove(matches[0]);
            ResetBookIDs();
            return (true, info, matches);
        }
    }

    public (bool Success, string? Info, List<Book>? Matches) EditBook(string? Title = null, string? Pages = null, string? Author = null)
    {
        (bool Success, List<Book>? matches, string? info) = FindBooks(Title, Pages, Author);

        if (!Success || matches == null)
        {
            return (Success, info, matches);
        }
        else if (matches.Count > 1)
        {
            return (Success, "Book Options", matches);
        }
        else
        {
            Book book = matches[0];
            book.ResetBookDetails(Title, Pages, Author);
            return (Success, null, matches);
        }
    }

    public void DisplayBooks(int index)
    {
        UIHelper.DisplayBookPage(index, books);
    }
}

class MyApp
{
    private static Library library = new();
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
                    library.DisplayBooks(0);
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

    // Manage Books
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
                }
                else if (userInput.ToLower().StartsWith("a"))
                {
                    (string Title, string Pages, string Author) = GetDetails();
                    library.AddBook(Title, Pages, Author);
                }
                else if (userInput.ToLower().StartsWith("r"))
                {
                    // Remove Book
                }
                else if (userInput.ToLower().StartsWith("e"))
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

    // Selecting books => Page Viewing
    private static Book? SelectBook()
    {
        bool running = true;
        List<string> options = [
            "Next Page", "Previous Page", "Exit"
        ];
        int index = 0;
        int pageLength = 10;

        while (running)
        {
            // Display Pages
            library.DisplayBooks(index);

            // Display Options
            UIHelper.DisplayOptions(options);

            Console.Write("Select an option or book (by ID) > ");
            string? userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("You must enter in an option...");
            }
            else
            {
                char fixedInput = userInput.ToLower()[0];

                switch (fixedInput)
                {
                    case 'n':
                        if (index + pageLength < library.BooksCount())
                            index += pageLength;
                        else
                            Console.WriteLine("You are on the last page...");
                        break;

                    case 'p':
                        if (index < pageLength)
                        {
                            index -= pageLength;
                        } else
                        {
                            Console.WriteLine("You are on the first page...");
                        }
                        break;

                    case 'e':
                        return null;

                    default:
                        try
                        {
                            int bookID = Convert.ToInt32(userInput);

                            (bool success, List<Book>? books, string? info) = library.FindBooks(null, null, null, bookID);
                            if (success && books != null)
                            {
                                return books[0];
                            } else
                            {
                                Console.WriteLine("That is not a valid book id...");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("That is not a valid option...");
                        }
                        break;
                }
            }
            Console.Write("Press Enter to continue > ");
            Console.ReadKey(true);
            Console.Clear();
        }

        return null;
    }

    // Methods for Book grabbing
    private static (string Title, string Pages, string Author) GetDetails()
    {
        string Title = "";
        string Pages = "";
        string Author = "";
        bool running = true;
        bool brunning = true;

        while (brunning)
        {
            // Get Title
            while (running)
            {
                Console.Write("Enter in the book title > ");
                string? titleInput = Console.ReadLine();

                if (string.IsNullOrEmpty(titleInput))
                {
                    Console.WriteLine("You must enter in a book title...");
                    continue;
                }

                Title = titleInput;
                running = false;
            }

            // Get Pages
            running = true;
            while (running)
            {
                Console.Write("Enter in the page amount for your book > ");
                string? pageInput = Console.ReadLine();

                if (string.IsNullOrEmpty(pageInput))
                {
                    Console.WriteLine("You must enter in a page amount...");
                    continue;
                }

                Pages = pageInput;
                running = false;
            }

            // Get Author
            running = true;
            while (running)
            {
                Console.Write("Enter in the author's name > ");
                string? authInput = Console.ReadLine();

                if (string.IsNullOrEmpty(authInput))
                {
                    Console.WriteLine("You must enter in a name...");
                    continue;
                }

                Author = authInput;
                running = false;
            }

            // Check
            running = true;
            while (running)
            {
                Console.WriteLine("Do you want to add this book?");
                Console.WriteLine($" Title: {Title} | Pages: {Pages} | Author: {Author}");
                Console.Write("Input (y/n) > ");

                string? userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    if (userInput.ToLower().StartsWith("y"))
                    {
                        running = false;
                        brunning = false;
                    }
                    else if (userInput.ToLower().StartsWith("n"))
                    {
                        running = false;
                    }
                    else { Console.WriteLine("Please enter y or n..."); }
                }
                else
                {
                    Console.WriteLine("Yes or no...");
                }
            }
            Console.Clear();
        }

        return (Title, Pages, Author);
    }

    private static (string Title, string Pages, string Author) RemoveBook()
    {
        string Title = "";
        string Pages = "";
        string Author = "";



        return (Title, Pages, Author);
    }

}
