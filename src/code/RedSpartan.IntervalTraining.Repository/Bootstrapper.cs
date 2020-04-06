using Microsoft.EntityFrameworkCore;

namespace RedSpartan.IntervalTraining.Repository
{
    public static class Bootstrapper
    {
        public static void Initialise(string dbPath)
        {
            using (var context = new DatabaseContext(dbPath))
            {
                context.Database.Migrate();
            }
        }
    }
}
