using Dmx.Net.Controllers;

namespace Dmx.Net.Demo
{
    internal class Program
    {
        private static IController udmx, openDmx;
        private static float t = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("DMX.Net Demo Program");

            // Initialize synchronization timer
            var timer = new DmxTimer();
            timer.BeforeUpdate += Timer_BeforeUpdate;

            // Initialize controllers
            udmx = new UDmxController();
            udmx.Open();

            openDmx = new OpenDmxController();
            openDmx.Open();

            // Start the timer
            timer.Start();

            Console.WriteLine("Press any key to stop the animation...");
            Console.ReadKey();

            // Dispose and close
            timer.Dispose();
            udmx.Dispose();
            openDmx.Dispose();
        }

        private static void Timer_BeforeUpdate(object? sender, EventArgs e)
        {
            t += 0.05f;

            var r = (byte)(Math.Sin(Math.Max(0, Math.Min(t % (3 * Math.PI), 2 * Math.PI)) / 2f) * 255);
            var g = (byte)(Math.Sin(Math.Max(0, Math.Min((t - Math.PI) % (3 * Math.PI), 2 * Math.PI)) / 2f) * 255);
            var b = (byte)(Math.Sin(Math.Max(0, Math.Min((t - 2 * Math.PI) % (3 * Math.PI), 2 * Math.PI)) / 2f) * 255);

            udmx.SetChannelRange(1, 255, r, g, b);
            openDmx.SetChannelRange(1, 255, r, g, b);

            udmx.WriteSafe();
            openDmx.WriteSafe();
        }
    }
}