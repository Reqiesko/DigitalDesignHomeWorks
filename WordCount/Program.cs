using System.Reflection;
using DigitalDesignDll;

namespace WordCount
{
    

    class Program
    {
        static void Main()
        {
            var inputFilePath = Console.ReadLine();
            var outputFilePath = Console.ReadLine();

            var wordWorker = new WordWorker();

            var methodInfo = typeof(WordWorker).GetMethod("GetWordsCount",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            var answerInvoke = methodInfo?.Invoke(wordWorker, parameters: new object?[] { inputFilePath });

            var wordCounts = (Dictionary<string, int>)answerInvoke!;

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