using Dmx.Net.Common;
using Dmx.Net.Controllers;

var timer = new DmxTimer();
var controller = new OpenDmxController(timer);

// Make sure your Open DMX interface is connected!
controller.Open(0);

// Set values of the channel range (1-4)
controller.SetChannelRange(1, 255, 255, 255, 255);

// Don't forget to start the timer.
timer.Start();

// Keep console open
Console.ReadKey();

// Cleanup, ensure all channels are set to 0
timer.Dispose();
controller.Dispose();