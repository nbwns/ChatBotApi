using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static ChatBotApi.Models.KbServices;
using System.Web.Script.Serialization;

namespace ChatBotApi.Models
{
    public class KbServices : IKbServices
    {

        HttpClient client = new HttpClient();
        Uri uri = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v4.0/knowledgebases/65c4283a-e248-4ec9-909f-8dc25c216f7b");
        const string key = "3fcd881406784f08be6e43c3ad35ce48";
         const string kb = "65c4283a-e248-4ec9-909f-8dc25c216f7b";
        
         //KBAddJson new_kb = new KBAddJson("how are you", "fine and you");
//        static string new_kb = @"
//{
//  'add': {
//    'qnaList': [
//      {
//        'id': 1,
//        'answer': 'Here is the answer',
//        'source': 'Editorial',
//        'questions': [
//          'This is question'
//        ],
//        'metadata': []
//      }
//    ],
//    'urls': []
//  },
//  'update' : {},
//  'delete': {}
//}";



        public KbServices()
        {
            Initialize();
        }

        public struct Response
        {
            public HttpResponseHeaders headers;
            public StatusResponse response;

            public Response(HttpResponseHeaders headers, StatusResponse response)
            {
                this.headers = headers;
                this.response = response;
            }
        }


        async Task<Response> Patch(Uri uri, KBAddJson new_kb)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("PATCH");
                request.RequestUri = uri;
                //request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Content = new StringContent(new JavaScriptSerializer().Serialize(new_kb), Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                return new Response(response.Headers, null);
            }
        }

        async Task<Response> Get(Uri uri)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = uri;
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsAsync<StatusResponse>();
                return new Response(response.Headers, responseBody);
            }
        }

        async Task<Response> PostUpdateKB(string kb, KBAddJson new_kb)
        {
            return await Patch(uri, new_kb);
        }

        async Task<Response> GetStatus(string operation)
        {          
            string statusUrl = $"https://westus.api.cognitive.microsoft.com/qnamaker/v4.0/{operation}";
            return await Get(new Uri(statusUrl));
        }

        async Task UpdateKBAsync(string kb, KBAddJson new_kb)
        {
            var response = await PostUpdateKB(kb, new_kb);
            var operation = response.headers.GetValues("Location").First();


            var done = false;
            while (true != done)
            {
                response = await GetStatus(operation);

                String state = response.response.operationState;
                if (state.CompareTo("Running") == 0 || state.CompareTo("NotStarted") == 0)
                {
                    //var wait = response.headers.GetValues("Retry-After").First();
                    //Console.WriteLine("Waiting " + wait + " seconds...");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Press any key to continue.");
                    done = true;
                }
            }

            //PUBLISH

            await publishRequestAsync();

            // END

        }

        private async Task publishRequestAsync()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "3fcd881406784f08be6e43c3ad35ce48");

            var uri = "https://westus.api.cognitive.microsoft.com/qnamaker/v4.0/knowledgebases/65c4283a-e248-4ec9-909f-8dc25c216f7b";

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
        }

        public async Task UpdateAsync(string question, string answer)
        {
            KBAddJson new_kb = new KBAddJson(question, answer);
            await UpdateKBAsync(kb, new_kb);
        }




        private void Initialize()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v4.0/knowledgebases/65c4283a-e248-4ec9-909f-8dc25c216f7b");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("EndpointKey", "1e3eced3-ef47-4091-9139-2651a5bafa60");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "3fcd881406784f08be6e43c3ad35ce48");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}