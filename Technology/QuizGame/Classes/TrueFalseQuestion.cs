using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Classes {
    public class TrueFalseQuestion:Question{
        public bool CorrectAnswer {  get; set; }
        
        public TrueFalseQuestion(string questionText, bool correctAnswer, double scoreWeight) : base(questionText, scoreWeight) {
            this.CorrectAnswer = correctAnswer; 
        }

        public override string? ToString() {
            return $"Question: {this.QuestionText} \n Correct Answer: {this.CorrectAnswer}";
        }

        public bool Evalute(bool userAnswer) {
            return userAnswer == this.CorrectAnswer;
        }
    }
}
