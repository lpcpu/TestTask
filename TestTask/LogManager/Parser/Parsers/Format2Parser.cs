using System.Text.RegularExpressions;
using TestTask.LogManager.Entry;

namespace TestTask.LogManager.Parser.Parsers
{
    public class Format2Parser : ILogParser
    {
        // регулярное выражение делал через https://regex101.com/
        private readonly Regex _regex = new Regex(@"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}\.\d{4})\|(INFO|INFORMATION|WARN|WARNING|ERROR|DEBUG)\|\d+\|([\w.]+)\|(.*)$");

        public bool CanParse(string line)
        {
            return _regex.IsMatch(line);
        }

        public LogEntry Parse(string line)
        {
            Match match = _regex.Match(line);

            DateOnly date = DateOnly.Parse(match.Groups[1].Value);
            TimeOnly time = TimeOnly.Parse(match.Groups[2].Value);

            string level = match.Groups[3].Value;
            string calledMethod = match.Groups[4].Value;
            string message = match.Groups[5].Value;

            return new LogEntry(date, time, level, calledMethod, message);
        }
    }
}
