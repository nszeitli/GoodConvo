using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class UserData
    {
        public String GUID { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String PassHash { get; set; }

        public SortedList<int, Coach> CoachList { get; set; }
        public SortedList<DateTime, Conversation> PreviousConversations { get; set; }

        public UserData()
        {
            CoachList = new SortedList<int, Coach>();
            PreviousConversations = new SortedList<DateTime, Conversation>();
        }
    }
}
