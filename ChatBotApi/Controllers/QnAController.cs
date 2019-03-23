using ChatBotApi.DAL;
using ChatBotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ChatBotApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QnAController : ApiController
    {

        const string noAnswer = "No good match found in KB.";
        private IQservices _qServices;
        UnitOfWork unitOfWork = new UnitOfWork();

        public QnAController()
        {
            _qServices = new QnAServices();
        }

        // GET: api/QnA
        [Route("api/qna/getuq")]
        public List<UnansweredQuestion> Get()
        {
            var result = unitOfWork.UQRepository.GetAll().ToList();
            return result;
        }

        // GET: api/QnA/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/QnA
        [Route("api/QnA/answer")]
        public async Task<QnaResponse> Post(Question question)
        {
            var result = await _qServices.GetAnswerAsync(question);
            //logique pour envoyer les questions sans réponses dans la db
            var text = result.answers[0];
            if(text.answer == noAnswer)
            {
                //ajoutez à la db
                unitOfWork.UQRepository.Insert(new UnansweredQuestion(question.question));
                unitOfWork.Save();
                //puis renvoyer tout
                return result;
            }
            else
            return result;
        }

        // PUT: api/QnA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QnA/5
        public void Delete(int id)
        {
            var entityToDelete = unitOfWork.UQRepository.GetById(id);
            unitOfWork.UQRepository.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}
