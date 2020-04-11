using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Resources
{
    public static class Reader
    {
        public async static Task<string> GetTextFileAsync(string manifest)
        {
            var assembly = typeof(Reader).Assembly;
            string[] names = assembly.GetManifestResourceNames();
            var resourceStream = assembly.GetManifestResourceStream(manifest);
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
