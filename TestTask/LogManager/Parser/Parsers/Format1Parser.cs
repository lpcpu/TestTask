using System.Text.RegularExpressions;
using TestTask.LogManager.Entry;

namespace TestTask.LogManager.Parser.Parsers
{
    public class Format1Parser : ILogParser
    {
        // регулярное выражение делал через https://regex101.com/
        private readonly Regex _regex = new Regex(@"(\d{2}\.\d{2}\.\d{4})\s(\d{2}:\d{2}:\d{2}\.\d{3})\s(INFO|INFORMATION|WARN|WARNING|ERROR|DEBUG)\s(.*)$");

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
            string message = match.Groups[4].Value;

            return new LogEntry(date, time, level, "DEFAULT", message);
        }
    }
}
