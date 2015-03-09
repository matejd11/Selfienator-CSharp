using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Selfienator.Models
{
    public class Commands : ICommands
    {
        public string motor(double[] angle, int[] direction, int[] delay, char[] names)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < angle.Length; i++)
            {
                sb.AppendFormat("M{3}-{0}-{1}-{2},", angle[i], direction[i], delay[i], names[i]);
            }
            sb.Remove(sb.Length-1, 1);
            sb.Append(";");
            return sb.ToString();
        }
        public string motorX(double angle, int direction, int delay = 5)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("MX-{0}-{1}-{2}", angle, direction, delay);

            sb.Append(";");
            return sb.ToString();
        }

        public string motorY(double angle, int direction, int delay = 5)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("MY-{0}-{1}-{2}", angle, direction, delay);

            sb.Append(";");
            return sb.ToString(); ;
        }

        public string focus()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("F");

            sb.Append(";");
            return sb.ToString();
        }

        public string shot()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("S");

            sb.Append(";");
            return sb.ToString();
        }

        public string focusAndShot()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("F,S");

            sb.Append(";");
            return sb.ToString();
        }
    }
}
