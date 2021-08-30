using System;
using System.Collections.Generic;
using System.Linq;

namespace Gotoinc_task
{
    class WordClass
    {
        public int Count { get; set; }
        public string Word { get; set; }
    }
    class Program
    {
        static int numberOfTask = 0;

        static class WordObject
        {
            static int Count { get; set; }
            static string Word { get; set; }
        }
        static void Main(string[] args)
        {
            while (numberOfTask != 9)
            {
                Console.Write("Choose task 1-2 and 9 for exit: ");
                int choosenTaskNumber = int.Parse(Console.ReadLine());

                switch (choosenTaskNumber)
                {
                    case 1:
                        Task_1();
                        break;
                    case 2:
                        Task_2();
                        break;
                    case 9:
                        Console.WriteLine("App is down...");
                        numberOfTask = 9;
                        break;
                }
            }
        }

        static void Task_1()
        {
            Console.Write("------Task 1-------\nEnter string for encription: ");
            string str = Console.ReadLine();

            Console.Write("Enter the count of repetitions: ");
            int countOfRepetitions = int.Parse(Console.ReadLine());

            Console.WriteLine("Encrypted result for \"{0}\" with n = {1}: {2}", str, countOfRepetitions, Encrypt(str, countOfRepetitions));
            Console.WriteLine("Decrupted result for \"{0}\" with n = {1}: {2}", str, countOfRepetitions, Decrypt(Encrypt(str, countOfRepetitions), countOfRepetitions));
        }
        private static string Encrypt(string text, int n)
        {
            if (n <= 0)
            {
                return text;
            }

            string even = "";
            string odd = "";

            for (var i = 1; i <= n; i++)
            {
                char[] arr = text.ToCharArray();
                even = "";
                odd = "";
                for (var j = 0; j < arr.Length; j++)
                {
                    if (j % 2 == 0)
                    {
                        even += arr[j];
                    } else if (j % 2 != 0)
                    {
                        odd += arr[j];
                    }
                }
                text = odd + even;
            }

            return text;
        }
        private static string Decrypt(string encrypted_text, int n)
        {
            if (n <= 0)
            {
                return encrypted_text;
            }
            
            int length = encrypted_text.Length / 2;
            bool evenCountOfNumbers = true;

            if (encrypted_text.Length % 2 != 0)
            {
                evenCountOfNumbers = false;
            }

            for (var i = 1; i <= n; i++)
            {
                string res = "";
                char[] arr = encrypted_text.ToCharArray();
                for (var j = 0; j < length; j++)
                {
                    res += arr[length + j];
                    res += arr[j];
                }

                if (evenCountOfNumbers == false)
                {
                    res += arr[arr.Length - 1];
                    encrypted_text = res;
                } else
                {
                    encrypted_text = res;
                }
            }

            return encrypted_text;
        }

        static void Task_2()
        {
            Console.Write("------Task 2------\nEnter the text: ");
            string str = Console.ReadLine();
            FindWords(str);
        }

        private static void FindWords(string text)
        {
            char[] arr = text.ToCharArray();

            List<WordClass> words = new List<WordClass>();

            for (var i = 0; i < arr.Length; i++)
            {
                if (char.IsPunctuation(arr[i]) && arr[i] != '\'')
                {
                    arr[i] = ' ';
                }
            }

            string res = new string(arr);
            res = res.ToLower();

            string[] wordsArr = res.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var result = wordsArr.GroupBy(x => x).Select(x => new { Word = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).Take(3);

            var test = result.Where(x => x.Count > 1).Select(x => new { Word = x.Word, Count = x.Count });

            if (test.Count() < 3)
            {
                arr = null;
                Console.WriteLine("Популярных слов меньше трёх.");
            }
            else
            {
                foreach (var word in result)
                {
                    Console.WriteLine("\'{0}\' повторилось {1} раз", word.Word, word.Count);
                }
            }






            //var result = wordsArr.GroupBy(x => x).OrderBy(x => x.Count()).Where(x => x.Count() > 1).Select(x => new { Word = x.Key, Frequency = x.Count() });

            //if (result.Count() < 3)
            //{
            //    result = null;
            //}

            //foreach (var item in result)
            //{
            //    Console.WriteLine("Слово: {0}\t Повторилось {1} раз.", item.Word, item.Frequency);
            //}
        }
    }
}
