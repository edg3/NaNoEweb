using System.Collections.Generic;
using System.Text;

namespace NaNoEweb.Data
{

    public static class DBInterop
    {
        public static int Port { get => 11223; }
        public static string[] SeparatingStrings = { "\r\n", "\0" };

        public static byte[] StringConvertToByte(string msg)
        {
            return Encoding.ASCII.GetBytes(msg);
        }

        public static string ByteConvertToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static string ConvertToSafeString(string input)
        {
            return input.Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "''").Replace(",", "\\,");
        }

        public static string ConvertFromSafeString(string intput)
        {
            return intput.Replace("\\r", "\r").Replace("\\n", "\n").Replace("''", "'").Replace("\\,",",");
        }

        public static string[] ExplodeNetworkString(string input)
        {
            Dictionary<int, string> parts = new Dictionary<int, string>();
            string currentOp = "";
            bool ignoreChar = false;
            for (int i = 0; i < input.Length; ++i)
            {
                if (ignoreChar)
                {
                    currentOp += input[i];
                    ignoreChar = false;
                }
                else
                {
                    if (input[i] == ',')
                    {
                        parts.Add(parts.Count, currentOp);
                        currentOp = "";
                    }
                    else
                    {
                        currentOp += input[i];
                        if (input[i] == '\\') ignoreChar = true;
                    }
                }
            }
            if (currentOp.Length > 0) parts.Add(parts.Count, currentOp);

            string[] answer = new string[parts.Count];
            for (int i = 0; i < parts.Count; ++i)
            {
                answer[i] = parts[i];
            }
            return answer;
        }
    }

}