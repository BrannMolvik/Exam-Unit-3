using System.Text.Json;

namespace task3
{
    class Program
    {
        private const string FilePath = @"C:\Users\BrannMolvik\Documents\examUnit3\example_files\nodes.json";
        private const string FileNotFoundMessage = "The file '{0}' was not found.";
        private const string ProblemMessage = "A problem has occurred with reading the file.";
        private const string ValueKey = "value";
        private const char SpaceChar = ' ';
        private const int TabLength = 4;
        private const string SumOutputFormat = "Sum = {0}";
        private const string MaxTabs = "Deepest level/Max tabs found: {0}";
        private const string Nodes = "Nodes found: {0}";
        private const string NumberNodes = "Amount of nodes: {0}";

        public static void LeftAndRight()
        {
            try
            {
                int sum = CalculateSumFromJson();
                Console.WriteLine(SumOutputFormat, sum);

                int maxTabs = FindMaxConsecutiveTabs();
                Console.WriteLine(MaxTabs, maxTabs);

                int Nodes = NumberValueTimes();
                Console.WriteLine(NumberNodes, Nodes);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(string.Format(FileNotFoundMessage, FilePath));
            }
            catch (Exception)
            {
                Console.WriteLine(ProblemMessage);
            }
        }

        private static int CalculateSumFromJson()
        {
            string jsonContent = File.ReadAllText(FilePath);
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                JsonElement root = doc.RootElement;
                return CalculateSum(root);
            }
        }

        private static int CalculateSum(JsonElement element)
        {
            int sum = 0;
            foreach (JsonProperty property in element.EnumerateObject())
            {
                if (property.Name == ValueKey && property.Value.ValueKind == JsonValueKind.Number)
                {
                    sum += property.Value.GetInt32();
                }
                else if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    sum += CalculateSum(property.Value);
                }
            }
            return sum;
        }

        private static int FindMaxConsecutiveTabs()
        {
            int maxTabs = 0;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int count = CountConsecutiveTabs(line);
                    maxTabs = Math.Max(maxTabs, count);
                }
            }
            return maxTabs;
        }

        private static int CountConsecutiveTabs(string line)
        {
            int maxCount = 0;
            int count = 0;
            foreach (char c in line)
            {
                if (c == SpaceChar)
                {
                    count++;
                    if (count == TabLength)
                    {
                        maxCount++;
                        count = 0;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return maxCount;
        }

        private static int NumberValueTimes()
        {
            int occurrences = 0;
            string jsonContent = File.ReadAllText(FilePath);
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                JsonElement root = doc.RootElement;
                occurrences += CountOccurrences(root);
            }
            return occurrences;
        }

        private static int CountOccurrences(JsonElement element)
        {
            int count = 0;
            foreach (JsonProperty property in element.EnumerateObject())
            {
                if (property.Name == ValueKey && property.Value.ValueKind == JsonValueKind.Number)
                {
                    count++;
                }
                else if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    count += CountOccurrences(property.Value);
                }
            }
            return count;
        }
    }
}