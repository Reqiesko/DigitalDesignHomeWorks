using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DigitalDesignDll;

namespace WordCount
{
    class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Путь к исходному файлу");
            var inputFilePath = Console.ReadLine();
            Console.WriteLine("Путь к файлу с результатом");
            var outputFilePath = Console.ReadLine();

            var text = File.ReadAllText(inputFilePath!);

            var methodInfo = typeof(WordWorker).GetMethod("GetWordsCount", BindingFlags.Static | BindingFlags.NonPublic);

            var res = new object();

            var time = new Stopwatch();

            var stringBuilder = new StringBuilder();

            var methods = typeof(WordWorker).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Where(m => m.ReturnType == typeof(Dictionary<string, int>) 
                            && m.GetParameters().Length == 1
                            && m.GetParameters().First().ParameterType == typeof(string));

            foreach (var method in methods)
            {
                time.Restart();
                method.Invoke(null, new object?[] {text});
                time.Stop();
                Console.WriteLine(stringBuilder.Append(method.Name + ": " + time.ElapsedMilliseconds));
                stringBuilder.Clear();
            }

            time.Restart();
            res = await RunAsyncPost(text);
            time.Stop();
            Console.WriteLine("WebApiService with Parallel:     " + time.ElapsedMilliseconds);

            var wordCounts = (Dictionary<string, int>)res!;

            PrintRateToFile(WordWorker.SortDictionary(wordCounts), outputFilePath!);

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        public static async Task<IDictionary<string, int>?> RunAsyncPost(string text)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();

                var content = new StringContent(JsonSerializer.Serialize(text));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                
                var wordCounts = new Dictionary<string, int>();

                HttpResponseMessage response = await client.PostAsync("/api/WordWorker", content);
                if (response.IsSuccessStatusCode)
                {
                    wordCounts = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
                }

                return wordCounts;
            }
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