using System.Text;

namespace TestTask.LogManager.Utils
{
    public class FileUtils
    {
        public static IEnumerable<string> ReadFromFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);

            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                yield return line;
            }

            streamReader.Dispose();
            fileStream.Dispose(); // как по мне явный вызов Dispose лучше, чем использование using (в плане читабельности кода)
        }

        public static void WriteToFile(string path, IEnumerable<string> lines)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);

            foreach (var line in lines)
            {
                streamWriter.WriteLine(line);
            }

            streamWriter.Dispose();
            fileStream.Dispose();
        }
    }
}
