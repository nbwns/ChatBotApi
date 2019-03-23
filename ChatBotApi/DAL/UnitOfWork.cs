using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBotApi.DAL
{
    public class UnitOfWork
    {
        private ChatBotDbContext _db = new ChatBotDbContext();

        private Repository<UnansweredQuestion> uqRepository;

        public Repository<UnansweredQuestion> UQRepository
        {
            get {

                uqRepository = new Repository<UnansweredQuestion>(_db);
                return uqRepository;
            }
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }





    }
}