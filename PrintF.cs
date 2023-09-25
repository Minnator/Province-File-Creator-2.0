using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EU4_Province_Creator
{
    internal static class PrintF
    {
        public static void PrintListNewLines<T>(List<T> list)
        {
            Debug.WriteLine($"{nameof(list)}; length: {list.Count}");
            foreach (var item in list)
            {
                Debug.WriteLine($"{item}; ");
            }
        }
        public static string PrintListNewLinesS<T>(List<T> list)
        {
            StringBuilder sb = new ();
            //sb.AppendLine($"{nameof(list)}; length: {list.Count}");
            foreach (var item in list)
            {
                sb.AppendLine($"{item}");
            }
            return sb.ToString ();
        }
        public static string DebugPrintListNewLinesS<T>(List<T> list)
        {
            StringBuilder sb = new ();
            sb.AppendLine($"{nameof(list)}; length: {list.Count}");
            foreach (var item in list)
            {
                sb.AppendLine($"{item}");
            }
            return sb.ToString ();
        }
        public static string WriteListNewLines(List<Modifier> list)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{nameof(list)}; length: {list.Count}");
            foreach (var item in list)
            {
                sb.AppendLine(item.ToStringFancy());
            }
            return sb.ToString();
        }
        public static string PrintDictionarySpacedTitle<TKey, TValue>(string title, Dictionary<TKey, TValue> dictionary)
            where TKey : notnull
        {
            foreach (var kvp in dictionary)
            {
                if (kvp.Key == null || kvp.Value == null)
                {
                    return "Null value in Dictionary"; // Exit the loop as soon as a null key or value is found
                }
            }
            if (dictionary == null)
            {
                Debug.WriteLine("Dictionary is null.");
                return "";
            }
            StringBuilder sb = new (); var maxKeyLength = dictionary.Any() ? dictionary.Keys.Max(key => key?.ToString()?.Length ?? 0) + 1 : 0;

            sb.AppendLine($"{title}: ");
            foreach (var output in from kvp in dictionary let key = kvp.Key?.ToString() ?? "" let value = kvp.Value?.ToString() ?? "" select $"{key.PadRight(maxKeyLength)}: {value}")
            {
                sb.AppendLine(output);
            }

            return sb.ToString();
        }
        public static string PrintDictionaryS<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
            where TKey : notnull
        {
            if (dictionary == null)
            {
                return "Empty Dictionary";
            }

            StringBuilder sb = new();
            //var maxKeyLength = dictionary.Keys.Max(key => key?.ToString()?.Length ?? 0) + 1;
            foreach (var output in dictionary) //from kvp in dictionary let key = kvp.Key?.ToString() ?? "" let value = kvp.Value?.ToString() ?? "" select $"{key.PadRight(maxKeyLength)}: {value}"
            {
                sb.AppendLine(output.Key + " - " + output.Value.ToString());
            }
            return sb.ToString();
        }
        public static string ArrayToString(IEnumerable<string> arr)
        {
            StringBuilder sb = new ();
            foreach (var str in arr)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        public static string RulerToString()
        {
            var sb = new StringBuilder();
            foreach (var item in GlobalVars.formattedLeaders)
            {
                sb.AppendLine($"{item.Key} - {item.Value}");
            }
            return sb.ToString();
        }

        public static string HistoryToCheck(IEnumerable<string> array)
        {
            StringBuilder sb = new();
            foreach (var str in array)
            {
                sb.AppendLine($"-- start --\n {str} \n-- end --");
            }
            return sb.ToString();
        }
    }
}
