using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodConvo.Areas.Coaches;
using GoodConvo.Models;
using GoodConvo.Models.EntityModels;
using GoodConvo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodConvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private JournalContext _context;
        public ResponseController(JournalContext context)
        {
            this._context = context;
        }

        // GET: api/Response/5
        [HttpGet(Name = "Get a response")]
        [Authorize]
        public GcItem Get([FromQuery] string value, [FromQuery] string coach, [FromQuery] int index)
        {
            //get coach
            GcItem output = new GcItem();
            var c = _context.Coaches.Include(j => j.QuestionList).Where(i => i.Name.ToLower() == coach.ToLower()).ToList()[0];
            if (c == null) { output = new GcItem { Content = "Something got scrambled, refresh the browser", Author = coach, compstyle = "coach" }; return output; }
            if (index >= c.QuestionList.Count ) { return new GcItem { Content = "Thats all I have today, come back tomorrow", Author = "Coach " + c.Name, compstyle = "coach", Type = "Final" }; }
            else
            {
                Question nextQ = c.QuestionList[index];
                return new GcItem { Content = nextQ.QuestionText, Author = "Coach " + c.Name, compstyle = "coach", Type = nextQ.Type.ToString() };
            }

        }

        // POST: api/Response
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Response/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
