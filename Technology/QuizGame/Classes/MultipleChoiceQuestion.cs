using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Classes {
    public class MultipleChoiceQuestion : Question {
        public Dictionary<string, string> ChoiceList { get; }
        public string CorrectAnswer { get; }
        public MultipleChoiceQuestion(string questionText, Dictionary<string, string> choiceList, string correctAnswer, double scoreWeight) : base(questionText, scoreWeight) {
            if (choiceList.ContainsKey(correctAnswer)) {
                correctAnswer = correctAnswer.Trim().ToLower();
                CorrectAnswer = correctAnswer;
            }
            else {
                throw new ArgumentException("Inserted question has no answer. Add an answer and try again.");
            }

            if (choiceList.Count == 0) {
                throw new ArgumentException("Choice List should have at least 1 option.");
            }
            ChoiceList = choiceList;
        }

        public bool Evaluate(string userAnswer) {
            if (String.IsNullOrWhiteSpace(userAnswer)) {
                throw new ArgumentException($"Malformed userAnswer. Provided: {userAnswer}");
            }
            userAnswer = userAnswer.Trim().ToLower();
            return userAnswer.Equals(CorrectAnswer);
        }
        public string GetFormattedListOfChoices() {
            string answers = string.Join("\n", this.ChoiceList.Select(x => $"{x.Key} : {x.Value}"));
            return answers;
        }
    }
}
