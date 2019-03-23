using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChatBotApi.DAL
{
    public class ChatBotDbContext : DbContext
    {
        public ChatBotDbContext() : base("name = ChatBotdbContext")
        { }

            public DbSet<UnansweredQuestion> UnansweredQuestions { get; set; }

    }
}