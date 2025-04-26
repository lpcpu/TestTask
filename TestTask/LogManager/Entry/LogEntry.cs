using TestTask.LogManager.Utils;

namespace TestTask.LogManager.Entry
{
    public class LogEntry(DateOnly date, TimeOnly time, string level, string calledMethod, string message)
    {
        private readonly DateOnly _date = date;
        private readonly TimeOnly _time = time; // тут вместо TimeOnly можно было использовать просто строку так как никаких преобразований со временем не происходит
        private readonly string _level = level, _calledMethod = calledMethod, _message = message;

        public string ToStandartFormat()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                _date.ToString("yyyy-dd-MM"),
                _time.ToString("HH:mm:ss.FFFFF"),
                LogUtils.GetFormattedLogLevel(_level),
                _calledMethod, _message);
        }
    }
}
