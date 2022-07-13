namespace Dmx.Net
{
    public interface IController : IDisposable
    {
        void Open();
        void Close();
        void SetChannel(int channel, byte value);
        void SetChannelRange(int startChannel, params byte[] values);    
        Task WriteSafe();
        Task WriteBuffer();
        void ClearBuffer();
    }
}