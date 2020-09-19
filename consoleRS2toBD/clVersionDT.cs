using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class clVersionDT
{
    public static string Decript()
    {
        String strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        //Console.WriteLine("Version:" + strVersion);

        int indexDay = strVersion.IndexOf(".");
        string sDay = strVersion.Remove(indexDay, strVersion.Length - indexDay);
        if (sDay.Length == 1) sDay = "0" + sDay;
        //Console.WriteLine(sDay);

        string sMountB = strVersion.Remove(0, indexDay + 1);
        int indexMount = sMountB.IndexOf(".");
        string sMountE = sMountB.Remove(indexMount, sMountB.Length - indexMount);
        if (sMountE.Length == 1) sMountE = "0" + sMountE;
        //Console.WriteLine(sMountE);

        string sYearB = sMountB.Remove(0, indexMount + 1);
        int indexYear = sYearB.IndexOf(".");
        string sYearE = sYearB.Remove(indexYear, sYearB.Length - indexYear);
        //Console.WriteLine(sYearE);

        //--------------------------------------------------------------------------------------------------------------------------------------
        //string sDays = (Int32.Parse(strVersion.Remove(strVersion.IndexOf("."), strVersion.Length - strVersion.IndexOf(".")))).ToString("D2");
        //Console.WriteLine(sDays);
        //---------------------------------------------------------------------------------------------------------------------------------------


        string sTimeB = sYearB.Remove(0, indexYear + 1);

        int iTime = Int32.Parse(sTimeB);
        int iHour = iTime / 60;
        int iMinut = iTime - (iHour * 60);

        string sHour = iHour.ToString("D2");
        string sMinut = iMinut.ToString("D2");

        //Console.WriteLine("Время: {0}:{1}", sHour, iMinut);




        return "от  "+ sDay + "."+ sMountE + "."+ sYearE + " "+ sHour + ":"+ sMinut + "";
    }
    
}

