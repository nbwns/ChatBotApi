using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ChatBotApi.Models
{
    public class QnAServices : IQservices
    {
        HttpClient client = new HttpClient();

        public QnAServices()
        {
            Initialize();

        }

        public async Task<QnaResponse> GetAnswerAsync(Question question)
        {

            string uri = "https://qnabxlformation.azurewebsites.net/qnamaker/knowledgebases/65c4283a-e248-4ec9-909f-8dc25c216f7b/generateAnswer";
            HttpResponseMessage answer = await client.PostAsJsonAsync(uri, question);

            var response= await answer.Content.ReadAsAsync<QnaResponse>();

            return response;
            //var json = await answer.Content.ReadAsStringAsync();
            //var superjson =  await JsonConvert.DeserializeObject<Task<string>>(json);
            //return superjson;
        }

        private void Initialize()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://qnabxlformation.azurewebsites.net/qnamaker/knowledgebases/65c4283a-e248-4ec9-909f-8dc25c216f7b/generateAnswer");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("EndpointKey", "1e3eced3-ef47-4091-9139-2651a5bafa60");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}