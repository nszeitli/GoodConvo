using GoodConvo.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Areas.Coaches
{
    public class CoachHelper
    {
        public String Name { get; set; }
        public String ClassName { get; set; }
        public List<Question> QuestionList { get; set; }

        public Question GetNextQuestion(int index)
        {
            if(index > QuestionList.Count - 1)
            {
                return new Question
                {
                    Id = 99,
                    Index = 99,
                    QuestionText = "Thats all I have right now, come back tomorrow"
                };
            }
            return QuestionList[index];
        }
        public CoachHelper()
        {

            QuestionList = new List<Question>();
            
        }
    }
}