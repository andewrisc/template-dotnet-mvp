using System;

namespace API.Interfaces;

public interface ILogService
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
    void LogError(Exception ex);
    void LogError(string message, Exception ex);
}