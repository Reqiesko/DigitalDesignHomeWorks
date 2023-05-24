namespace DigitalDesignDll
{
    public class WordWorker
    {
        private Dictionary<string, int> GetWordsCount(string text)
        {
            // Создаём словарь для подсчёта уникальных слов
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            // Читаем входной файл и разбиваем его на слова
            //using (StreamReader reader = new StreamReader(inputFilePath))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        string? line = reader.ReadLine();
            //        string[] words = line!.Split(new[] { ' ', '\t', ',', '.', ':', ';', '!', '?', '(', ')', '[', ']', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            //
            //        // Добавляем слова в словарь или увеличиваем счётчик, если они уже есть в словаре
            //        foreach (string word in words)
            //        {
            //            string lowercaseWord = word.ToLower();
            //            if (!wordCounts.ContainsKey(lowercaseWord))
            //            {
            //                wordCounts[lowercaseWord] = 1;
            //            }
            //            else
            //            {
            //                wordCounts[lowercaseWord]++;
            //            }
            //        }
            //    }
            //}


            string[] words = text.Split(new[] { ' ', '\t', ',', '.', ':', ';', '!', '?', '(', ')', '[', ']', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

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