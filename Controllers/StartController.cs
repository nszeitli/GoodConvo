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
    public class StartController : ControllerBase
    {
        private JournalContext _context;
        public StartController(JournalContext context)
        {
            this._context = context;
        }
        
        // GET: api/Start
        [HttpGet]
        [Authorize]
        public IEnumerable<GcItem> Get([FromQuery] string coach)
        {
            //get coach
            List<GcItem> output = new List<GcItem>();
            var cq = _context.Coaches.Include(j => j.QuestionList).Where(i => i.Name.ToLower() == coach.ToLower()).ToList();
            if (cq == null) { output.Add(new GcItem { Content = "Something got scrambled, refresh the browser", Author = coach, compstyle = "coach" }); return output; }
            Question nextQ = cq[0].QuestionList[0];
            
            output.Add(new GcItem { Content = nextQ.QuestionText, Author = "Coach " + cq[0].Name, compstyle = "coach", Type = nextQ.Type.ToString() });
            return output;
        }


        // POST: api/Start
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Start/5
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
