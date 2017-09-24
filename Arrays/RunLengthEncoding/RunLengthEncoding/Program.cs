using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunLengthEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding e = Encoding.GetEncoding(1252);
            Console.OutputEncoding = e;

            AsciiEncoder ae = new AsciiEncoder();
            string s = ae.Encode("a12BBBB");
            Console.WriteLine(s);
        }
    }

    // Assumptions for AsciiEncoder
    // string contains only Ascii set characters from 0 to 127
    // The maximum time a character repeats is 127
    // The length of compressed string shouldn't be bigger than original
    //
    class AsciiEncoder
    {
        public string Encode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            Encoding encoding = Encoding.GetEncoding(1252);
            StringBuilder sb = new StringBuilder();
            const int offset = 127;
            int count = 1;
            char ch = str[0];

            Action<int, char> appendFunc = (num, c) =>
            {
                if (num > 1)
                {
                    sb.Append(encoding.GetString(new byte[] { (byte)(offset + num) }));
                }
                sb.Append(c);
            };

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == ch)
                {
                    count++;
                    if (count > offset)
                    {
                        throw new ArgumentException(
                            string.Format("Expected max repetition to be {0}", offset));
                    }
                }
                else
                {
                    appendFunc(count, ch);

                    ch = str[i];
                    count = 1;
                }
            }

            appendFunc(count, ch);
            return sb.ToString();
        }
    }

    // Assumptions for BasicEncoder
    // string contains only alphabets [a-z]/[A-Z]
    // The maximum time a character repeats is 127
    //
    class BasicEncoder
    {
        public string Encode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            char c = str[0];
            int count = 1;

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    count++;
                }
                else
                {
                    sb.Append(count > 1 ? count.ToString() : string.Empty).Append(c);

                    c = str[i];
                    count = 1;
                }
            }

            sb.Append(count > 1 ? count.ToString() : string.Empty).Append(c);
            return sb.ToString();
        }

        public string Decode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            int count = 1;
            
            for (int i = 0; i < str.Length; )
            {
                if ( (str[i] >= 'a' && str[i] <= 'z') ||
                     (str[i] >= 'A' && str[i] <= 'Z') )
                {
                    sb.Append(str[i], count);
                    count = 1;
                    i++;
                }
                else
                {
                    int start = i;
                    while (i < str.Length && str[i] >= '0' && str[i] <= '9')
                    {
                        i++;
                    }

                    if (i == str.Length || i == start)
                    {
                        throw new ArgumentException("Input encoding is incorrect");
                    }

                    count = Int32.Parse(str.Substring(start, i - start));
                }
            }

            return sb.ToString();
        }
    }
}
