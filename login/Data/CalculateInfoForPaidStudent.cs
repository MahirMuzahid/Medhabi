using System.Collections.Generic;
using login.Pages;

namespace login.Data
{
    public class CalculateInfoForPaidStudent
    {
        private int ttqB1;
        private int ttqB2;
        private int ttqICT;
        private int ttqPhy1;
        private int ttqChe1;
        private int ttqBio1;
        private int ttqPhy2;
        private int ttqChe2;
        private int ttqBio2;

        private int anqB1;
        private int anqB2;
        private int anqICT;
        private int anqPhy1;
        private int anqChe1;
        private int anqBio1;
        private int anqPhy2;
        private int anqChe2;
        private int anqBio2;

        private int accB1;
        private int accB2;
        private int accICT;
        private int accPhy1;
        private int accChe1;
        private int accBio1;
        private int accPhy2;
        private int accChe2;
        private int accBio2;

        private int thisID;
        private List<PlayerHistory> MyPhList = new List<PlayerHistory> ();
        private List<string> qsCode = new List<string> ();
        public static List<int> allqs { get; set; }
        public static List<int> allans { get; set; }

        public void setDataforCalculation ( List<PlayerHistory> ph , int id )
        {
            MyPhList = ph;
            thisID = id;
            for ( int i = 0; i < MyPhList.Count; i++ )
            {
                qsCode.Add ( MyPhList [ i ].q1 );
                qsCode.Add ( MyPhList [ i ].q2 );
                qsCode.Add ( MyPhList [ i ].q3 );
                qsCode.Add ( MyPhList [ i ].q4 );
                qsCode.Add ( MyPhList [ i ].q5 );
            }
            calTotalQuestionOfEverySubject ( qsCode );
        }

        public void calTotalQuestionOfEverySubject ( List<string> qsCode )
        {
            string Sub = null;
            for ( int i = 0; i < qsCode.Count; i++ )
            {
                Sub = qsCode [ i ] [ 5 ].ToString () + qsCode [ i ] [ 6 ].ToString () + qsCode [ i ] [ 7 ].ToString () + "";
                if ( Sub == "ICT" )
                {
                    anqICT++;
                    if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                    {
                        accICT++;
                    }
                }
                else
                {
                    Sub = qsCode [ i ] [ 5 ].ToString () + qsCode [ i ] [ 6 ].ToString () + qsCode [ i ] [ 7 ].ToString () + qsCode [ i ] [ 8 ].ToString () + qsCode [ i ] [ 9 ].ToString () + "";

                    if ( Sub == "PHY01" )
                    {
                        anqPhy1++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accPhy1++;
                        }
                    }
                    else if ( Sub == "PHY02" )
                    {
                        anqPhy2++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accPhy2++;
                        }
                    }
                    else if ( Sub == "CHE01" )
                    {
                        anqChe1++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accChe1++;
                        }                       
                    }
                    else if ( Sub == "CHE02" )
                    {
                        anqChe2++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accChe2++;
                        }
                    }
                    else if ( Sub == "BIO01" )
                    {
                        anqBio1++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accBio1++;
                        }
                    }
                    else if ( Sub == "BIO03" )
                    {
                        anqBio2++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accBio2++;
                        }
                    }
                    else if ( Sub == "BAN01" )
                    {
                        anqB1++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accB1++;
                        }
                    }
                    else if ( Sub == "BAN02" )
                    {
                        anqB2++;
                        if ( int.Parse ( qsCode [ i ] [ 0 ].ToString () ) == 1 )
                        {
                            accB2++;
                        }
                    }
                }
            }
            List<int> allqsForShow = new List<int> ();
            allqsForShow.Add ( anqB1 );
            allqsForShow.Add ( anqB2 );
            allqsForShow.Add ( anqICT );
            allqsForShow.Add ( anqPhy1 );
            allqsForShow.Add ( anqPhy2 );
            allqsForShow.Add ( anqChe1 );
            allqsForShow.Add ( anqChe2 );
            allqsForShow.Add ( anqBio1 );
            allqsForShow.Add ( anqBio2 );

            List<int> allansForShow = new List<int> ();
            allansForShow.Add ( accB1 );
            allansForShow.Add ( accB2 );
            allansForShow.Add ( accICT );
            allansForShow.Add ( accPhy1 );
            allansForShow.Add ( accPhy2 );
            allansForShow.Add ( accChe1 );
            allansForShow.Add ( accChe2 );
            allansForShow.Add ( accBio1 );
            allansForShow.Add ( accBio2 );

            
            allqs = allqsForShow;
            allans = allansForShow;
        }

    }
}