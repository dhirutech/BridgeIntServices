namespace BridgeIntServices.Core.Interfaces
{
    public interface IBridgeLogger
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogInfo(string message);
    }
}
