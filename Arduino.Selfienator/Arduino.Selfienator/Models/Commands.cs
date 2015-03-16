using Arduino.Selfienator.Core;
using System.Text;

namespace Arduino.Selfienator.Models
{
    public class Commands : ICommands
    {
        public string motor(double[] angle, int[] direction, int[] delay, char[] names)
        {
            if (angle.Length > 0 && direction.Length > 0 && delay.Length > 0 && names.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < angle.Length; i++)
                {
                    sb.AppendFormat("M{3}-{0}-{1}-{2},", angle[i], direction[i], delay[i], names[i]);
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(";");
                return sb.ToString();
            }
            return "";
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
            return sb.ToString();
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
