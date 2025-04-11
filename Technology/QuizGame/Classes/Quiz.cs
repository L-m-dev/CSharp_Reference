using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Classes {

    public class Quiz {
        public enum ValidOptions {
            a, b, c, d, e,
        }
        public List<Question> QuestionList { get; set; }
        public int CurrentCorrectQuestions { get; set; }
        public double CurrentQuizGrade { get; set; } = 0;
        public int QuestionQuantity { get; set; } = 0;
        public double TotalQuizWeight { get; set; } = 0;
        public double ResultingQuizGrade { get; set; } = 0;
        ValidOptions validOption { get; set; }

        public Quiz() {
            this.QuestionList = new List<Question>();

            //default questions.
            string questionText = "Constitui crime inafiançável, imprescritível e insuscetível de graça ou anistia a prática da tortura, o tráfico\r\nilícito de entorpecentes, de animais silvestres, de minerais preciosos, de madeiras nobres e de material\r\ngenético, o terrorismo e o bioterrorismo, os crimes hediondos, a ação de grupos armados, civis ou\r\nmilitares, contra a ordem constitucional e o Estado democrático, a prática do racismo, do especismo e do\r\necocídio.";
            TrueFalseQuestion question1 = new(questionText, false, 3);
            QuestionList.Add(question1);

            questionText = "Direitos fundamentais como a proteção à maternidade e à infância são direitos sociais tratados como\r\nmatérias irrevogáveis na CF, conhecidas como cláusulas pétreas, não podendo ser alvo de diminuição ou\r\nrevogação por emenda constitucional.";
            TrueFalseQuestion question2 = new(questionText, true, 3);
            QuestionList.Add(question2);

            questionText = "Sobre as possibilidades de interferência estatal no direito fundamental à\r\nliberdade de associação, assinale a opção correta.";
            Dictionary<string, string> choiceList = new();
            choiceList.Add("a", "Cabe ao Poder Executivo determinar a dissolução compulsória de associação que tenha por objetivo a\r\npromoção de fins ilícitos.");
            choiceList.Add("b", "A produção dos efeitos da decisão judicial que determina a dissolução compulsória de associação depende\r\ndo seu trânsito em julgado.");
            choiceList.Add("c", "A legitimidade da associação para a representação de seus filiados restringe-se ao âmbito judicial.");
            choiceList.Add("d", "A atuação judicial de associação na condição de substituta processual depende de autorização dos\r\nassociados por meio de procuração.");
            choiceList.Add("e", "A exclusão de um associado de uma entidade religiosa por questões ideológicas está sujeita a revisão pelo\r\nEstado.");
            MultipleChoiceQuestion question3 = new(questionText, choiceList, "b", 5);
            QuestionList.Add(question3);

            questionText = " A República Federativa do Brasil, formada pela união indissolúvel dos Estados e Municípios e do Distrito Federal, constitui-se em Estado Democrático de Direito e tem como fundamentos:";
            choiceList = new();
            choiceList.Add("a", "a soberania");
            choiceList.Add("b", "a cidadania");
            choiceList.Add("c", "a dignidade da pessoa humana");
            choiceList.Add("d", "a autodeterminação dos povos");
            choiceList.Add("e", "a defesa da paz");
            HashSet<string> correctKeys = new HashSet<string>() { "a", "b", "c" };
            CheckBoxQuestion question4 = new(questionText, choiceList, correctKeys, 5);
            QuestionList.Add(question4);


           
           

            this.QuestionQuantity = this.QuestionList.Count;
            this.TotalQuizWeight = 16;
           
        }
        public Quiz(List<Question> questionlist) {
            this.QuestionList = questionlist;
        }

        public void AddQuestion(Question question) {
            QuestionList.Add(question);
            QuestionQuantity++;
            TotalQuizWeight = TotalQuizWeight + question.ScoreWeight;
        }
        public bool RemoveQuestion(int index) {
            try {
                QuestionList.RemoveAt(index);
                return true;
            }
            catch (ArgumentOutOfRangeException) {
                Console.WriteLine("Error. Invalid Index for Question removal.");
                return false;
            }
            catch (Exception ex) {
                Console.WriteLine("Error.", ex.ToString());
                return false;
                throw;
            }

        }

        public bool ResetQuiz() {
            try {
                this.QuestionList.Clear();
                this.CurrentQuizGrade = 0;
                this.QuestionQuantity = 0;
                this.TotalQuizWeight = 0;
                this.ResultingQuizGrade = 0;
                return true;
            }
            catch {
                return false;
            }
        }



        public void ExecuteQuizProgram() {
            if (this.QuestionList.Count < 1) {
                throw new Exception("Unable to start quiz. Quiz has no registered questions");
            }
            bool inExecution = true;

            while (inExecution) {
                Console.WriteLine("Quiz 2025");
                Console.WriteLine("1. Start Quiz.");
                Console.WriteLine("2. Reset Quiz.");
                Console.WriteLine("3. Exit.");
                Console.WriteLine();

                int choice;
                while (!Int32.TryParse(Console.ReadLine(), out choice)) {
                    Console.WriteLine("Invalid choice. Try again.");
                }

                switch (choice) {
                    
                    case 1:
                        StartQuiz();
                        Console.WriteLine("-----------------------------------------");
                        break;

                    case 2:
                        try {
                            ResetQuiz();
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 3:
                        inExecution = false;
                        break;

                }

            }
        }

        public void StartQuiz() {
            if (this.QuestionList.Count < 1) {
                throw new ArgumentNullException("Question List has no Questions.");
            }

            try {
                this.QuestionList.ForEach(q =>
            {
                HandleQuestion(q);
                Console.WriteLine("-----------------------------------------");
            });
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            double resultGrade = CalculateQuizGrade();
            Console.WriteLine("Quiz Complete!");
            Console.WriteLine($"Correct Questions / Total: {CurrentCorrectQuestions}/{QuestionQuantity}");
            Console.WriteLine($"Grade: {resultGrade}/100");
            
        }

        public void HandleQuestion(Question question) {

            if (question.GetType() == typeof(TrueFalseQuestion)) {
                TrueFalseQuestion q = question as TrueFalseQuestion;
                PrintTrueFalseQuestion(q);
                Console.WriteLine("True or False? Write:");
                bool answer;
                while (!bool.TryParse(Console.ReadLine().ToLower().Trim(), out answer)) {
                    Console.WriteLine("Couldn't understand the answer, try again.");
                }
                //if returns true, add the questions weight to the quiz grading.
                if (q.Evalute(answer)) {
                    Console.WriteLine("Correct.");
                    Console.WriteLine("The answer is: " + q.CorrectAnswer);
                    this.CurrentQuizGrade += q.ScoreWeight;
                    CurrentCorrectQuestions++;
                }
                else {
                    Console.WriteLine("Not correct.");
                    Console.WriteLine("The answer is: " + q.CorrectAnswer);
                }
            }
            else if (question.GetType() == typeof(MultipleChoiceQuestion)) {
                MultipleChoiceQuestion q = question as MultipleChoiceQuestion;
                PrintMultipleChoiceQuestion(q);
                Console.WriteLine("Which option is the correct answer? A, B, C, D or E?");
                ValidOptions userAnswerOption;
                while (!ValidOptions.TryParse(Console.ReadLine().Trim().ToLower(), out userAnswerOption)) {
                    Console.WriteLine("Couldn't understand the answer, try again.");
                }
                if (q.Evaluate(userAnswerOption.ToString())) {
                    Console.WriteLine("Correct.");
                    Console.WriteLine("The answer is: " + q.CorrectAnswer);
                    this.CurrentQuizGrade += q.ScoreWeight;
                    CurrentCorrectQuestions++;
                }
                else {
                    Console.WriteLine("Not correct.");
                    Console.WriteLine("The answer is: " + q.CorrectAnswer);
                }
            }
            else if (question.GetType() == typeof(CheckBoxQuestion)) {
                CheckBoxQuestion q = question as CheckBoxQuestion;
                PrintCheckBoxQuestion(q);
                Console.WriteLine("Write the options that you think are correct. Example: \"D,E,A\"");
                string userAnswerInput = Console.ReadLine();
                HashSet<string> userAnswerSet = this.ParseCheckBoxInput(userAnswerInput);
                if (q.Evaluate(userAnswerSet)) {
                    Console.WriteLine("Correct.");
                    //Use ToString to print the correct answers.
                    Console.WriteLine("The answer is: " + q.ToString());
                    this.CurrentQuizGrade += q.ScoreWeight;
                    CurrentCorrectQuestions++;
                }
                else {
                    Console.WriteLine("Not correct.");
                    Console.WriteLine("The answer is: " + q.ToString());
 
                }


            }
        }
        public void PrintCheckBoxQuestion(CheckBoxQuestion question) {
            Console.WriteLine(question.QuestionText);
            foreach (var choice in question.OptionList) {
                Console.WriteLine($"{choice.Key} - { choice.Value}");
            }
        }
        public void PrintMultipleChoiceQuestion(MultipleChoiceQuestion question) {
            Console.WriteLine(question.QuestionText);
            foreach(var choice in question.ChoiceList) {
                Console.WriteLine($"{choice.Key} - {choice.Value}");
            }
        }
        public void PrintTrueFalseQuestion(TrueFalseQuestion question) {
            Console.WriteLine(question.QuestionText);
        }

        public HashSet<string> ParseCheckBoxInput(string input) {
            if (string.IsNullOrWhiteSpace(input) || input == null) {
                throw new ArgumentNullException("Empty Checkbox Input.");
            }
            string[] normalizedInput;
            HashSet<string> userSelectedAnswers;
            if (input.Length > 1 && input.IndexOf(',') > -1) {
                normalizedInput = input.Split(",");
                for (int i = 0; i < normalizedInput.Length; i++) {
                    normalizedInput[i] = normalizedInput[i].Trim().ToLower();
                }
                normalizedInput = normalizedInput.Where(x => x.Length == 1 && char.IsLetter(Char.Parse(x)) && !string.IsNullOrEmpty(x)).ToArray();
                userSelectedAnswers = new HashSet<string>(normalizedInput);
            }
            else {
                userSelectedAnswers = new HashSet<string>();
                userSelectedAnswers.Add(input);
            }
            return userSelectedAnswers;
        }

        public double CalculateQuizGrade() {
            double totalGrade = ((double)CurrentQuizGrade / TotalQuizWeight) * 100;
            return totalGrade;
        }
        

        
    }
}