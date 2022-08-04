# DMX.NET
![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/wberdowski/DMX.NET/.NET/master)
![GitHub](https://img.shields.io/github/license/wberdowski/DMX.NET)

DMX.NET is a compact library that makes it easy to communicate with your USB to DMX512 interfaces.
<!---It includes all necessary driver files, so you don't have to install them manually.--->

## Usage
Interraction with the library is mostly done through the ```ControllerManager```.
Additionally there is a build-in ```DmxTimer```, which is used to synchronize multiple controllers together.

**You can learn on real world examples by exploring projects in the [Samples](https://github.com/wberdowski/DMX.NET/tree/master/Samples/) directory.**

For the impatient here's the minimal code to get things working (you can find the whole project [here](https://github.com/wberdowski/DMX.NET/tree/master/Samples/Dmx.Net.MinimalSample):
```csharp
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
```

## Status
I'm a full-time programmer and maintain this project in my free time.
It's far from finished, but I'm doing my best to deliver regular updates.
Additional help is very much wanted, mainly in terms of adding support for more controller modules.
If you're interested in contributing to this project, please see **Contributing** section below.

## Roadmap
- Improve handling of USB disconnecting and reconnecting
- Art-Net support

## Contributing
Contributing to the project is very desireable.
I mostly encourage creating controller modules for other vendors. I physically don't have access to more USB interfaces, which makes debugging impossible.

**You learn on how to contribute to this project [here](CONTRIBUTING.md).**

## License
This project is under MIT license. You can find it in the [LICENSE](https://github.com/wberdowski/DMX.NET/blob/master/LICENSE) file.
