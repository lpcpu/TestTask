using TestTask.LogManager.Parser;
using TestTask.LogManager.Parser.Parsers;
using TestTask.LogManager.Utils;

namespace TestTask.LogManager
{
    public class LogStandartizationService
    {
        private List<ILogParser> parsers = new List<ILogParser>();
        private List<string> standatizedLogs = new List<string>(), invalidLogs = new List<string>();

        public LogStandartizationService()
        {
            parsers.Add(new Format1Parser());
            parsers.Add(new Format2Parser());
        }

        private void HandleLogLine(string line)
        {
            foreach (ILogParser parser in parsers)
            {
                if (parser.CanParse(line))
                {
                    string formattedLine = parser.Parse(line).ToStandartFormat();

                    standatizedLogs.Add(formattedLine);
                    return;
                }
            }

            invalidLogs.Add(line);
        }

        // на самом деле не думаю что это правильный подход..
        public void StandartizeLogsAndWriteToFile(string sourcePath, string formattedPath, string problemsPath)
        {
            standatizedLogs.Clear();
            invalidLogs.Clear();

            IEnumerable<string> logs = FileUtils.ReadFromFile(sourcePath);

            foreach (string line in logs)
            {
                HandleLogLine(line);
            }

            FileUtils.WriteToFile(formattedPath, standatizedLogs);
            FileUtils.WriteToFile(problemsPath, invalidLogs);
        }
    }
}
