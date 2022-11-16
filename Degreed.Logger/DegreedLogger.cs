using NLog;

namespace Degreed.Logger
{
    public class DegreedLogger : IDegreedLogger
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
