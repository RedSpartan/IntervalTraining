using Microsoft.EntityFrameworkCore;
using RedSpartan.IntervalTraining.Internal.Repository.Access;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.Repository
{
    public static class Bootstrapper
    {
        public static async Task InitialiseAsync(string dbPath)
        {
            using (var context = new DataContext(dbPath))
            {
                //await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
    }
}
