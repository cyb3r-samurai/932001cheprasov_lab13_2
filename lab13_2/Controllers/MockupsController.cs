using Microsoft.AspNetCore.Mvc;
using lab13_2.Utilities;
using lab13_2.Models;

namespace lab13_2.Controllers
{
    public class MockupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private int QuestionCount
        {
            get
            {
                return HttpContext.Session.Get<int>(nameof(QuestionCount));
            }
        }
        private QuestModel NextQuestion
        {
            get
            {
                var question = QuestModel.randQust;

                var count = QuestionCount;
                HttpContext.Session.Set($"Question{count}", question);
                count += 1;
                HttpContext.Session.Set(nameof(QuestionCount), count);
                return question;
            }
        }

        private QuestModel LastQuestion
        {
            get
            {
                var count = QuestionCount - 1;
                return HttpContext.Session.Get<QuestModel>($"Question{count}");
            }
        }

        private void SaveAnswer(Nullable<int> answer)
        {
            var lastQuestion = LastQuestion;
            lastQuestion.UserAnswer = answer;
            HttpContext.Session.Set($"Question{QuestionCount - 1}", lastQuestion);
        }


        private QuestListModel Result
        {
            get
            {
                var result = new QuestListModel { quests = new() };
                for (var i = 0; i < QuestionCount; i++)
                {
                    var question = HttpContext.Session.Get<QuestModel>($"Question{i}");
                    result.quests.Add(question);
                }

                return result;
            }
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            var question = QuestionCount switch
            {
                0 => NextQuestion, _ => LastQuestion
            };
            ViewData["Question"] = question.output();
            return View();
        }
        [HttpPost]
        public IActionResult Quiz(AnswerModel answerModel, string action)
        {
            if (ModelState.IsValid)
            {
                SaveAnswer(answerModel.Answer);
                if (action == "Finish")
                {
                    return RedirectToAction("QuizResult");
                }
                else
                {
                    ViewData["Question"] = NextQuestion;
                    return RedirectToAction("Quiz");
                }
            }
            else
            {
                ViewData["Question"] = LastQuestion;
                return View();
            }
        }
        public IActionResult QuizResult()
        {
            var result = Result;

            HttpContext.Session.Clear();

            return View(result);
        }
    }

}

