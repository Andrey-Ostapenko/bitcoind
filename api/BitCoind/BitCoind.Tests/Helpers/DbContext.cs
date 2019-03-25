using BitCoind.Database;
using Microsoft.EntityFrameworkCore;

namespace BitCoind.Tests.Helpers
{
    public class DbContext
    {
        public static readonly string _connection =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\.projects\\andrey\\bitcoind\\bitcoind\\api\\BitCoind\\BitCoind.Database\\BitCoindDatabase.mdf;Integrated Security=True";

        public DbContext()
        {
            DbContextOptions = new DbContextOptionsBuilder<BitcoindDbContext>()
                .UseSqlServer(_connection)
                .Options;
        }

        public DbContextOptions<BitcoindDbContext> DbContextOptions { get; set; }
    }
}