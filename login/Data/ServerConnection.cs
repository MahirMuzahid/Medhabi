using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using login.Pages;
using System.Collections.Generic;

namespace login.Data
{
    public class ServerConnection
    {
        public int qsNumber { get; set; }
        public int submitted { get; set; }
        public Student student { get; set; }
        public Teacher teacher { get; set; }
        public List<Student> TopStudentList { get; set; }
        public List<Student> allStudent { get; set; }
        public List<Teacher> allTeacher { get; set; }
        public List<Teacher> TopTeacherList { get; set; }

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
            string jsonData = JsonConvert.SerializeObject(new { question_code = q.question_code, question = q.question, first_choice = q.first_choice, second_choice = q.second_choice, third_choice = q.third_choice, forth_choice = q.forth_choice, right_answer = q.right_answer, name = q.name, teacherID  = q.teacherID});
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
            if (s.mail != null)
            {
                submitted = 0;

            }
            else
            {
                submitted = 1;
            }

        }
        public async Task SignInTeacher(string Mail, string pass)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/SignInTeacher";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { mail = Mail, password = pass });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var t = JsonConvert.DeserializeObject<Teacher>(result);
            teacher = t;
            if (t.mail != null)
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

        public async Task GetTopStudent()
        {
            List<Student> tenList = new List<Student>();
            string url = "https://api.shikkhanobish.com/api/Medhabi/GetTopStudents";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var studentList = JsonConvert.DeserializeObject<List<Student>>(result);
            allStudent = studentList;
            if (studentList[0].name != null)
            {
                submitted = 0;
                for (int i = 0; i < 10; i++)
                {
                    tenList.Add(studentList[i]);
                }
            }
            else
            {
                submitted = 1;
            }
            TopStudentList = tenList;
        }
        public async Task GetTopTeacher()
        {
            List<Teacher> topTen = new List<Teacher>();
            string url = "https://api.shikkhanobish.com/api/Medhabi/GetTopTeacher";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var TeacherList = JsonConvert.DeserializeObject<List<Teacher>>(result);
            allTeacher = TeacherList;
            if (TeacherList[0].name != null)
            {
                submitted = 0;
                for (int i = 0; i < 10; i++)
                {
                    topTen.Add(TeacherList[i]);
                }

            }
            else
            {
                submitted = 1;
            }
            TopTeacherList = topTen;
        }
    }
}