using QuizGame.Classes;
using QuizGameTests;
using static QuizGame.Classes.Quiz;
namespace QuizGameTests {
    [TestClass]
    public class UnitTest1 {
        TrueFalseQuestion trueFalseQuestion1;
        MultipleChoiceQuestion multipleChoiceQuestion1;
        CheckBoxQuestion checkBoxQuestion1;
        Quiz quiz;

        [TestInitialize]
        public void Init() {
            quiz = new Quiz();
            List<Question> list = new List<Question>();

            trueFalseQuestion1 = new TrueFalseQuestion("Brasília é a capital do Brasil.", true, 2);

            Dictionary<string, string> choices = new Dictionary<string, string>();
            choices.Add("a", "Cristiano Ronaldo");
            choices.Add("b", "Vini Jr.");
            choices.Add("c", "Mbappe");
            choices.Add("d", "Roberto");
            choices.Add("e", "James");
            multipleChoiceQuestion1 = new MultipleChoiceQuestion("Who is the new Real Madrid player?", choices, "c", 5);

            Dictionary<string, string> choicesForCheckbox = new Dictionary<string, string>();
            choicesForCheckbox.Add("a", "RS");
            choicesForCheckbox.Add("b", "Canada");
            choicesForCheckbox.Add("c", "SP");
            choicesForCheckbox.Add("d", "United States");
            choicesForCheckbox.Add("e", "Zambia");
            HashSet<string> userAnswerSet = new HashSet<string>() { "a", "c" };
            checkBoxQuestion1 = new CheckBoxQuestion("List all of Brazilian states:", choicesForCheckbox, userAnswerSet, 5);

        }
        [TestMethod]
        public void TestMethod1() {
        }

        [TestMethod]
        public void TestHandleQuestion_TrueFalse_EvaluateCorrectAnswer() {
            bool userAnswer = trueFalseQuestion1.Evalute(true);
            Assert.IsTrue(userAnswer);

            userAnswer = trueFalseQuestion1.Evalute(false);
            Assert.IsFalse(userAnswer);
        }
        [TestMethod]
        public void TestHandleQuestion_MultipleChoice_EvaluateCorrectAnswer() {
            ValidOptions option = ValidOptions.c;
            bool userAnswer = multipleChoiceQuestion1.Evaluate(option.ToString());
            Assert.IsTrue(userAnswer);
        }

        [TestMethod]
        public void TestHandleQuestion_MultipleChoice_EvaluateWrongAnswer() {
            ValidOptions option = ValidOptions.e;
            bool userAnswer = multipleChoiceQuestion1.Evaluate(option.ToString());
            Assert.IsFalse(userAnswer);
        }
        [TestMethod]
        public void TestHandleQuestion_CheckBox_EvaluateCorrectAnswer_ShouldThrowExceptionWhenNullInput() {
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void TestParseCheckBoxInput_EmptyNullInput_ShouldThrowException(string input) {
            Assert.ThrowsException<ArgumentNullException>(() => quiz.ParseCheckBoxInput(input));
        }

        [TestMethod]
        public void TestHandleQuestion_CheckBox_EvaluateCorrectAnswer() {
            HashSet<string> userAnswerSet = new HashSet<string>() { "a", "c" };
            bool isCorrect = checkBoxQuestion1.Evaluate(userAnswerSet);
            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestParseCheckBoxInput_ShouldHaveCorrectSet() {
            string input = "A, B,D,";
            var userAnswerSet = quiz.ParseCheckBoxInput(input);
            HashSet<string> set = new HashSet<string>() { "a", "b", "d" };
            bool equalityResult = set.SetEquals(userAnswerSet);
            Assert.IsTrue(equalityResult);
        }

        [TestMethod]
        public void TestQuizCreation_ShouldInitFieldsProperly() {
            Quiz q = new Quiz();
            Assert.AreEqual(0, q.QuestionQuantity);
            Assert.AreEqual(0, q.CurrentQuizGrade);
            Assert.AreEqual(0, q.TotalQuizWeight);
            Assert.AreEqual(0, q.ResultingQuizGrade);
        }

        [TestMethod]
        public void TestAddQuestionToQuiz() {
            double totalWeight = 0;
            totalWeight = trueFalseQuestion1.ScoreWeight + checkBoxQuestion1.ScoreWeight + multipleChoiceQuestion1.ScoreWeight;
            int qtyAdded = 3;

            quiz.AddQuestion(trueFalseQuestion1);
            quiz.AddQuestion(checkBoxQuestion1);
            quiz.AddQuestion(multipleChoiceQuestion1);

            Assert.AreEqual(qtyAdded, quiz.QuestionQuantity);
            Assert.AreEqual(totalWeight, quiz.TotalQuizWeight);
            Assert.AreEqual(3, quiz.QuestionList.Count);
        }

        [TestMethod]
        public void TestResetQuiz() {
            {
                this.quiz.ResetQuiz();
                Assert.AreEqual(0, quiz.QuestionQuantity);
                Assert.AreEqual(0, quiz.CurrentQuizGrade);
                Assert.AreEqual(0, quiz.TotalQuizWeight);
                Assert.AreEqual(0, quiz.ResultingQuizGrade);
            }
        }
    }
}
