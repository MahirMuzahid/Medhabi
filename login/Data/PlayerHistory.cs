using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.Data
{
    public class PlayerHistory
    {
        public string matchID { get; set; }
        public string matchStatus { get; set; }
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
        public int playerID { get; set; }
        public int whatToDO { get; set; }
        public string response { get; set; }
        public int status { get; set; }
    }
}
