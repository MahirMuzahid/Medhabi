using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace login.Data
{
    public class CalculateInfoForPaidStudent
    {

        int ttqB1;
        int ttqB2;
        int ttqICT;
        int ttqPhy1;
        int ttqChe1;
        int ttqBIO1;
        int ttqPhy2;
        int ttqChe2;
        int ttqBIO2;

        int anqB1;
        int anqB2;
        int anqICT;
        int anqPhy1;
        int anqChe1;
        int anqBIO1;
        int anqPhy2;
        int anqChe2;
        int anqBIO2;

        int accB1;
        int accB2;
        int accICT;
        int accPhy1;
        int accChe1;
        int accBIO1;
        int accPhy2;
        int accChe2;
        int accBIO2;

        int thisID;
        List<PlayerHistory> alPhList = new List<PlayerHistory> ();
        public void getDataforCalculation(List<PlayerHistory> ph, int id)
        {
            alPhList = ph;
            thisID = id;
        }

        
        public void calTotalQuestionOfEverySubject(List<string> qsCode)
        {
            string Sub = null ;
            for(int i  = 0; i < qsCode.Count; i++ )
            {
                Sub = qsCode [ i ] [ 5 ] + qsCode [ i ] [ 6 ] + qsCode [ i ] [ 7 ] + "";
                if (Sub == "ICT")
                {
                    ttqICT++ ;
                }
                else
                {
                    Sub = qsCode [ i ] [ 5 ] + qsCode [ i ] [ 6 ] + qsCode [ i ] [ 7 ] + qsCode [ i ] [ 8 ] + qsCode [ i ] [ 9 ] + "";

                    for ( int j = 0; j < 6; i++ )
                    {
                        if ( Sub == "PHY01" )
                        {
                            ttqPhy1++;
                        }
                        else if ( Sub == "PHY01" )
                        {
                            ttqPhy2++;
                        }
                        else if ( Sub == "CHE01" )
                        {
                            ttqChe1++;
                        }
                        else if ( Sub == "CHE01" )
                        {
                            ttqChe2++;
                        }
                        else if ( Sub == "BIO01" )
                        {
                            ttqBIO1++;
                        }
                        else if ( Sub == "BIO01" )
                        {
                            ttqBIO2++;
                        }
                        else if ( Sub == "BAN" )
                        {
                            ttqB1++;
                        }
                        else if ( Sub == "BAN" )
                        {
                            ttqB2++;
                        }
                    }
                }
                
            }
        }

    }
}
