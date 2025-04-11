using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Classes {
    public abstract class Question {
        public int NextId { get; set; } = 1;
        public int Id { get; set; }
        public double ScoreWeight { get; set; }
        public string? QuestionText { get; }

        public Question(string questionText, double scoreWeight) {
            if (questionText == null) {
                throw new ArgumentNullException("Question Text is null.", nameof(questionText));
            }
            if (string.IsNullOrWhiteSpace(questionText)) {
                throw new ArgumentException("Question Text should not be empty", nameof(questionText));
            }
            this.QuestionText = questionText;
            Id = NextId;
            NextId++;
            ScoreWeight = scoreWeight;
        }

    }
}
