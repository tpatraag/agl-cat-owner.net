using NLog;
using System;

namespace AGL.CatOwner.Utility
{
    public class Logging
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void AuditLog(string message)
        {
            logger.Info(message);
        }

        public static void HandleException(Exception exception)
        {
            //log exception here..
            logger.Error(exception.InnerException + ":" + exception.StackTrace);
        }

        public static void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
