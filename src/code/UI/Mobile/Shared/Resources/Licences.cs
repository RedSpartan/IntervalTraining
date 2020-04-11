using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Resources
{
    public static class Licences
    {
        public async static Task<string> GetAsync(string name)
        {
            var json = await Reader.GetTextFileAsync("RedSpartan.IntervalTraining.UI.Mobile.Shared.licence.usr");
            var obj = JObject.Parse(json);

            return obj.Value<string>(name);
        }
    }
}
