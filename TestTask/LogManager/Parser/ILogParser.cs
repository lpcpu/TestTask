using TestTask.LogManager.Entry;

namespace TestTask.LogManager.Parser
{
    public interface ILogParser
    {
        bool CanParse(string line);
        LogEntry Parse(string line);
    }
}
