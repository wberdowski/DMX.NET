using System.Runtime.InteropServices;

namespace Dmx.Net.Controllers
{
    public class UDmxController : ControllerBase
    {
        private const string DLL_NAME = "uDMX";

        #region INTEROP

        [DllImport(DLL_NAME, EntryPoint = "ChannelSet", CallingConvention = CallingConvention.StdCall)]
        public static extern bool ChannelSet(uint channel, uint value);

        [DllImport(DLL_NAME, EntryPoint = "ChannelsSet", CallingConvention = CallingConvention.StdCall)]
        public static extern bool ChannelsSet(uint channelCnt, uint channel, byte[] value);

        [DllImport(DLL_NAME, EntryPoint = "Configure", CallingConvention = CallingConvention.StdCall)]
        public static extern bool Configure();

        [DllImport(DLL_NAME, EntryPoint = "Connected", CallingConvention = CallingConvention.StdCall)]
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
    }
}
