using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace ReservaVuelo_Speech_
{
    public class InputCheck
    {
        public string CheckOrigin (string origin)
        {
            if (origin == "") return "Su ubicación";
            else return origin;
        }


        public string CheckDate(string date)
        {
            string res = "";
            DateTime dateTime = DateTime.Now;

            switch (date)
            {
                case "":
                case "mañana":
                    res = dateTime.Day + 1 + "/" + dateTime.Month + "/" + dateTime.Year;
                    break;
                case "semana que viene":
                    res = dateTime.Day + 7 + "/" + dateTime.Month + "/" + dateTime.Year;
                    break;
                case "siguiente mes":
                    res = dateTime.Day + "/" + dateTime.AddMonths(1).Month + "/" + dateTime.Year;
                    break;
                default:
                    res = date + "/" + dateTime.Year;
                    if(!IsDate(res)) res = "Fecha no válida";
                    break;
            }
            return res;
        }

        public string CheckNumTickets(string n)
        {
            if (n == "") return "1";
            else return n;
        }

        private bool IsDate(object inValue)
        {
            bool bValid;
            try
            {
                DateTime myDT = DateTime.Parse(inValue.ToString());
                bValid = true;
            }
            catch (Exception e)
            {
                bValid = false;
            }

            return bValid;
        }
    }
}
