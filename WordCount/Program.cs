using System.Diagnostics;
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

            var time = new Stopwatch();

            time.Start();
            methodInfo?.Invoke(null, parameters: new object?[] { text });
            time.Stop();
            Console.WriteLine("Common method:     " + time.ElapsedMilliseconds);

            time.Restart();
            WordWorker.GetWordsCountParallel(text);
            time.Stop();
            Console.WriteLine("Method with Parallel:     " + time.ElapsedMilliseconds);

            time.Restart();
            res = WordWorker.GetWordsCountWithThread(text);
            time.Stop();
            Console.WriteLine("Method with Thread:     " + time.ElapsedMilliseconds);

            time.Restart();
            res = WordWorker.GetWordsCountWithThreadPool(text);
            time.Stop();
            Console.WriteLine("Method with ThreadPool:     " + time.ElapsedMilliseconds);

            var wordCounts = (Dictionary<string, int>)res;

            PrintRateToFile(WordWorker.SortDictionary(wordCounts), outputFilePath!);

            Console.WriteLine("Done.");
            Console.ReadLine();
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