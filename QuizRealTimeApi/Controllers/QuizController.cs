﻿using System;
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
        public async Task<IActionResult> Post(int receiverID, int qn, int qa)
        {
            await _hubContext.Clients.All.SendAsync("QuizInfo", receiverID, qn, qa);
            
            return Ok("Answer has been sent successfully!");
        }
        [HttpPost("SohwConnected")]
        public async Task<IActionResult> GetConnected(int receiverID, int studentID )
        {
            await _hubContext.Clients.All.SendAsync("Connected", receiverID,studentID);

            return Ok("Conneted");
        }
        [HttpPost("Reject")]
        public async Task<IActionResult> Rejected(int receiverID, int studentID)
        {
            await _hubContext.Clients.All.SendAsync("Rejected", receiverID, studentID);

            return Ok("Rejected");
        }
        [HttpPost("MatchconnectRequest")]
        public async Task<IActionResult> connectionsts(int receiverID, int studentID, string matchID)
        {
            await _hubContext.Clients.All.SendAsync("mcr", receiverID, studentID, matchID);

            return Ok("matchconnected");
        }
        [HttpPost("SendDoneMsg")]
        public async Task<IActionResult> done(int receiverID, int studentID, int ra, int time)
        {
            await _hubContext.Clients.All.SendAsync("donemsg", receiverID, studentID, ra,time);

            return Ok("sent");
        }
    }
}