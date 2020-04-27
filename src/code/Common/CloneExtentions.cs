using Newtonsoft.Json;
using System.Linq;

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

        public static void CopyTo<T>(this T source, T target)
        {
            var type = typeof(T);

            foreach (var sourceProperty in type.GetProperties().Where(x => x.CanWrite))
            {
                var targetProperty = type.GetProperty(sourceProperty.Name);
                targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
            }

            foreach (var sourceField in type.GetFields())
            {
                var targetField = type.GetField(sourceField.Name);
                targetField.SetValue(target, sourceField.GetValue(source));
            }
        }
    }
}
