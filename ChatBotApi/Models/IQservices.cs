using System.Threading.Tasks;

namespace ChatBotApi.Models
{
    public interface IQservices
    {
        Task<string> GetAnswerAsync(Question question);
    }
}