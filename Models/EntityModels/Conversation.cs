using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class Conversation
    {
        public String GUID { get; set; }
        public DateTime DateTime { get; set; }
        
        
        public SortedList<int, Response> RespondedQuestions { get; set; }
        public SortedList<int, Response> ResponseList { get; set; }

        public Conversation()
        {
            GUID = Guid.NewGuid().ToString();
            DateTime = DateTime.Now;
            RespondedQuestions = new SortedList<int, Response>();
            ResponseList = new SortedList<int, Response>();
        }
        public virtual Coach Coach { get; set; }
        public virtual UserData UserData { get; set; }
    }
}
