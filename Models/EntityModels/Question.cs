using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class Question
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public String QuestionText { get; set; }

        public Question()
        {
        }
    }
}
