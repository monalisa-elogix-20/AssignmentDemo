using AssignmentGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AssignmentGit.BusinessRule
{
    public class OccourencesCountRule
    {
        public Dictionary<string, int> GetAllWordsCount(List<string> data)
        {
            var tasks = new List<Task<List<OccurenceObjectModel>>>();
            data?.ForEach(x =>
            {
                tasks.Add(Task.Run(() => OccurencesCount(x)));
            });

            var primaryResult = Task.WhenAll(tasks).Result;
            Dictionary<string, int> finalResult = new Dictionary<string, int>();
            if (primaryResult?.Any() ?? false)
            {
                foreach (List<OccurenceObjectModel> item in primaryResult)
                {
                    item.ForEach(wordObj =>
                    {
                        if (finalResult.ContainsKey(wordObj.Word))
                        {
                            finalResult[wordObj.Word] = finalResult[wordObj.Word] + wordObj.Value;
                        }
                        else
                        {
                            finalResult.Add(wordObj.Word, wordObj.Value);
                        }
                    });
                }
            }
            return finalResult;
        }
        private List<OccurenceObjectModel> OccurencesCount(string currentLine)
        {
            var rslt = new List<OccurenceObjectModel>();
            var words = GetWords(currentLine);
            var wordCount = from word in words group word by word into g select new { g.Key, Count = g.Count() };
            wordCount?.ToList()?.ForEach(x =>
            {
                rslt.Add(new OccurenceObjectModel() { Word = x.Key, Value = x.Count });
            });
            return rslt;
        }

        static string[] GetWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select TrimSuffix(m.Value);

            return words.ToArray();
        }

        static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }
    }
}