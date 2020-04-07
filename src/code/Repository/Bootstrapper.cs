using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.Repository
{
    public static class Bootstrapper
    {
        public static async Task InitialiseAsync(string dbPath)
        {
            using (var context = new DatabaseContext(dbPath))
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}
