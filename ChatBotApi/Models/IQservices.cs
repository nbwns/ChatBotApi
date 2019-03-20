using System.Threading.Tasks;

namespace ChatBotApi.Models
{
    public interface IQservices
    {
        Task<QnaResponse> GetAnswerAsync(Question question);
    }
}