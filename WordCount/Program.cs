﻿using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Emit;
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

            time.Start();
            methodInfo?.Invoke(null, parameters: new object?[] { text });
            time.Stop();
            Console.WriteLine("Common method:     " + time.ElapsedMilliseconds);

            time.Restart();
            WordWorker.GetWordsCountParallel(text);
            time.Stop();
            Console.WriteLine("Method with Parallel:     " + time.ElapsedMilliseconds);

            time.Restart();
            WordWorker.GetWordsCountWithThread(text);
            time.Stop();
            Console.WriteLine("Method with Thread:     " + time.ElapsedMilliseconds);

            time.Restart();
            WordWorker.GetWordsCountWithThreadPool(text);
            time.Stop();
            Console.WriteLine("Method with ThreadPool:     " + time.ElapsedMilliseconds);

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