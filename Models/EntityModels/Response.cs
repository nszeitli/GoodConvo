using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models.EntityModels
{
    public class Response
    {
            [Key]
            public String GUID { get; set; }
            public int Index { get; set; }
            public bool IsTextResponse { get; set; }
            public String TextResponse { get; set; }
            public int NumResponse { get; set; }

            public Response()
            {
                GUID = Guid.NewGuid().ToString();
            }
    }
}
