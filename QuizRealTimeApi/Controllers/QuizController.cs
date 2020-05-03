using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace QuizRealTimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private readonly IHubContext<QuizHub> _hubContext;
        public QuizInfo info = new QuizInfo();
        public QuizController(IHubContext<QuizHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("PassAnswer")]
        public async Task<IActionResult> Post([FromQuery]int qn, int qa)
        {
            info.QuestionNumber = qn;
            info.AnswerNumber = qa;
            await _hubContext.Clients.All.SendAsync("QuizInfo",info);
            
            return Ok("Answer has been sent successfully!");
        }
        [HttpPost("SohwConnected")]
        public async Task<IActionResult> GetConnected(int receiverID, int studentID )
        {
            await _hubContext.Clients.All.SendAsync("Connected", receiverID,studentID);

            return Ok("Conneted");
        }
    }
}