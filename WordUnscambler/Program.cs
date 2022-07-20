using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscambler.Workers;
using WordUnscambler.Data;

namespace WordUnscambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private const string wordListFileName = "wordlist.txt";

        static void Main(string[] args)
        {
            bool continueWordUnscramble = true;
            do
            {

                Console.WriteLine("Please enter option - F for File mand M for Maunal");
                var option = Console.ReadLine() ?? string.Empty;

                switch (option.ToUpper())
                {
                    case "F":
                        Console.Write("Enter scrambbled words file name: ");
                        ExecuteScrambledWordsInFileScenerio();
                        break;
                    case "M":
                        Console.Write("Enter scrambbled words manually: ");
                        ExecuteScrambledWordsManualEntryScenerio();
                        break;
                    default:
                        Console.Write("Option not recognized: ");
                        break;
                }

                var continueDecision = string.Empty;
                do
                {
                    Console.WriteLine("Do you want to continue Y/N?");
                    continueDecision = (Console.ReadLine() ?? string.Empty);

                } while
                    (!continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase) &&
                    !continueDecision.Equals("N", StringComparison.OrdinalIgnoreCase));

                continueWordUnscramble = continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase);

            } while (continueWordUnscramble);
        }

        private static void ExecuteScrambledWordsManualEntryScenerio()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenerio()
        {
            var filename = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(filename);
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList); 

            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine("Match found for {0}: {1}", matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine("No matches have been found.");
            }
        }
    }
}
