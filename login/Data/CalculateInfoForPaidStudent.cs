using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.Data
{
    public class CalculateInfoForPaidStudent
    {
        List<PlayerHistory> playerHistory = new List<PlayerHistory> ();

        int ttqB1;
        int ttqB2;
        int ttqICT;
        int ttqPhy;
        int ttqChe;
        int ttqBIO;

        int anqB1;
        int anqB2;
        int anqICT;
        int anqPhy;
        int anqChe;
        int anqBIO;

        int accB1;
        int accB2;
        int accICT;
        int accPhy;
        int accChe;
        int accBIO;

        public void setPlayer( List<PlayerHistory> ph )
        {
            playerHistory = ph;
        }
        public void calTotalQuestionOfEverySubject()
        {
            
        }
    }
}
