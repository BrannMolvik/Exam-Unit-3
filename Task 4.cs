using System.Text.Json;
using System.Text.Json.Serialization;

namespace task4
{
    class Program
    {
        private const string FilePath = @"C:\Users\BrannMolvik\Documents\examUnit3\example_files\books.json";
        private const string FileNotFound = "The file '{0}' was not found.";
        private const string ProblemMessage = "A problem has occurred with reading the file.";
        private const string StartingWith = "the";
        private const string SearchCharacter = "t";
        private const string BooksStartingWith = $"\u001b[34mBooks starting with '{StartingWith}':\u001b[0m";
        private const string AuthorsWithTMessage = $"\u001b[34mBooks by authors with '{SearchCharacter}' in their name:\u001b[0m";
        private const string ErrorJsonMessage = "An error occurred with the JSON: ";
        private const string FileDoesNotExistMessage = "The file does not exist.";
        private const string NoBooksFoundMessage = "No books found.";
        private const string NewLines = "\n\n\n";
        private const string BookInfo = "Title: {0}, Author: {1}, Publication Year: {2}, ISBN: {3}";
        const int StartYear = 1992;
        private static readonly string BooksAfterMessage = $"\u001b[34mNumber of books written in and after January {StartYear}: {{0}}\u001b[0m";
        private static readonly string BooksBeforeMessage = $"\u001b[34mNumber of books written before {BeforeYear}: {{0}}\u001b[0m";
        const int BeforeYear = 2004;

        private const string AuthorSearch = "Brandon Sanderson";
        private const string IsbnName = "ISBN: ";
        private const string AuthorSearchMessage = $"\u001b[34mISBN numbers by {AuthorSearch} are: \u001b[0m";
        private const string SortedBooks = $"\u001b[34mSorted books example age: \u001b[0m";
        private const string sortedBooksMessage = $"\u001b[34m{NewLines}Example of sorted books Chronologically: \u001b[0m";
        private const string Author = "Author: ";
        private const string AuthorMessage = $"\u001b[34m{NewLines}Here are books by each author grouped up:\u001b[0m";
        private const string AuthorMessageFirstName = $"\u001b[34m{NewLines}Here are books by each author grouped up by first name:\u001b[0m";

        public static void MessyBooks()
        {
            try
            {
                List<Book>? books = ReadBooksFromJson(FilePath);
                if (books != null)
                {
                    SortBooksAlphabeticallyAscending(books);

                    Console.WriteLine(SortedBooks);
                    foreach (var book in books)
                    {
                        Console.WriteLine(string.Format(BookInfo, book.TITLE, book.Author, book.Publication_year, book.Isbn));
                    }
                    Console.WriteLine(sortedBooksMessage);
                    SortBooksChronologicallyAscending(books);
                    foreach (var book in books)
                    {
                        Console.WriteLine(string.Format(BookInfo, book.TITLE, book.Author, book.Publication_year, book.Isbn));
                    }

                    Console.WriteLine(NewLines);

                    List<Book> booksStartingWithThe = FilterBooksStartingWith(books, StartingWith);

                    Console.WriteLine(BooksStartingWith);
                    foreach (var book in booksStartingWithThe)
                    {
                        Console.WriteLine(string.Format(BookInfo, book.TITLE, book.Author, book.Publication_year, book.Isbn));
                    }

                    Console.WriteLine(NewLines);

                    List<Book> authorsWithT = FilterAuthorsWithT(books);

                    Console.WriteLine(AuthorsWithTMessage);
                    foreach (var book in authorsWithT)
                    {
                        Console.WriteLine(string.Format(BookInfo, book.TITLE, book.Author, book.Publication_year, book.Isbn));
                    }

                    Console.WriteLine(NewLines);

                    int booksAfterYearCount = books.Count(book => book.Publication_year >= StartYear);
                    Console.WriteLine(string.Format(BooksAfterMessage, booksAfterYearCount));
                    Console.WriteLine(NewLines);

                    int booksBeforeYearCount = books.Count(book => book.Publication_year < BeforeYear);
                    Console.WriteLine(string.Format(BooksBeforeMessage, booksBeforeYearCount));
                    Console.WriteLine(NewLines);

                    List<string> isbnNumbers = IsbnNumbersByAuthor(books, AuthorSearch);
                    foreach (var isbn in isbnNumbers)
                    {
                        Console.WriteLine(IsbnName + isbn);
                    }

                    Console.WriteLine(AuthorMessageFirstName);
                    GroupBooksByAuthorFirstName(books);
                }
                else
                {
                    Console.WriteLine(NoBooksFoundMessage);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(string.Format(FileNotFound, FilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorJsonMessage + ex.Message);
            }
        }

        public static List<string> IsbnNumbersByAuthor(List<Book> books, string authorName)
        {
            List<string> isbnNumbers = new List<string>();
            Console.WriteLine(AuthorSearchMessage);
            foreach (var book in books)
            {
                if (book.Author != null && book.Author.Equals(authorName, StringComparison.OrdinalIgnoreCase))
                {
                    isbnNumbers.Add(book.Isbn ?? "");
                }
            }

            return isbnNumbers;
        }

        public static void SortBooksAlphabeticallyAscending(List<Book> books)
        {
            books.Sort((x, y) => string.Compare(x.TITLE, y.TITLE, StringComparison.OrdinalIgnoreCase));
        }

        public static void SortBooksAlphabeticallyDescending(List<Book> books)
        {
            books.Sort((x, y) => string.Compare(y.TITLE, x.TITLE, StringComparison.OrdinalIgnoreCase));
        }

        public static void SortBooksChronologicallyAscending(List<Book> books)
        {
            books.Sort((x, y) => x.Publication_year.CompareTo(y.Publication_year));
        }

        public static void SortBooksChronologicallyDescending(List<Book> books)
        {
            books.Sort((x, y) => y.Publication_year.CompareTo(x.Publication_year));
        }

        private static List<Book>? ReadBooksFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(FileDoesNotExistMessage);
                return null;
            }

            string jsonContent = File.ReadAllText(filePath);
            try
            {
                return JsonSerializer.Deserialize<List<Book>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorJsonMessage + ex.Message);
                return null;
            }
        }

        private static List<Book> FilterBooksStartingWith(List<Book> books, string startingWith)
        {
            return books.Where(book =>
                book.Author != null &&
                ContainsCharacter(book.Author, SearchCharacter) &&
                !ContainsCharacterAfterParentheses(book.Author, SearchCharacter))
                .ToList();
        }

        private static List<Book> FilterAuthorsWithT(List<Book> books)
        {
            return books.Where(book =>
                book.Author != null &&
                ContainsCharacterBeforeParentheses(book.Author, SearchCharacter))
                .ToList();
        }

        private static bool ContainsCharacterBeforeParentheses(string input, string character)
        {
            int index = input.IndexOf("(");  if (index != -1)
            {
                string substring = input.Substring(0, index);
                return ContainsCharacter(substring, character);
            }
            return ContainsCharacter(input, character);
        }

        private static bool ContainsCharacterAfterParentheses(string input, string character)
        {
            int index = input.IndexOf("(");
            if (index != -1)
            {
                string substring = input.Substring(index + 1);
                return ContainsCharacter(substring, character);
            }
            return false;
        }

        private static bool ContainsCharacter(string input, string character)
        {
            return input.IndexOf(character, StringComparison.OrdinalIgnoreCase) != -1;
        }

        public static void GroupBooksByAuthorFirstName(List<Book> books)
        {
            var groupedBooks = books.GroupBy(book =>
            {
                var authorParts = book.Author?.Split(' ');
                if (authorParts != null && authorParts.Length > 0)
                {
                    return authorParts.FirstOrDefault()?.Trim('(', ')');
                }
                return "";
            });

            foreach (var group in groupedBooks)
            {
                Console.WriteLine($"{Author}{group.Key}");
                foreach (var book in group)
                {
                    Console.WriteLine(string.Format(BookInfo, book.TITLE, book.Author?.Replace("(", ", ").Replace(")", ""), book.Publication_year, book.Isbn));
                }
                Console.WriteLine(NewLines);
            }
        }

        public class Book
        {
            [JsonPropertyName("title")]
            public string? TITLE { get; set; }

            [JsonPropertyName("publication_year")]
            public int Publication_year { get; set; }

            [JsonPropertyName("author")]
            public string? Author { get; set; }

            [JsonPropertyName("isbn")]
            public string? Isbn { get; set; }
        }
    }
}