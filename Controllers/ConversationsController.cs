using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodConvo.Models;
using GoodConvo.Models.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GoodConvo.Models.ViewModels;

namespace GoodConvo.Controllers
{
    [Authorize]
    public class ConversationsController : Controller
    {
        private JournalContext _context;
        private UserManager<ApplicationUser> _userManager;
        public ConversationsController(JournalContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }


        // GET: Conversations
        public async Task<IActionResult> Index()
        {
            var list = await _context.Conversations.
                Include(i => i.UserData).
                Include(i => i.QuestionsAsked).
                Include(i => i.ResponseList).
                ToListAsync();

            list = list.Where(i => i.UserData.Email == User.Identity.Name).ToList();

            return View(list);
        }

        public async Task<ConversationVM> GetLastConvo([FromQuery] String coach)
        {
            if (coach == null)
            {
                return new ConversationVM();
            }

            var user = await _userManager.GetUserAsync(User);

            var conversation =  _context.Conversations
                .Include(i => i.QuestionsAsked)
                .Include(i => i.ResponseList)
                .Include(i => i.Coach)
                .OrderByDescending(e => e.DateTime)
                .FirstOrDefault(m => m.UserData.Email == user.Email && m.inProgress == false && m.Coach.Name.ToLower() == coach.ToLower());
            if (conversation == null)
            {
                return new ConversationVM();
            }
            conversation.QuestionsAsked = conversation.QuestionsAsked.OrderBy(i => i.Index).ToList();
            conversation.ResponseList = conversation.ResponseList.OrderBy(i => i.Index).ToList();
            string longStr = "Journal entry at " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " by " + user.FirstName;

            //build item list
            List<GcItem> itemList = new List<GcItem>();
            int q = 0;
            foreach (var item in conversation.QuestionsAsked)
            {
                itemList.Add(new GcItem {
                    Content = item.QuestionText,
                    Author = "Coach " + conversation.Coach.Name,
                    compstyle = "coach",
                    Type = ""
                });
                if(q < conversation.ResponseList.Count)
                {
                    var response = conversation.ResponseList[q];
                    itemList.Add(new GcItem
                    {
                        Content = response.IsTextResponse ? response.TextResponse : response.NumResponse.ToString(),
                        Author = user.FirstName,
                        compstyle = "",
                        Type = ""
                    });
                    
                }
                q++;
            }

            var vm = new ConversationVM
            {
                DateStr = conversation.DateTime.ToShortDateString(),
                ItemList = itemList,
                LongDateStr = longStr
            };

            return vm;
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }



        // GET: Conversations/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Conversations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,Document")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,Document")] Conversation conversation)
        {
            if (id != conversation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.Id == id);
        }
    }
}
