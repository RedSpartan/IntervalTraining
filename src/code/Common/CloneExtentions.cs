using Newtonsoft.Json;

namespace RedSpartan.IntervalTraining
{
    public static class Extentions
    {
        public static T Clone<T>(this T source)
        {
            if (source == null)
            {
                return default;
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}
