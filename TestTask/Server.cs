namespace TestTask
{
    public static class Server
    {
        private static int _count;
        private static int _activeWriters = 0;

        public static void AddToCount(int value)
        {
            Interlocked.Increment(ref _activeWriters);

            Interlocked.Add(ref _count, value);

            Interlocked.Decrement(ref _activeWriters);
        }

        // как я понял суть задачи нужно чтобы пока в _count пишется, тут не могли получить к ней доступ
        public static int GetCount()
        {
            SpinWait.SpinUntil(() => Volatile.Read(ref _activeWriters) == 0);

            return Volatile.Read(ref _count);
        }
    }
}
