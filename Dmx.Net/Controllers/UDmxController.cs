using Dmx.Net.Attributes;
using Dmx.Net.Common;
using System.Runtime.InteropServices;

namespace Dmx.Net.Controllers
{
    [Experimental]
    [Controller("uDMX")]
    public class UDmxController : ControllerBase
    {
        public new bool IsOpen => Connected();

        private const string DllName = $"{Config.LibPath}/uDMX";

        #region INTEROP

        [DllImport(DllName, EntryPoint = "ChannelSet", CallingConvention = CallingConvention.StdCall)]
        public static extern bool ChannelSet(uint channel, uint value);

        [DllImport(DllName, EntryPoint = "ChannelsSet", CallingConvention = CallingConvention.StdCall)]
        public static extern bool ChannelsSet(uint channelCnt, uint channel, byte[] value);

        [DllImport(DllName, EntryPoint = "Configure", CallingConvention = CallingConvention.StdCall)]
        public static extern bool Configure();

        [DllImport(DllName, EntryPoint = "Connected", CallingConvention = CallingConvention.StdCall)]
        public static extern bool Connected();

        #endregion

        public UDmxController()
        {

        }

        public UDmxController(DmxTimer timer) : base(timer)
        {

        }

        public override async Task WriteBuffer()
        {
            if (IsOpen && !IsDisposed)
            {
                ChannelsSet(512, 1, writeBuffer);
            }

            await Task.CompletedTask;
        }

        public static new IEnumerable<Device> GetDevices()
        {
            if (Connected())
            {
                yield return new Device(
                    new ControllerInfo(typeof(UDmxController)),
                    0,
                    null,
                    "uDMX USB interface"
                );
            }
        }
    }
}
