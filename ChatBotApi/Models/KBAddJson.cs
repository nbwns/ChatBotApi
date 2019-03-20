namespace ChatBotApi.Models
{
    public class KBAddJson
    {

        public KBAddJson(string question, string answer)
        {
            this.add = new Add(question, answer);
            this.update = null;
            this.delete = null;
        }
        public Add add { get; set; }
        public Update update { get; set; }
        public Delete delete { get; set; }
    }

    public class Add
    {
        public Add(string question, string answer)
        {
            this.qnaList = new Qnalist[] { new Qnalist(question, answer) };
        }
        public Qnalist[] qnaList { get; set; }
        public object[] urls { get; set; }
    }

    public class Qnalist
    {

        public Qnalist(string question, string answer)
        {
            this.id = 0;
            this.questions = new string[] { question };
            this.answer = answer;
            this.source = "Editorial";
            this.metadata = null;
        }
        public int id { get; set; }
        public string answer { get; set; }
        public string source { get; set; }
        public string[] questions { get; set; }
        public Metadata[] metadata { get; set; }
    }

    public class Metadata
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Update
    {
    }

    public class Delete
    {
    }


}