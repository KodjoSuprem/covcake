using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace covCake
{
    public struct Duration
    {
        public int Jours{get;set;}
        public int Mois{get;set;}
        public int Anness{get;set;}
        public double Heures{get;set;}
        public TimeSpan timeSpan {get;set;}


        public override string ToString()
        {
 
            if (timeSpan.TotalDays == 0)
                return "O jour";
            string pluralize = (Jours > 1) ? "s" : "";
            string plurOnlyDays = (timeSpan.TotalDays > 1) ? "s" : "";


            if (timeSpan.TotalDays <= 31)
            {
                int nbWeeks = (Jours / 7);
                if (nbWeeks >= 1) // si plus de 7 jours
                {
                    int nbRestDays = (Jours % 7);
                    string weeksPlur = (nbWeeks > 1) ? "s" : "";
                    string weeks = "";

                    if (nbWeeks >= 1)
                        weeks += (Jours / 7).ToString() + " semaine" + weeksPlur;
                    if (nbRestDays > 1)
                        weeks += " et " + nbRestDays + " jours ";
                    return weeks;
                }
                return timeSpan.TotalDays + " jour" + plurOnlyDays;
            }

            string et = (Mois > 0 || Anness > 0) ? " et " : "";
            string jours = (Jours > 0) ? et + Jours + " jour" + pluralize : "";
            string mois = (Mois > 0) ? Mois + " mois " : "";
            pluralize = (Anness > 1) ? "s" : "";
            string annees = (Anness > 0) ? Anness + " an " + pluralize + ", " : "";
            return annees + mois + jours; 

        }
        [Obsolete]
        private string ToStringOsb()
        {
            
            if(timeSpan.TotalDays==0)
                return "O jour";
            string pluralize = (Jours > 1) ? "s" : "";
            string plurOnlyDays = (timeSpan.TotalDays >1)?"s":"";
          

            if(timeSpan.TotalDays <= 31)
                return timeSpan.TotalDays + " jour" + plurOnlyDays;

            string et = (Mois > 0 || Anness > 0) ? " et " : "";
            string jours  = (Jours > 0) ? et + Jours + " jour" + pluralize : "";
            string mois  = (Mois > 0) ? Mois + " mois " : "";
            pluralize = (Anness > 1) ? "s" : "";
            string annees  = (Anness > 0) ? Anness + " an " + pluralize+", " : "";
            return annees  + mois +  jours; 
        }
    }

    public class DateTimeException : Exception
    {
        public DateTimeException(): base()
        {
       
        }
        public DateTimeException(string msg) : base(msg)
        {

        }
    }



    public static class DateTimeExtension
    {

        public static string ToString(this DateTime? date)
        {
            return (date.HasValue) ? date.Value.ToString() : "";
        }

        public static string ToString(this DateTime? date,string format)
        {
            return (date.HasValue) ? date.Value.ToString(format) : "";
        }

        /// <summary>
        /// Renvoi une chaine vide si la date == null
        /// sinon ToShortDateString
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTime? date)
        {
            return (date.HasValue) ? date.Value.ToShortDateString() : "";
        }

        public static string GetMonthString(this DateTime date)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
        }
        public static string GetMonthString(this DateTime? date)
        {
            return (date.HasValue) ? date.Value.GetMonthString() : "";
        }


        /// <summary>
        /// Renvoi une chaine vide si la date == null
        /// sinon ToLongDateString
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToLongDateString(this DateTime? date)
        {
            return (date.HasValue) ? date.Value.ToLongDateString() : "";
        }

        public static Duration GetDuration(this DateTime date, DateTime toThisDate)
        {
            if(date.CompareTo(toThisDate) > 1)
                throw new DateTimeException("La date passée en paramettre ne peut être anterieur à la date de l'instance");
            if(date.CompareTo(toThisDate) == 0)
                return new Duration();

            TimeSpan time = toThisDate - date;

            int totaldays = (int)time.TotalDays;
            int years = (int)(totaldays / 365);
            int months = (int)((totaldays / 30) - (years * 12));
            int days = totaldays - (30 * months) - (years * 365);

            /*
            int years = toThisDate.Year - date.Year;
            int mois = toThisDate.Month - date.Month;
            if (mois < 0) mois = 12 - mois;
            int jours = toThisDate.Day - date.Day;
            int jourSub = DateTime.DaysInMonth(date.Year,date.Month);
            if (jours < 0) jours = jourSub - jours;
            */
            return new Duration
            {
                Anness = years,
                Mois = months,
                Jours = days,
                Heures = time.TotalHours,
                timeSpan = time
            };
        }


        public static string ToMedDateString(this DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }

        public static int GetAge(this DateTime dateNaissance)
        {
            // cache the current time
            DateTime now = DateTime.Today; // today is fine, don't need the timestamp from now
            // get the difference in years
            int years = now.Year - dateNaissance.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (now.Month < dateNaissance.Month || (now.Month == dateNaissance.Month && now.Day < dateNaissance.Day))
                --years;
            return years;

        }
    }
}
