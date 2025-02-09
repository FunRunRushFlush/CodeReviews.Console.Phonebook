using Microsoft.EntityFrameworkCore;
using Phonebook.FunRunRushFlush.Data.Model;

namespace Phonebook.FunRunRushFlush.Data.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<PhonebookTable> PhonebookTable { get; set; }
}