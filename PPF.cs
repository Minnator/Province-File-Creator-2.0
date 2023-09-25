using System.Text;
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EU4_Province_Creator
{
    internal class PPF
    {
        public static string ParsePatternString(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success)
                return match.Groups[1].Value;   
            return "";
        }
        
        public static bool ParsePatternStringBool(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            if (!match.Success)
                return false;
            if (match.Groups[1].Value.Equals("no"))
                return false;
            if (match.Groups[1].Value.Equals("yes"))
                return true;
            return false;
        }
        public static string ParseCores(string input, string pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern);
            StringBuilder sb = new StringBuilder();
            foreach (var item in matches)
            {
                sb.Append(item).Append(",");
            }
            return sb.ToString();
        }
        public static string[] RemoveHistory(string[] lines)
        {
            string all = ConvertArrayToString(lines);
            Match match = Regex.Match(all, "(\\d{4}.[\\d{1,2}].\\d{1,2}(?:.*\\s*)*)");
            if (match.Success)
                return new string[] { all.Replace(match.Groups[1].Value, ""), match.Groups[1].Value };
            return new string[1] { all };
        }
        public static string ConvertArrayToString(string[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append(item.ToString()).Append(Environment.NewLine);
            }
            return sb.ToString();
        }
        public static string[] ConvertTextToArray(string text)
        {
            return text.Split(new[] { "\r\n" }, StringSplitOptions.None);
        }
        public static string GetFirstLineOfArray(string[] array)
        {
            if (array.Length > 0)
            return array[0];
            else return "";
        }
    }
}
