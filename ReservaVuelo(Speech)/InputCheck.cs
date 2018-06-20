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
            if (origin == "") return "Tu ubicación";
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
                    if (!IsDate(res)) res = "Fecha no válida";
                    else res = date + "/" + GetCorrectYear(res);
                    break;
            }
            return res;
        }

        public string CheckNumTickets(string n)
        {
            if (n == "") return "1";
            else return n;
        }

        public string CheckVoiceText(string voice)
        {
            var text = "";
            if (voice[0] == '¿')
                text = "Has dicho: \"" + voice[0] + voice[1].ToString().ToUpper() + voice.Substring(2) + "?\"";
            else
                text = "Has dicho: \"" + voice[0].ToString().ToUpper() + voice.Substring(1) + "\"";
            return text;
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

        private string GetCorrectYear(string inValue)
        {
            DateTime myDT = DateTime.Parse(inValue);
            DateTime todayDT = DateTime.Now.Date;
            if (myDT.Subtract(todayDT) < TimeSpan.Zero)
                return todayDT.AddYears(1).Year.ToString();
            else
                return todayDT.Year.ToString();
        }
    }
}
