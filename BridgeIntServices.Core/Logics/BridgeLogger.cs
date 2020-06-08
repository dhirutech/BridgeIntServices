using BridgeIntServices.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace BridgeIntServices.Core.Logics
{
    public class BridgeLogger : IBridgeLogger
    {
        private readonly ILogger<BridgeLogger> _log;
        public BridgeLogger(ILogger<BridgeLogger> log)
        {
            _log = log;
        }

        public void LogDebug(string message)
        {
            _log.LogDebug(message);
        }

        public void LogError(string message)
        {
            _log.LogError(message);
        }

        public void LogInfo(string message)
        {
            _log.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _log.LogWarning(message);
        }
    }
}
