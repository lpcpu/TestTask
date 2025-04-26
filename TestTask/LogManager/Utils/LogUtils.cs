namespace TestTask.LogManager.Utils
{
    internal class LogUtils
    {
        //private static Dictionary<string, string> logLevel = new()
        //{
        //    { "INFORMATION", "INFO" },
        //    { "WARNING", "WARN" },
        //    { "ERROR", "ERROR" },
        //    { "DEBUG", "DEBUG" },
        //};


        // можно использовать тут вариант с Dictionary но для 2 строк это бесполезно
        public static string GetFormattedLogLevel(string level)
        {
            return level switch
            {
                "INFORMATION" => "INFO",
                "WARNING" => "WARN",
                _ => level,
            };

            //if (logLevel.TryGetValue(level, out string? value))
            //{
            //    return value;
            //}
            //return level;
        }
    }
}
