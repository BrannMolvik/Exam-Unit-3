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

        public static void MessyBooks()
        {
            try
            {
                List<Book>? books = ReadBooksFromJson(FilePath);
                if (books != null)
                {
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

        private static List<Book> FilterBooksStartingWith(List<Book> books, string StartingWith)
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
            int index = input.IndexOf("(");
            if (index != -1)
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