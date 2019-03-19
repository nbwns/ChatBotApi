using ChatBotApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatBotApi.Controllers
{
    public class QnAController : ApiController
    {
        private IQservices _qServices;

        public QnAController()
        {
            _qServices = new QnAServices();
        }
        // GET: api/QnA
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/QnA/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/QnA
        [Route("api/QnA/answer")]
        public async Task<string> Post(Question question)
        {
            var result = await _qServices.GetAnswerAsync(question);
            return result;
        }

        // PUT: api/QnA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QnA/5
        public void Delete(int id)
        {
        }
    }
}
