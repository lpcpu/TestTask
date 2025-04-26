using System.Diagnostics;
using System.Text;
using TestTask.LogManager;
using TestTask.LogManager.Parser.Parsers;
using TestTask.LogManager.Utils;

namespace TestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine(StringCompressor.Compress("hfdjhuuuuuqwoiqndddcdc"));
            Console.WriteLine(StringCompressor.Decompress("aw5yt2"));


            int i = 10;
            while (i-- > 0)
            {
                Task.Run(() => Server.AddToCount(33));
            }

            Console.WriteLine(Server.GetCount());

            LogStandartizationService logStandartizationService = new LogStandartizationService();
            logStandartizationService.StandartizeLogsAndWriteToFile(@"C:\Users\W\Downloads\fsdfdsf.txt", @"C:\Users\W\Downloads\formatted.txt", @"C:\Users\W\Downloads\problems.txt");
        }
    }
}
