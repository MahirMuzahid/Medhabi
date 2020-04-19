using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace login.Data
{
    public class ServerConnection
    {
        public int qsNumber { get; set; }

        public ServerConnection ()
        {
            getTotalQuestion();
        }
        public async void getTotalQuestion()
        {
            string url = "https://api.shikkhanobish.com/api/Medhabi/GetQuestionNumber";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var question = JsonConvert.DeserializeObject<Question>(result);
            if(question.stetus == 0)
            {
                qsNumber = question.amount;
            }
            
        }
    }
}
