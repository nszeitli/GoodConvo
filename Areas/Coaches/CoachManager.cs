using GoodConvo.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Areas.Coaches
{
    public class CoachManager
    {
        public Dictionary<string, CoachHelper> CoachList { get; set; }

        public CoachManager()
        {
            CoachList = new Dictionary<string, CoachHelper>();
            CoachHelper jack = new CoachHelper();
            jack.Name = "Jack";
            jack.ClassName = "coach";
            jack.QuestionList.Add(
                new Question
                {
                    Id = 0,
                    Index = 0,
                    Type = QuestionType.Text,
                    QuestionText = "How was waking up this morning?"
                }
            );

            jack.QuestionList.Add(
                new Question
                {
                    Id = 1,
                    Index = 1,
                    Type = QuestionType.Numerical,
                    QuestionText = "What time did you start effectively working?"
                }
            );

            jack.QuestionList.Add(
                new Question
                {
                    Id = 2,
                    Index = 2,
                    Type = QuestionType.Numerical,
                    QuestionText = "How many hours of work did you get done this morning?"
                }
            );

            jack.QuestionList.Add(
                new Question
                {
                    Id = 3,
                    Index = 3,
                    Type = QuestionType.LongText,
                    QuestionText = "What is the biggest challenge to your morning routine?"
                }
            );

            jack.QuestionList.Add(
                new Question
                {
                    Id = 4,
                    Index = 4,
                    Type = QuestionType.LongText,
                    QuestionText = "What should you work on in the short term?"
                }
            );

            CoachList.Add("jack", jack);
        }
    }
}
