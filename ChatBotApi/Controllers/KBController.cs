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
    public class KBController : ApiController
    {

        private KbServices _kbService;

        public KBController()
        {
            _kbService = new KbServices();
        }

        // GET: api/KB
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/KB/5
        public string Get(int id)
        {
            return "value";
        }



        // POST: api/KB
        [Route("api/KBController/update")]
        public async Task Post(QnAPair qnAPair)
        {
            await _kbService.UpdateAsync(qnAPair.question, qnAPair.answer);
        }

        // PUT: api/KB/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/KB/5
        public void Delete(int id)
        {
        }


    }
}
