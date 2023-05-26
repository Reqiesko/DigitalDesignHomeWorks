using System.Collections.Concurrent;
using System.Diagnostics;

namespace DigitalDesignDll
{
    public static class WordWorker
    {
        private static readonly char[] Separators = { ' ', '\t', ',', '.', ':', ';', '!', '?', '(', ')', '[', ']', '{', '}', '-', '–', '\n' };

        public static Dictionary<string, int> GetWordsCountWithThreadPool(string text)
        {
            var wordCounts = new ConcurrentDictionary<string, int>();

            string[] words = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            var countdownEvent = new CountdownEvent(words.Length);

            foreach (string word in words)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    string lowercaseWord = word.ToLower();
                    wordCounts.AddOrUpdate(lowercaseWord, 1, (_, count) => count + 1);
                    countdownEvent.Signal();
                });
            }

            countdownEvent.Wait();

            return wordCounts.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static Dictionary<string, int> GetWordsCountWithThreads(string text)
        {
            // Создаём словарь для подсчёта уникальных слов
            var wordCounts = new ConcurrentDictionary<string, int>();

            // Добавляем слова в словарь или увеличиваем счётчик, если они уже есть в словаре
            var thread = new Thread(() =>
            {
                var words = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    string lowercaseWord = word.ToLower();
                    wordCounts.AddOrUpdate(lowercaseWord, 1, (_, count) => count + 1);
                }
            });

            thread.Start();
            thread.Join();

            return wordCounts.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static Dictionary<string, int> GetWordsCountParallel(string text)
        {
            // Создаём словарь для подсчёта уникальных слов
            ConcurrentDictionary<string, int> wordCounts = new ConcurrentDictionary<string, int>();

            string[] words = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            // Добавляем слова в словарь или увеличиваем счётчик, если они уже есть в словаре
            Parallel.ForEach(words, word =>
            {
                string lowercaseWord = word.ToLower();
                wordCounts.AddOrUpdate(lowercaseWord, 1, (_, count) => count + 1);
                
            });
            
            return wordCounts.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static Dictionary<string, int> GetWordsCount(string text)
        {
            // Создаём словарь для подсчёта уникальных слов
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            string[] words = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            // Добавляем слова в словарь или увеличиваем счётчик, если они уже есть в словаре
            foreach (string word in words)
            {
                string lowercaseWord = word.ToLower();
                if (!wordCounts.ContainsKey(lowercaseWord))
                {
                    wordCounts[lowercaseWord] = 1;
                }
                else
                {
                    wordCounts[lowercaseWord]++;
                }
            }

            return wordCounts;
        }
    }
}