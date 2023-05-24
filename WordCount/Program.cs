using System.Reflection;
using System.Reflection.Emit;
using DigitalDesignDll;

namespace WordCount
{
    class Program
    {
        private static void Main()
        {
            Console.WriteLine("Путь к исходному файлу");
            var inputFilePath = Console.ReadLine();
            Console.WriteLine("Путь к файлу с результатом");
            var outputFilePath = Console.ReadLine();

            string text = File.ReadAllText(inputFilePath!);

            var wordWorker = new WordWorker();

            var methodInfo = typeof(WordWorker).GetMethod("GetWordsCount",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            var wordCounts = (Dictionary<string, int>)methodInfo?.Invoke(wordWorker, parameters: new object?[] { text })!;

            // Сортируем словарь по убыванию количества употреблений слов
            var sortedWordCounts = wordCounts.OrderByDescending(x => x.Value);

            // Записываем результаты в выходной файл
            using (var writer = new StreamWriter(outputFilePath!))
            {
                foreach (var wordCount in sortedWordCounts)
                {
                    writer.WriteLine("{0}\t{1}", wordCount.Key, wordCount.Value);
                }
            }

            Console.WriteLine("Done.");
        }
    }
}