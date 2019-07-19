using GoodConvo.Models;
using GoodConvo.Models.EntityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GoodConvo.Data
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

            context.Database.EnsureCreated();

            //Delete users
            /*var me = await userManager.FindByNameAsync("nszeitli@gmail.com");
            await userManager.DeleteAsync(me);

            var shaira = await userManager.FindByNameAsync("azcunasmb@gmail.com");
            await userManager.DeleteAsync(shaira);*/

            //Create roles
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "admin" });
            }
            if (await roleManager.FindByNameAsync("free") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "free" });
            }
            if (await roleManager.FindByNameAsync("paid") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "paid" });
            }

            //Create roles
            if (await userManager.FindByNameAsync("admin@vacmopscrub.com") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Nathan",
                    UserName = "admin@vacmopscrub.com",
                    Email = "admin@vacmopscrub.com"
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "!Akarma464");
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }
            var me = await userManager.FindByNameAsync("nszeitli@gmail.com");
            if (me != null)
            {
                await userManager.AddToRoleAsync(me, "admin");
            }
        }


        public static async Task SeedCoaches(JournalContext context)
        {

            //Seed coaches
            List<Question> shairaQuestions = new List<Question>();
            shairaQuestions.Add(new Question
            {
                Index = 0,
                QuestionText = "How was your day today?",
                Type = QuestionType.Text
            });
            shairaQuestions.Add(new Question
            {
                Index = 1,
                QuestionText = "Were you a good boy/girl?",
                Type = QuestionType.Text
            });
            shairaQuestions.Add(new Question
            {
                Index = 2,
                QuestionText = "What did you get done today?",
                Type = QuestionType.LongText
            });
            shairaQuestions.Add(new Question
            {
                Index = 3,
                QuestionText = "How many naughty things did you eat?",
                Type = QuestionType.Numerical
            });
            shairaQuestions.Add(new Question
            {
                Index = 4,
                QuestionText = "What will be tomorrows most important task?",
                Type = QuestionType.Text
            });
            shairaQuestions.Add(new Question
            {
                Index = 5,
                QuestionText = "What are you most grateful for?",
                Type = QuestionType.LongText
            });

            Coach newCoach = new Coach
            {
                Author = "Nathan",
                Name = "Shaira",
                QuestionList = shairaQuestions
            };

            context.Coaches.Add(newCoach);


            List<Question> nathanQuestions = new List<Question>();
            nathanQuestions.Add(new Question
            {
                Index = 0,
                QuestionText = "What was the highlight of today?",
                Type = QuestionType.Text
            });
            nathanQuestions.Add(new Question
            {
                Index = 1,
                QuestionText = "What things do you feel great about?",
                Type = QuestionType.LongText
            });
            nathanQuestions.Add(new Question
            {
                Index = 2,
                QuestionText = "What do you feel not-so-great about?",
                Type = QuestionType.LongText
            });
            nathanQuestions.Add(new Question
            {
                Index = 3,
                QuestionText = "What did you learn today?",
                Type = QuestionType.LongText
            });
            nathanQuestions.Add(new Question
            {
                Index = 4,
                QuestionText = "What will be tomorrows biggest challenge?",
                Type = QuestionType.Text
            });

            Coach newCoachN = new Coach
            {
                Author = "Nathan",
                Name = "Nathan",
                QuestionList = nathanQuestions
            };

            context.Coaches.Add(newCoachN);

            List<Question> jackQuestions = new List<Question>();
            jackQuestions.Add(new Question
            {
                Index = 0,
                QuestionText = "How was waking up this morning?",
                Type = QuestionType.LongText
            });
            jackQuestions.Add(new Question
            {
                Index = 1,
                QuestionText = "What time did you start effectively working?",
                Type = QuestionType.Text
            });
            jackQuestions.Add(new Question
            {
                Index = 2,
                QuestionText = "How many hours of work did you get done this morning?",
                Type = QuestionType.Numerical
            });
            jackQuestions.Add(new Question
            {
                Index = 3,
                QuestionText = "What is the biggest challenge to your morning routine?",
                Type = QuestionType.LongText
            });
            jackQuestions.Add(new Question
            {
                Index = 4,
                QuestionText = "How can this be improved?",
                Type = QuestionType.LongText
            });

            Coach newCoachJ = new Coach
            {
                Author = "Nathan",
                Name = "Jack",
                QuestionList = jackQuestions
            };

            context.Coaches.Add(newCoachJ);

            context.SaveChanges();

        }



    }
}
