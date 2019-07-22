using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.ViewModels
{
    public class ConversationVM
    {
        public List<GcItem> ItemList { get; set; }
        public String DateStr { get; set; }

        public String LongDateStr { get; set; }


    }
}
