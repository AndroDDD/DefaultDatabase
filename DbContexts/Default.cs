using Microsoft.EntityFrameworkCore;
using DefaultDatabase.Models;

namespace DefaultDatabase.DbContexts;
public class DefaultContext: DbContext {
    public DefaultContext(DbContextOptions options): base(options) {}
    DbSet<Employee> Employees {
        get;
        set;
    }
    DbSet<Item> Items {
        get;
        set;
    }
    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Item>()
            .Property(e => e.ItemImages)
            .HasConversion(
                v => string.Join(",",v),
                v => v.Split(",",StringSplitOptions.RemoveEmptyEntries));
        modelBuilder.Entity<Item>().HasData(new Item { ItemId = 1, ItemName = "Test Item 1", ItemPrice = 1, ItemDescription = "Item 1 Description", ItemImages = new string[] { "image.jpg" } });
    }
    #endregion
}