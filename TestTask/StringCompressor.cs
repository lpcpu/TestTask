namespace TestTask
{
    // сделал с некоторыми оптимизациями и кешированием (просто от себя добавил)
    public class StringCompressor
    {
        private static Dictionary<string, string> _compressedCache = [];
        private static Dictionary<string, string> _decompressedCache = [];

        public static bool caching = false;

        private static int AppendCompressed(char[] buffer, int pos, char character, int charactersCount)
        {
            buffer[pos++] = character;
            if (charactersCount > 1)
            {
                char[] numberArray = charactersCount.ToString().ToCharArray();
                int numberArrayLength = numberArray.Length;

                Array.Copy(numberArray, 0, buffer, pos, numberArrayLength);

                pos += numberArrayLength;
            }
            return pos;
        }

        public static string Compress(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            int length = input.Length;

            if (length < 2)
                return input;

            if (caching && _compressedCache.TryGetValue(input, out var cached))
            {
                return cached;
            }

            char[] buffer = new char[length];
            int bufferPos = 0;

            int charCount = 1;
            char lastChar = input[0];

            for (int i = 1; i < length; i++)
            {
                if (input[i] == lastChar)
                {
                    charCount++;
                    continue;
                }

                bufferPos = AppendCompressed(buffer, bufferPos, lastChar, charCount);

                lastChar = input[i];
                charCount = 1;
            }

            AppendCompressed(buffer, bufferPos, lastChar, charCount);

            string compressed = new string(buffer);

            if (caching)
            {
                _compressedCache[input] = compressed;
                _decompressedCache[compressed] = input;
            }

            return compressed;
        }
        
        private static void AppendDecompressed(ref string decompressed, ref string lastCharCountBuffer, char lastChar)
        {
            if (lastCharCountBuffer == string.Empty)
            {
                decompressed += lastChar;
            }
            else
            {
                decompressed += new string(lastChar, int.Parse(lastCharCountBuffer));
                lastCharCountBuffer = string.Empty;
            }
        }

        public static string Decompress(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            int length = input.Length;

            if (length < 2)
                return input;

            if (caching && _decompressedCache.TryGetValue(input, out var cached))
            {
                return cached;
            }

            char lastChar = input[0];
            string lastCharCountBuffer = string.Empty;

            string decompressed = string.Empty;


            for (int i = 1; i < length; i++)
            {
                char c = input[i];

                if (char.IsDigit(c))
                {
                    lastCharCountBuffer += c;
                }
                else if (char.IsLetter(c))
                {
                    AppendDecompressed(ref decompressed, ref lastCharCountBuffer, lastChar);
                    lastChar = c;
                }
            }

            AppendDecompressed(ref decompressed, ref lastCharCountBuffer, lastChar);

            if (caching)
            {
                _decompressedCache[input] = decompressed;
                _compressedCache[decompressed] = input;
            }

            return decompressed;
        }
    }
}
