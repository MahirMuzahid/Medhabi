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
        public Student secondstudent { get; set; }
        public Teacher teacher { get; set; }
        public List<Student> TopStudentList { get; set; }
        public List<Student> allStudent { get; set; }
        public List<Teacher> allTeacher { get; set; }
        public List<Teacher> TopTeacherList { get; set; }
        public List<MatchMaking> matchmakingList { get; set; }
        public MatchMaking ConnectedMatch { get; set; }
        public List<Question> allqs { get; set; }
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
                for (int i = 0; i < allStudent.Count; i++)
                {
                    if(i == 10)
                    {
                        break;
                    }
                    else
                    {
                        tenList.Add(studentList[i]);
                    }
                    
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
                for (int i = 0; i < allTeacher.Count; i++)
                {
                    if( i == 10)
                    {
                        break;
                    }
                    else
                    {
                        topTen.Add(TeacherList[i]);
                    }
                    
                }

            }
            else
            {
                submitted = 1;
            }
            TopTeacherList = topTen;
        }

        public async Task GetMatchMaking()
        {
            List<MatchMaking> topTen = new List<MatchMaking>();
            string url = "https://api.shikkhanobish.com/api/Medhabi/GetMatchMaking";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var MatchMaking = JsonConvert.DeserializeObject<List<MatchMaking>>(result);
            matchmakingList = MatchMaking;
        }

        public async Task DeleteIfFound(int id)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/DeleteIfFoundMatch";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { firstPlayerID = id });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<MatchMaking>(result);
            ConnectedMatch = r;
        }
        public async Task SetMachMaking(MatchMaking mm)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/SetMachMaking";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { firstPlayerID = mm.firstPlayerID, matchID = mm.matchID, firstQuestionID = mm.firstQuestionID, secondQuestionID = mm.secondQuestionID, thirdQuestionID = mm.thirdQuestionID, forthQuestionID = mm.forthQuestionID, fifthQuestionID = mm.fifthQuestionID });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<Response>(result);
            submitted = r.status;
        }

        public async Task MakeQuestion(QuestionMaker qs)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/MakeQuestion";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { firstqsID = qs.firstqsID, secondID = qs.secondID, thirdID = qs.thirdID, forthID = qs.forthID, fifthID = qs.fifthID });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<List<Question>>(result);

            if(r.Count < 5)
            {
                submitted = 1;
            }
            else
            {
                submitted = 0;
                allqs = r;
            }
        }

        public async Task ConnectWith2ndStudent( int recevierID, int studentID)
        {
            string url = "https://medhabiapi.shikkhanobish.com/api/Quiz/SohwConnected?receiverID=" + recevierID + "&studentID=" + studentID;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<string>(result);
        }

        public async Task getInfoFromId(int id)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/GetInfoFromId";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { studentID = id });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var s = JsonConvert.DeserializeObject<Student>(result);
            secondstudent = s;
            if (s.mail != null)
            {
                submitted = 0;

            }
            else
            {
                submitted = 1;
            }

        }

        public async Task DidactPoint(int id)
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/DidactPoint";
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { studentID = id });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<Response>(result);
            submitted = r.status;
        }

        public async Task sendIfReject(int recevierID, int studentID)
        {
            string url = "https://medhabiapi.shikkhanobish.com/api/Quiz/Reject?receiverID=" + recevierID + "&studentID=" + studentID;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<string>(result);
        }
        public async Task sendmatchInfo(int recevierID, int studentID, string matchmakingID)
        {
            string url = "https://medhabiapi.shikkhanobish.com/api/Quiz/Matchconnect?receiverID=" + recevierID + "&studentID=" + studentID + "&matchID=" + matchmakingID ;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var r = JsonConvert.DeserializeObject<string>(result);
        }
    }
}