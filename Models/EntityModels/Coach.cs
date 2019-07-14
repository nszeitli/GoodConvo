using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class Coach
    {
        public int Id { get; set; }
        public String GUID { get; set; }
        public String Name { get; set; }
        public String Author { get; set; }
        public bool IsPrivate { get; set; }


        public SortedList<int, Question> QuestionList { get; set; }

        public Coach()
        {
            QuestionList = new SortedList<int, Question>();
        }
    }
}
