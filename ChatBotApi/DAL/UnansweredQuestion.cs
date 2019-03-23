using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBotApi.DAL
{
    public class UnansweredQuestion
    {

        public UnansweredQuestion()
        {

        }
        public UnansweredQuestion(string question)
        {
            this.QuestionText = question;
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }

    }
}