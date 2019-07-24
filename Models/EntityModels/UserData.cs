using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class UserData
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }

        public virtual List<Conversation> PreviousConversations { get; set; }

    }
}
