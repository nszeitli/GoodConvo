using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodConvo.Models;
using GoodConvo.Models.EntityModels;
using GoodConvo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodConvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {
        JournalContext _db;

        public SubmitController(JournalContext context)
        {
            _db = context;
        }

        // GET: api/Submit
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Submit/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Submit
        [HttpPost]
        public void Post([FromBody] List<GcItem> items)
        {
            if(items != null && items.Count > 2)
            {
                UserData user = new UserData
                {
                    Email = User.Identity.Name,
                    FirstName = User.Identity.Name
                };

                Conversation newConvo = new Conversation
                {
                    UserData = user,
                    DateTime = DateTime.Now
                };

                int iCoach = 0;
                int iUser = 0;
                string coachName = "";
                newConvo.QuestionsAsked = new List<Question>();
                newConvo.ResponseList = new List<Response>();

                foreach (GcItem item in items)
                {
                    if(item.compstyle != null && item.compstyle.Contains("coach"))
                    {
                        coachName = item.Author;
                        newConvo.QuestionsAsked.Add(new Question
                        {
                            Index = iCoach,
                            QuestionText = item.Content
                        });
                        iCoach++;
                    }
                    else
                    {
                        newConvo.ResponseList.Add(new Response
                        {
                            Index = iUser,
                            IsTextResponse = true,
                            TextResponse = item.Content
                        });
                        iUser++;
                    }
                }
                String doc = "<p>Journal entry at " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() +" by " + User.Identity.Name + "</p>";
                foreach (var item in newConvo.QuestionsAsked)
                {
                    doc = doc + "<p><b>" + "(" + (item.Index + 1) + ") " + item.QuestionText + "</b></p>";
                    string response = "-";
                    try
                    {
                        response = newConvo.ResponseList[item.Index].TextResponse;
                    }
                    catch (Exception) { }

                    doc = doc + "<p>" + response + "</p>";
                }
                newConvo.Document = doc;
                //TODO add coach ref
                coachName = coachName.Replace("Coach ", "");
                var coach = _db.Coaches.FirstOrDefault(i => i.Name == coachName);

                newConvo.Coach = coach;

                _db.Conversations.Add(newConvo);
                _db.SaveChanges();
            }
            
        }

        // PUT: api/Submit/5
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
