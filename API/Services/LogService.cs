using API.Interfaces;
using NLog;


namespace API.Services;


public class LogService : ILogService
{
    private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
    private readonly string _logId;

    public LogService()
    {
        this._logId = Guid.NewGuid().ToString();
    }

    public void LogInfo(string message)
    {
        logger.Info("{@LogData}", new { LogId = _logId, Message = message });
    }

    public void LogWarn(string message)
    {
        logger.Warn("{@LogData}", new { LogId = _logId, Message = message });
    }

    public void LogDebug(string message)
    {
        logger.Debug("{@LogData}", new { LogId = _logId, Message = message });
    }

    public void LogError(string message)
    {
        logger.Error("{@LogData}", new { LogId = _logId, Message = message });
    }
    
    public void LogError(Exception ex)
    {
        LogException(ex, "An exception occurred.");
    }
    
    public void LogError(string message, Exception ex)
    {
        LogException(ex, message);
    }

    private void LogException(Exception ex, string message)
    {
        // Log the main exception
        logger.Error(ex, "{@LogData}", new { LogId = _logId, CustomMessage = message, ExceptionType = ex.GetType().Name });

        // Log inner exceptions in a loop
        Exception? innerEx = ex.InnerException;
        while (innerEx != null)
        {
            logger.Error(innerEx, "{@LogData}", new { LogId = _logId, CustomMessage = "Inner Exception", ExceptionType = innerEx.GetType().Name });
            innerEx = innerEx.InnerException;
        }
    }
}