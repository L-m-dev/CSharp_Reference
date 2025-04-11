using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Classes {
    public class CheckBoxQuestion : Question {
        public Dictionary<string, string> OptionList { get; set; }

        private HashSet<string> CorrectAnswerKeys { get; set; }

        public CheckBoxQuestion(string questionText, Dictionary<string, string> optionList,
            HashSet<string> correctAnswerKeys, double scoreWeight) : base(questionText, scoreWeight) {
            if (correctAnswerKeys == null || correctAnswerKeys.Count < 1) {
                throw new ArgumentNullException("Question should have a valid answer set");
            }

            bool valid = this.ValidateOptionList(optionList);
            if (valid) {
                this.OptionList = optionList;
            }
            else {
                throw new ArgumentException("Couldn't validate Option List.");
            }

            var normalizedAnswerSet = new HashSet<string>(correctAnswerKeys.Select(x => x.Trim().ToLower()));
            if (ValidateCorrectAnswerKeysByHashSet(normalizedAnswerSet)) {
                this.CorrectAnswerKeys = normalizedAnswerSet;
            }
            else {
                throw new ArgumentException("Couldn't validate Answer Keys.");
            }

        }

        public override string? ToString() {
            //should print the Values from the hash table.
            string correctAnswers = "";
            if (this.OptionList.Count > 0) {
                foreach (var item in OptionList) {
                    if (CorrectAnswerKeys.Contains(item.Key)) {
                        correctAnswers += $"{item.Key} - {item.Value} \n";
                    }
                }
            }
            return $"Question: {this.QuestionText}\n {correctAnswers}";
        }

        public bool Evaluate(HashSet<string> userAnswer) {
            return userAnswer.SetEquals(this.CorrectAnswerKeys);
        }

        public HashSet<string> CreateCorrectAnswerKeysByString(string input) {
            if (string.IsNullOrWhiteSpace(input) || input == null) {
                throw new ArgumentNullException("Empty Input.");
            }
            string[] normalizedInput;
            HashSet<string> correctAnswerKeys;
            if (input.Length > 1 && input.IndexOf(',') > -1) {
                normalizedInput = input.Split(",");
                for (int i = 0; i < normalizedInput.Length; i++) {
                    normalizedInput[i] = normalizedInput[i].Trim().ToLower();
                }
                normalizedInput = normalizedInput.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                correctAnswerKeys = new HashSet<string>(normalizedInput);
            }
            else {
                correctAnswerKeys = new HashSet<string>();
                correctAnswerKeys.Add(input);
            }
            return correctAnswerKeys;
        }

        public bool ValidateCorrectAnswerKeysByHashSet(HashSet<string> input) {
            if (input.Count < 1) {
                return false;
            }
            foreach (var item in input) {
                if (!this.OptionList.ContainsKey(item)) {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(item)) {
                    return false;
                }
                else
                if (item.Length > 1) {
                    return false;
                }
                else
                if (!Char.IsLetterOrDigit(item[0])) {
                    return false;
                }
                return true;
            }
            throw new ArgumentException("Couldn't validate Answer Keys");

        }

        public bool ValidateOptionList(Dictionary<string, string> optionList) {
            if (optionList == null || optionList.Count == 0) {
                return false;
            }
            foreach (var item in optionList) {
                if (item.Key.Length != 1) {
                    return false;
                }
                if (!Char.IsLetterOrDigit(item.Key[0])) {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(item.Key)) {
                    return false;
                }
            }
            return true;
        }
    }
}
