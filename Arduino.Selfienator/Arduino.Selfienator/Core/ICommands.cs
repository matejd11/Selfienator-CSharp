namespace Arduino.Selfienator.Core
{
    public interface ICommands
    {
         string motor(double[] angle, int[] direction, int[] delay, char[] names);
         string motorX(double angle, int direction, int delay);
         string motorY(double angle, int direction, int delay);
         string focus();
         string shot();
         string focusAndShot();
    }
}
