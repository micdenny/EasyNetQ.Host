using System;
using Castle.Core.Logging;

namespace EasyNetQ.Host.Service.Logging
{
    public class CastleLoggingEasyNetQLogger : IEasyNetQLogger
    {
        private readonly ILogger _logger;

        public CastleLoggingEasyNetQLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void DebugWrite(string format, params object[] args)
        {
            if (args.Length > 0)
                _logger.DebugFormat(format, args);
            else
                _logger.Debug(format);
        }

        public void InfoWrite(string format, params object[] args)
        {
            if (args.Length > 0)
                _logger.InfoFormat(format, args);
            else
                _logger.Info(format);
        }

        public void ErrorWrite(string format, params object[] args)
        {
            if (args.Length > 0)
                _logger.ErrorFormat(format, args);
            else
                _logger.Error(format);
        }

        public void ErrorWrite(Exception exception)
        {
            _logger.Error(exception.Message, exception);
        }
    }
}
