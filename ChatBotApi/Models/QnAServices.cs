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

        public async Task<string> GetAnswerAsync(Question question)
        {

            string uri = "https://qnabxlformation.azurewebsites.net/qnamaker/knowledgebases/5b10b5b9-48cf-4cbe-a00e-27c6bb332588/generateAnswer";
            HttpResponseMessage answer = await client.PostAsJsonAsync(uri, question);
            return await answer.Content.ReadAsStringAsync();
        }

        private void Initialize()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://qnabxlformation.azurewebsites.net/qnamaker/knowledgebases/5b10b5b9-48cf-4cbe-a00e-27c6bb332588/generateAnswer");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("EndpointKey", "1e3eced3-ef47-4091-9139-2651a5bafa60");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}