using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class Conversation
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public String Document { get; set; }

        public bool inProgress { get; set; }
        public string SessionTag { get; set; }
        public virtual Coach Coach { get; set; }
        public virtual UserData UserData { get; set; }

        public virtual List<Question> QuestionsAsked { get; set; }
        public virtual List<Response> ResponseList { get; set; }
    }
}
