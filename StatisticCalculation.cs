using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    class StatisticCalculation
    {
        private static readonly char[] DelimiterChars = { ' ', ',', '.', ':', '\t' };   

        public static Dictionary<string, int> CountFrequencyWords(string text)
        {
            Dictionary<string,int> frequencyDictionary = new Dictionary<string, int>();
            string[] words = text.Split(DelimiterChars, StringSplitOptions.RemoveEmptyEntries);
            foreach (var downCaseWord in words.Select(word => word.ToLower()))
            {
                if (frequencyDictionary.ContainsKey(downCaseWord))
                {
                    frequencyDictionary[downCaseWord]++;
                    continue;
                }
                frequencyDictionary.Add(downCaseWord, 1);
            }
            return frequencyDictionary;
        }

        public static double MutualInformation(string text, string phrase)
        {
            phrase = phrase.ToLower();
            text = text.ToLower();
            string textWithoutPhrase = text.Replace(phrase, "");
            var wordsWithPhrase = text.Split(DelimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var wordsWithoutPhrase= textWithoutPhrase.Split(DelimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var phraseFrequency = (wordsWithPhrase.Length - wordsWithoutPhrase.Length) / 2;
            var freqDictionary = CountFrequencyWords(text);
            var words = phrase.Split(' ');
            int frequenceOfFirstWord = freqDictionary.FirstOrDefault(x => x.Key.Contains(words[0])).Value;
            int frequenceOfSecondWord = freqDictionary.FirstOrDefault(x => x.Key.Contains(words[1])).Value;

            return Math.Log((phraseFrequency * wordsWithPhrase.Length) / (frequenceOfFirstWord * frequenceOfSecondWord), 2);
        }

        public static double TScore(string text, string phrase)
        {
            phrase = phrase.ToLower();
            text = text.ToLower();
            string textWithoutPhrase = text.Replace(phrase, "");
            var wordsWithPhrase = text.Split(DelimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var wordsWithoutPhrase = textWithoutPhrase.Split(DelimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var phraseFrequency = (wordsWithPhrase.Length - wordsWithoutPhrase.Length) / 2;
            var freqDictionary = CountFrequencyWords(text);
            var words = phrase.Split(' ');
            int frequenceOfFirstWord = freqDictionary.FirstOrDefault(x => x.Key.Contains(words[0])).Value;
            int frequenceOfSecondWord = freqDictionary.FirstOrDefault(x => x.Key.Contains(words[1])).Value;
            int phraseOccurrencesCountInText = Regex.Matches(text, phrase).Count;
            return (phraseFrequency - (frequenceOfFirstWord*frequenceOfSecondWord / phraseOccurrencesCountInText)) /
                   phraseFrequency;
        }

    }
}
