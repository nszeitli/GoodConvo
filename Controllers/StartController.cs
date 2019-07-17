using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodConvo.Areas.Coaches;
using GoodConvo.Models.EntityModels;
using GoodConvo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodConvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        // GET: api/Start
        [HttpGet]
        [Authorize]
        public IEnumerable<GcItem> Get([FromQuery] string coach)
        {
            //get coach
            List<GcItem> output = new List<GcItem>();
            CoachManager cm = new CoachManager();
            CoachHelper c = null;
            cm.CoachList.TryGetValue(coach, out c);
            if (c == null) { output.Add(new GcItem { Content = "Something got scrambled, refresh the browser", Author = coach, compstyle = "coach" }); return output; }
            Question nextQ = c.GetNextQuestion(0);
            
            output.Add(new GcItem { Content = nextQ.QuestionText, Author = "Coach " + c.Name, compstyle = c.ClassName });
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
