using System.IO;

namespace aoc2019.Test
{
    public class LogOutputHelper
    {
        private readonly int _day;
        private string Path => $"C:\\Code\\aoc2019\\Log\\log{_day:00}.txt";

        public LogOutputHelper(int day)
        {
            _day = day;
            Delete();
        }

        public void Delete()
        {
            if (File.Exists(Path))
                File.Delete(Path);
        }

        public void WriteLine(string text)
        {
            File.AppendAllText(Path, text);
        }
    }
}