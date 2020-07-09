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
        private static int anqPhy1;
        private static int anqChe1;
        private static int anqBio1;
        private static int anqPhy2;
        private static int anqChe2;
        private static int anqBio2;

        private int accB1;
        private int accB2;
        private int accICT;
        private static int accPhy1;
        private static int accChe1;
        private static int accBio1;
        private static int accPhy2;
        private static int accChe2;
        private static int accBio2;

        private int thisID;
        private List<PlayerHistory> MyPhList = new List<PlayerHistory>();
        private List<string> qsCode = new List<string>();
        private static int avgPhy1,avgPhy2,avgChe1,avgChe2,avgBio1,avgBio2;

        public static List<double> RaderInfo { get; set; }
        public List<double> allRaderInfo { get; set; }
        public static List<double> AllRaderInfo { get; set; }
        public static List<int> allqs { get; set; }
        public static List<int> allans { get; set; }

        public static List<int> accuracy = new List<int>();
        public static List<string> allClass { get; set; }
        public static List<string> allSubject { get; set; }
        public static List<string> allPaper { get; set; }
        public static List<string> allChapter { get; set; }
        public static List<string> allquestionStatus { get; set; }
        public static List<int> allTime  { get; set; }

        public static int raderCounter { get; set; }


        private static List<int> phy1performance = new List<int>();
        private static List<int> phy2performance = new List<int>();
        private static List<int> che1performance = new List<int>();
        private static List<int> che2performance = new List<int>();
        private static List<int> bio1performance = new List<int>();
        private static List<int> bio2performance = new List<int>();


        public static int winCount;
        public static int loseCount;

        public static string name;
        //Initial Call From ServerConnection Class
        public void setDataforCalculation ( List<PlayerHistory> ph , int id )
        {
            List<string> qsCodeini = new List<string> () ;
            MyPhList = ph;
            thisID = id;
            for ( int i = 0; i < MyPhList.Count; i++ )
            {
                qsCodeini.Add ( MyPhList [ i ].q1 );
                qsCodeini.Add ( MyPhList [ i ].q2 );
                qsCodeini.Add ( MyPhList [ i ].q3 );
                qsCodeini.Add ( MyPhList [ i ].q4 );
                qsCodeini.Add ( MyPhList [ i ].q5 ); 
            }
            SeparateEverySegmentOfCode ( qsCodeini );
            calTotalQuestionOfEverySubject ();
            CalculateAccuracyForEveryMatch();
            CalculateWinLose();
            CalculatePerfomanceOfEveryCoreSubject();
        }

        //Make saparate variable for every segment
        public void SeparateEverySegmentOfCode ( List<string> code )
        {
            List<string> allClassini = new List<string> ();
            List<string> allSubjectini = new List<string> ();
            List<string> allPaperini = new List<string> ();
            List<string> allChapterini = new List<string> ();
            List<string> allquestionStatusini = new List<string> ();
            List<int> allTimeini = new List<int>();
            string Class, subject, paper, chapter, questionStatus = null;
            int time = 0;
            string strTime = null;
            for ( int i = 0; i < code.Count; i++ )
            {
                var singleCode = code [ i ].ToUpper ();
                qsCode.Add ( singleCode );
                if ( singleCode [ 0 ] == '0' )
                {
                    questionStatus = "wrong";
                }
                else if ( singleCode [ 0 ] == '1' )
                {
                    questionStatus = "right";
                }

                Class = singleCode [ 1 ].ToString () + singleCode [ 2 ].ToString () + singleCode [ 3 ].ToString () + "";
                subject = singleCode [ 4 ].ToString () + singleCode [ 5 ].ToString () + singleCode [ 6 ].ToString () + "";
                paper = singleCode [ 7 ].ToString () + singleCode [ 8 ].ToString () + "";
                chapter = singleCode [ 9 ].ToString () + singleCode [ 10 ].ToString () + "";
                if(singleCode.Length == 12)
                {
                    strTime = singleCode[11].ToString() + "";
                    time = int.Parse(strTime);
                }
                else if (singleCode.Length == 13)
                {
                    strTime = singleCode[11].ToString() + singleCode[12].ToString() + "";
                    time = int.Parse(strTime);
                }
                else if (singleCode.Length == 14)
                {                    
                    strTime = singleCode[11].ToString() + singleCode[12].ToString() + singleCode[13].ToString() + "";
                    time = int.Parse(strTime);
                }
                
                allClassini.Add ( Class );
                allSubjectini.Add ( subject );
                allPaperini.Add ( paper );
                allChapterini.Add ( chapter );
                allquestionStatusini.Add ( questionStatus );
                allTimeini.Add(time);
            }
            allClass = allClassini;
            allSubject = allSubjectini;
            allPaper = allPaperini;
            allChapter = allChapterini;
            allquestionStatus = allquestionStatusini;
            allTime = allTimeini;
            

        }

        //Calculate Total qsestion and total right answer for every subject
        public void calTotalQuestionOfEverySubject ( )
        {
            string Sub = null;
            for ( int i = 0; i < qsCode.Count; i++ )
            {
                Sub = allSubject [ i ];
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
                    Sub = allSubject [ i ] + allPaper [ i ];

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
                    else if ( Sub == "BIO02" )
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


        public void CalculateAccuracyForEveryMatch()
        {
            List<int> Accuracy = new List<int> ();
            int save = 0;
            for (int i = 0; i < qsCode.Count; i++ )
            {
                int accuracyCounter = 0;
                if(i % 5 == 0 && i > 0)
                {                  
                    for(int j = i; j >= save; j-- )
                    {
                        var singleCode = qsCode [ j ].ToUpper ();
                        if (singleCode[0] == '1')
                        {
                            accuracyCounter++;
                        }
                    }
                    Accuracy.Add ( accuracyCounter );
                    save = i;
                }               
            }
            accuracy = Accuracy;
           
        }


        public void CalculateWinLose()
        {
            int winCounter = 0, loseCounter = 0;
            for (int i = 0; i < MyPhList.Count; i++)
            {
                if(MyPhList[i].matchStatus == "Win")
                {
                    winCounter++;
                }
                else
                {
                    loseCounter++;
                }
            }
            winCount = winCounter;
            loseCount = loseCounter;
           
        }

        
        public void CalculatePerfomanceOfEveryCoreSubject ()
        {
            string Sub = null;
            List<int> ph1Time = new List<int>();
            List<int> ph2Time = new List<int>();
            List<int> che1Time = new List<int>();
            List<int> che2Time = new List<int>();
            List<int> bio1Time = new List<int>();
            List<int> bio2Time = new List<int>();
            int p1total = 0, p2total = 0, c1total = 0, c2total = 0, b1total = 0, b2total = 0;
            for (int i = 0; i < qsCode.Count; i++)
            {                
                Sub = allSubject[i] + allPaper[i];
                if (Sub == "PHY01")
                {                   
                    ph1Time.Add(getSingleTime(i));
                }
                else if (Sub == "PHY02")
                {
                    ph2Time.Add(getSingleTime(i));
                }
                else if (Sub == "CHE01")
                {
                    che1Time.Add(getSingleTime(i));
                }
                else if (Sub == "CHE02")
                {
                    che2Time.Add(getSingleTime(i));
                }
                else if (Sub == "BIO01")
                {
                    bio1Time.Add(getSingleTime(i));
                }
                else if (Sub == "BIO02")
                {
                    bio2Time.Add(getSingleTime(i));
                }
            }
            for(int j = 0; j < ph1Time.Count; j++)
            {
                p1total = ph1Time[j] + p1total;
            }
            for (int j = 0; j < ph2Time.Count; j++)
            {
                p2total = ph2Time[j] + p2total;
            }
            for (int j = 0; j < che1Time.Count; j++)
            {
                c1total = che1Time[j] + c1total;
            }
            for (int j = 0; j < che2Time.Count; j++)

            {
                c2total = che2Time[j] + c2total;
            }
            for (int j = 0; j < bio1Time.Count; j++)
            {
                b1total = bio1Time[j] + b1total;
            }
            for (int j = 0; j < bio2Time.Count; j++)
            {
                b2total = bio2Time[j] + b2total;
            }

            int avgTime;
            if (ph1Time.Count == 0)
            {
                avgPhy1 = 0;
            }
            else
            {
                avgTime = p1total / ph1Time.Count;
                avgPhy1 = avgTime;
            }

            if (ph2Time.Count == 0)
            {
                avgPhy2 = 0;
            }
            else
            {
                avgTime = p2total / ph2Time.Count;
                avgPhy2 = avgTime;
            }
            
            if (che1Time.Count == 0)
            {
                avgChe1 = 0;
            }
            else
            {
                avgTime = c1total / che1Time.Count;
                avgChe1 = avgTime;
            }
            
            if (che2Time.Count == 0)
            {
                avgChe2 = 0;
            }
            else
            {
                avgTime = c2total / che2Time.Count;
                avgChe2 = avgTime;
            }
            
            if (bio1Time.Count == 0)
            {
                avgBio1 = 0;
            }
            else
            {
                avgTime = b1total / bio1Time.Count;
                avgBio1 = avgTime;
            }           
            if (bio2Time.Count == 0)
            {
                avgBio2 = 0;
            }
            else
            {
                avgTime = b2total / bio2Time.Count;
                avgBio2 = avgTime;
            }
            
        }


        public int getSingleTime(int i)
        {
            var singleCode = qsCode[i].ToUpper();
            int Ftime = 0, Stime = 0;
            string strTime = null;
            if (singleCode.Length == 12)
            {
                strTime = singleCode[11].ToString() + "";
                Stime = int.Parse(strTime);
            }
            else if (singleCode.Length == 13)
            {
                strTime = singleCode[11].ToString() + singleCode[12].ToString() + "";
                Stime = int.Parse(strTime);
            }
            else if (singleCode.Length == 14)
            {
                strTime = singleCode[11].ToString() + singleCode[12].ToString() + singleCode[13].ToString() + "";
                Stime = int.Parse(strTime);
            }
            int time;
            if (i%5 != 0) //deviding all qs 5 per segment
            {
                var lastSingleCode = qsCode[i - 1].ToUpper();
                if (lastSingleCode.Length == 12)
                {
                    strTime = lastSingleCode[11].ToString() + "";
                    Ftime = int.Parse(strTime);
                }
                else if (lastSingleCode.Length == 13)
                {
                    strTime = lastSingleCode[11].ToString() + lastSingleCode[12].ToString() + "";
                    Ftime = int.Parse(strTime);
                }
                else if (lastSingleCode.Length == 14)
                {
                    strTime = lastSingleCode[11].ToString() + lastSingleCode[12].ToString() + lastSingleCode[13].ToString() + "";
                    Ftime = int.Parse(strTime);
                }
                time = Stime - Ftime;
                return time;
            }
            else
            {
                return Stime;
            }
            
            
        }

  
        public void raderinfo()
        {
            if (raderCounter == 6)
            {
                raderCounter = 1;
            }
            else
            {
                raderCounter++;
            }        
            if(raderCounter == 1)
            {
                name = "Physics First Paper";
                var pertialTime = 0;
                double amount, acc, time;
                if (raderCounter == 1)
                {
                    if(anqPhy1 == 0)
                    {
                        acc = 0;
                    }
                    else
                    {
                        acc = (int)(((float)accPhy1 / (float)anqPhy1) * 100);
                    }
                    
                    time = avgPhy1;
                    amount = anqPhy1;

                    if (avgPhy1 < 30) //this will be 30
                    {
                        time = 100;
                    }
                    else
                    {
                        //if 30 sec avg than 100 performence. If 150 sec avg than 0 performace
                        pertialTime = 90 - (avgPhy1 - 30);
                        time = ((100 / 60) * pertialTime);
                        if (time < 30)
                        {
                            time = time + 5;
                        }
                        else if (time < 50)
                        {
                            time = time + 10;
                        }
                        else if (time < 80)
                        {
                            time = time + 5;
                        }
                    }
                    amount = (int)(amount / 2);
                    List<double> ri = new List<double>();
                    
                   
                    ri.Add(time);
                    ri.Add(acc);
                    ri.Add(amount);
                    RaderInfo = ri;
                    allRaderInfo.Add(time);
                    allRaderInfo.Add(acc);
                    allRaderInfo.Add(amount);
                }
            }
            if (raderCounter == 2)
            {
                name = "Physics Second Paper";
                var pertialTime = 0;
                double amount, acc, time;
                if (anqPhy2 == 0)
                {
                    acc = 0;
                }
                else
                {
                    acc = (int)(((float)accPhy2 / (float)anqPhy2) * 100);
                }
               
                time = avgPhy2;
                amount = anqPhy2;

                if (avgPhy2 < 30) //this will be 30
                {
                    time = 100;
                }
                else
                {
                    //if 30 sec avg than 100 performence. If 150 sec avg than 0 performace
                    pertialTime = 90 - (avgPhy2 - 30);
                    time = ((100 / 60) * pertialTime);
                    if (time < 30)
                    {
                        time = time + 5;
                    }
                    else if (time < 50)
                    {
                        time = time + 10;
                    }
                    else if (time < 80)
                    {
                        time = time + 5;
                    }
                }
                amount = (int)(amount / 2);
                List<double> ri = new List<double>();
                ri.Add(time);
                ri.Add(acc);
                ri.Add(amount);
                RaderInfo = ri;
                allRaderInfo.Add(time);
                allRaderInfo.Add(acc);
                allRaderInfo.Add(amount);
            }
            if (raderCounter == 3)
            {
                name = "Chemistry First Paper";
                var pertialTime = 0;
                double amount, acc, time;
                if (anqChe1 == 0)
                {
                    acc = 0;
                }
                else {
                    acc = (int)(((float)accChe1 / (float)anqChe1) * 100);
                }
                
                time = avgChe1;
                amount = anqChe1;

                if (avgChe1 < 30) //this will be 30
                {
                    time = 100;
                }
                else
                {
                    //if 30 sec avg than 100 performence. If 150 sec avg than 0 performace
                    pertialTime = 90 - (avgChe1 - 30);
                    time = ((100 / 60) * pertialTime);
                    if (time < 30)
                    {
                        time = time + 5;
                    }
                    else if (time < 50)
                    {
                        time = time + 10;
                    }
                    else if (time < 80)
                    {
                        time = time + 5;
                    }
                }
                amount = (int)(amount / 2);
                List<double> ri = new List<double>();
                ri.Add(time);
                ri.Add(acc);
                ri.Add(amount);
                RaderInfo = ri;
                allRaderInfo.Add(time);
                allRaderInfo.Add(acc);
                allRaderInfo.Add(amount);
            }
            if (raderCounter == 4)
            {
                name = "Chemistry Second Paper";
                var pertialTime = 0;
                double amount, acc, time;
                if (anqChe2 == 0)
                {
                    acc = 0;
                }
                else
                {
                    acc = (int)(((float)accChe2 / (float)anqChe2) * 100);
                }
               
                time = avgChe2;
                amount = anqChe2;

                if (avgChe2 < 30) //this will be 30
                {
                    time = 100;
                }
                else
                {
                    //if 30 sec avg than 100 performence. If 150 sec avg than 0 performace
                    pertialTime = 90 - (avgChe2 - 30);
                    time = ((100 / 60) * pertialTime);
                    if (time < 30)
                    {
                        time = time + 5;
                    }
                    else if (time < 50)
                    {
                        time = time + 10;
                    }
                    else if (time < 80)
                    {
                        time = time + 5;
                    }
                }
                amount = (int)(amount / 2);
                List<double> ri = new List<double>();
                ri.Add(time);
                ri.Add(acc);
                ri.Add(amount);
                RaderInfo = ri;
                allRaderInfo.Add(time);
                allRaderInfo.Add(acc);
                allRaderInfo.Add(amount);
            }
            if (raderCounter == 5)
            {
                name = "Biology First Paper";
                var pertialTime = 0;
                double amount, acc, time;
                if (anqBio1 == 0)
                {
                    acc = 0;
                }
                else
                {
                    acc = (int)(((float)accBio1 / (float)anqBio1) * 100);
                }
               
                time = avgBio1;
                amount = anqBio1;

                if (avgBio1 < 30) //this will be 30
                {
                    time = 100;
                }
                else
                {
                    //if 30 sec avg than 100 performence. If 150 sec avg than 0 performace
                    pertialTime = 90 - (avgBio1 - 30);
                    time = ((100 / 60) * pertialTime);
                    if (time < 30)
                    {
                        time = time + 5;
                    }
                    else if (time < 50)
                    {
                        time = time + 10;
                    }
                    else if (time < 80)
                    {
                        time = time + 5;
                    }
                }
                amount = (int)(amount / 2);
                List<double> ri = new List<double>();
                ri.Add(time);
                ri.Add(acc);
                ri.Add(amount);
                RaderInfo = ri;
                allRaderInfo.Add(time);
                allRaderInfo.Add(acc);
                allRaderInfo.Add(amount);
            }
            if (raderCounter == 5)
            {
                name = "Biology Second Paper";
                var pertialTime = 0;
                double amount, acc, time;
                if (anqBio2 == 0)
                {
                    acc = 0;
                }
                else
                {
                    acc = (int)(((float)accBio2 / (float)anqBio2) * 100);
                }
                time = avgBio2;
                amount = anqBio2;

                if (avgBio2 < 30) //this will be 30
                {
                    time = 100;
                }
                else
                {
                    //if 30 sec avg than 100 performence. If 150 sec avg than 0 performace
                    pertialTime = 90 - (avgBio2 - 30);
                    time = ((100 / 60) * pertialTime);
                    if (time < 30)
                    {
                        time = time + 5;
                    }
                    else if (time < 50)
                    {
                        time = time + 10;
                    }
                    else if (time < 80)
                    {
                        time = time + 5;
                    }
                }
                amount = (int)(amount / 2);
                List<double> ri = new List<double>();
                ri.Add(time);
                ri.Add(acc);
                ri.Add(amount);
                RaderInfo = ri;
                allRaderInfo.Add(time);
                allRaderInfo.Add(acc);
                allRaderInfo.Add(amount);
            }


        }
    }
}