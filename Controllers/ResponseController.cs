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

            //update convo
            Conversation convo = null;
            Coach c = null;
            if (HttpContext.Session.IsAvailable && HttpContext.Session.Keys.Contains("convo-" + coach))
            {
                string convoId = HttpContext.Session.GetString("convo-" + coach);
                if (convoId != null) {
                    convo = _context.Conversations
                        .Include(i => i.QuestionsAsked)
                        .Include(i => i.ResponseList)
                        .Include(i => i.Coach)
                        .ThenInclude(coachItem => coachItem.QuestionList)

                        .OrderByDescending(e => e.DateTime)
                        .Where(i => i.SessionTag == convoId).SingleOrDefault();
                    convo.ResponseList.Add(new Response
                    {
                        Index = index,
                        IsTextResponse = true,
                        TextResponse = value
                    });

                    c = convo.Coach;
                }
                
            }
            else
            {
                //no convo...request client to send data
                return new GcItem { Content = "Thats all I have today, ive safely saved your journal entries.", Author = "Coach " + c.Name, compstyle = "coach", Type = "Final" };
            }


            if (index >= c.QuestionList.Count )
            {
                //submit
                convo.QuestionsAsked.Add(new Question
                {
                    Index = index +1,
                    QuestionText = "Thats all I have today, come back tomorrow",
                    Type = QuestionType.Text
                });
                convo.inProgress = false;
                _context.SaveChangesAsync();

                return new GcItem { Content = "Thats all I have today, ive safely saved your journal entries.", Author = "Coach " + c.Name, compstyle = "coach", Type = "Final" };
            }
            else
            {
                Question nextQ = c.QuestionList[index];

                convo.QuestionsAsked.Add(new Question
                {
                    Index = index + 1,
                    QuestionText = nextQ.QuestionText,
                    Type = nextQ.Type
                });
                _context.SaveChangesAsync();

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
