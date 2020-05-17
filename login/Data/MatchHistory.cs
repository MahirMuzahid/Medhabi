﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.Data
{
    public class MatchHistory
    {
        public string matchID { get; set; }
        public int winnerPlayerID { get; set; }
        public int looserPlayerID { get; set; }
        public int q1 { get; set; }
        public int q2 { get; set; }
        public int q3 { get; set; }
        public int q4 { get; set; }
        public int q5 { get; set; }
        public int whatToDO { get; set; }
        public string response { get; set; }
        public int status { get; set; }
    }
}
