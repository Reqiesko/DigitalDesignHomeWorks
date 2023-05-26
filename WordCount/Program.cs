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

            var text = File.ReadAllText(inputFilePath!);

            var methodInfo = typeof(WordWorker).GetMethod("GetWordsCount", BindingFlags.Static | BindingFlags.NonPublic);

            var res = new object();

            Parallel.Invoke(
                () =>
                {
                    WordWorker.GetWordsCountParallel(text);
                },

                () =>
                {
                    methodInfo?.Invoke(null, parameters: new object?[] { text });
                },

                () =>
                {
                    res = WordWorker.GetWordsCountWhithTask(text);
                });

            var wordCounts = (Dictionary<string, int>)res;

            // Сортируем словарь по убыванию количества употреблений слов
            var sortedWordCounts = wordCounts.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            PrintRateToFile(sortedWordCounts, outputFilePath!);

            Console.WriteLine("Done.");
        }

        private static void PrintRateToFile(Dictionary<string, int> sortedWordCounts, string outputFilePath)
        {
            // Записываем результаты в выходной файл
            using var writer = new StreamWriter(outputFilePath);
            foreach (var wordCount in sortedWordCounts)
            {
                writer.WriteLine("{0}\t{1}", wordCount.Key, wordCount.Value);
            }
        }
    }
}