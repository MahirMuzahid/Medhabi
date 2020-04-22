using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using login.Pages;

namespace login.Data
{
    public class ServerConnection
    {
        public int qsNumber { get; set; }
        public int submitted { get; set; }
        public Student student { get; set; }

        public ServerConnection()
        {
        }

        public async Task getTotalQuestion()
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/GetQuestionNumber";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var question = JsonConvert.DeserializeObject<Question>(result);
            if (question.stetus == 0)
            {
                qsNumber = question.amount;
            }
        }

        public async Task SubmitQuestion(Question q)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/SubmitQuestion";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { question_code = q.question_code, question = q.question, first_choice = q.first_choice, second_choice = q.second_choice, third_choice = q.third_choice, forth_choice = q.forth_choice, right_answer = q.right_answer, name = q.name });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<Response>(result);
            submitted = r.status;
            
        }

        public async Task SignIn(string Mail, string pass)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/SignIn";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { mail = Mail, password = pass });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var s = JsonConvert.DeserializeObject<Student>(result);
            student = s;
            if(s.mail != null)
            {
                submitted = 0;
                
            }
            else
            {
                submitted = 1;
            }
            
        }
        public async Task SignUp(string Mail, string pass, string name, string phonenumber)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/SignUp";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { mail = Mail, password = pass, name = name, phonenumber = phonenumber });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<Response>(result);
            submitted = r.status;
        }
    }
}