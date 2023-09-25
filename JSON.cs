using System.Text.Json;

namespace EU4_Province_Creator
{
    internal class JSON
    {
        public class JsonParserWriter
        {
            public static string ToJson<T>(T obj)
            {
                return JsonSerializer.Serialize(obj);
            }

            public static T FromJson<T>(string json)
            {
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
