using Dmx.Net.Common;
using Dmx.Net.Controllers;

namespace Dmx.Net.SingleControllerSample
{
    internal class Program
    {
        private static DmxTimer? _timer;
        private static IController? _openDmx;
        private static float t = 0;
        private static List<(byte r, byte g, byte b)> colors = new()
        {
            new(255, 0, 0),
            new(0, 255, 0),
            new(0, 0, 255),
        };

        private static void Main()
        {
            Console.WriteLine("DMX.NET Single controller sample");
            Console.WriteLine();

            // Initialize synchronization timer
            _timer = new DmxTimer();
            _timer.BeforeUpdate += (s, e) =>
            {
                if (!_openDmx?.IsOpen ?? false)
                {
                    try
                    {
                        _openDmx?.Open(0);
                    }
                    catch (IOException)
                    {

                    }
                }

                var (r, g, b) = colors[(int)Math.Floor(t) % colors.Count];

                if (_openDmx is not null)
                {
                    // Set channel values before writing to controller
                    _openDmx.SetChannelRange(1, 255, r, g, b);
                }

                t += _timer!.Interval / 1000f;
            };

            // List supported controller types
            Console.WriteLine("Supported controller types:");
            foreach (var controller in ControllerManager.SupportedControllers)
            {
                Console.WriteLine($" > {controller.Name}");
            }
            Console.WriteLine();

            // Enumerate controllers
            Console.WriteLine("Available devices:");
            foreach (var device in ControllerManager.GetDevices())
            {
                Console.WriteLine($" > {device.Controller.Name} {device}");
            }
            Console.WriteLine();

            // Initialize controller
            _openDmx = ControllerManager.RegisterController<OpenDmxController>(1, _timer);
            _openDmx.Open(0);

            // Start the timer
            _timer.Start();

            // Wait for a key press
            Console.WriteLine("Press any key to stop the animation...");
            Console.ReadKey();

            // Dispose and close
            ControllerManager.UnregisterAll();
            _timer.Dispose();
        }
    }
}