using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.Data
{
    public class RealTimeServerConnection
    {
        string url = "http://localhost:51173/QuizHub";

        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";

        List<QuizInfo> quizinfo = new List<QuizInfo>();
        int r = 0;

        private async Task ConnectToServer()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            await _connection.StartAsync();
            isConnected = true;
            connectionStatus = "Connected";

            _connection.Closed += async (s) =>
            {
                isConnected = false;
                connectionStatus = "Disconnected";
                await _connection.StartAsync();
                isConnected = true;
            };

            _connection.On<QuizInfo>("QuizInfo", m =>
            {
                //oqn = m.QuestionNumber;
                //oan = m.AnswerNumber;
                ///StateHasChanged(); have to add this is UI
            });
        }
    }
}
