using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace rating.Data
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }

        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string Correct { get; set; }

        public static List<Questions> GetAllQuestions()
        {
            var question_file = System.IO.File.ReadAllText(Constants.QuestionPath);

            var jsonData = JsonConvert.DeserializeObject<List<Questions>>(question_file);

            jsonData.Shuffle();
            return jsonData;
        }

    }

    public class QuestionAndAnswer
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
    }

    public class UserAnswers
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }

        public string TimeLeft { get; set; }
        public bool isDone { get; set; }
        public List<QuestionAndAnswer> QuestionAndAnswers { get; set; }
    }
}
