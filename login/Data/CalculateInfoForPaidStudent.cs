using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.Data
{
    public class CalculateInfoForPaidStudent
    {
        List<PlayerHistory> playerHistory = new List<PlayerHistory> ();

        int tqB1;
        int tqB2;
        int tqICT;
        int tqPhy;
        int tqChe;
        int tqBIO;

        int aqB1;
        int aqB2;
        int aqICT;
        int aqPhy;
        int aqChe;
        int aqBIO;

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
