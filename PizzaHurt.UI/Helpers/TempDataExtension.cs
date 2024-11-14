using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace PizzaHurt.UI.Helpers
{
    public static class TempDataExtension
    {
        // Set method: Serializes and stores an object in TempData with a specified key.
        public static void Set<T>(this ITempDataDictionary tempdata, string key, T value) where T : class
        {
            tempdata[key] = JsonSerializer.Serialize(value);
        }

        // Get method: Retrieves and deserializes an object from TempData with a specified key.
        public static T Get<T>(this ITempDataDictionary tempdata, string key) where T : class
        {
            tempdata.TryGetValue(key, out object o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }

        // Peek method: Retrieves and deserializes an object from TempData with a specified key without removing it from TempData.
        public static T Peek<T>(this ITempDataDictionary tempdata, string key) where T : class
        {
            object o = tempdata.Peek(key);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
    }
}
