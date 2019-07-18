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
    public class ResponseController : ControllerBase
    {
        // GET: api/Response

        // GET: api/Response/5
        [HttpGet(Name = "Get a response")]
        [Authorize]
        public GcItem Get([FromQuery] string value, [FromQuery] string coach, [FromQuery] int index)
        {
            //get coach
            CoachManager cm = new CoachManager();
            CoachHelper c = null;
            cm.CoachList.TryGetValue(coach, out c);
            if (c == null) { return new GcItem { Content = "Something got scrambled, refresh the browser", Author = coach, compstyle = "coach" }; }
            Question nextQ = c.GetNextQuestion(index);

            return new GcItem { Content = nextQ.QuestionText, Author = "Coach " + c.Name, compstyle = c.ClassName, Type = nextQ.Type.ToString() };
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
