using System.Text.Json;

namespace task2
{
    class Program
    {
        private const string FilePath = @"C:\Users\BrannMolvik\Documents\examUnit3\example_files\arrays.json";
        private const string FileNotFound = "The file '{0}' was not found.";
        private const string Error = "An error occurred while reading the file: {0}";
        private const string Problem = "A problem has occurred with reading the file.";

        public static void FlattenThoseNumbers()
        {
            try
            {
                string jsonContent = File.ReadAllText(FilePath);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    var flattenedList = FlattenArray(doc.RootElement);
                    Console.WriteLine(string.Join(", ", flattenedList));
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(string.Format(FileNotFound, FilePath));
            }
            catch (Exception)
            {
                Console.WriteLine(Problem);
            }
        }

        private static List<int> FlattenArray(JsonElement element)
        {
            List<int> flattenedList = new List<int>();
            if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Array)
                    {
                        flattenedList.AddRange(FlattenArray(item));
                    }
                    else if (item.ValueKind == JsonValueKind.Number)
                    {
                        flattenedList.Add(item.GetInt32());
                    }
                }
            }
            return flattenedList;
        }
    }
}