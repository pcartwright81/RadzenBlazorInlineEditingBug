namespace RadzenBlazorInlineEditingBug.Data;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class MyContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "SuperCoolDb");
        optionsBuilder.EnableSensitiveDataLogging(true);
        optionsBuilder.EnableDetailedErrors();
    }

    public DbSet<MyMainObject> Objects { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyMainObject>()
            .HasKey(a => a.Id);
        var dataToInsert = new List<MyMainObject>();
        for (int i = 1; i < 1000; i++)
        {
            var categoryId = i % 2 == 0 ? 1 : 2;
            var newObject = new MyMainObject { Description = $"{i}_Description", Name = $"{i}_Name", Id = i, CategoryId = categoryId };
            dataToInsert.Add(newObject);
        }
        modelBuilder.Entity<MyMainObject>().HasData(dataToInsert);

        modelBuilder.Entity<Category>().HasData([new Category { Id = 1, Name = "C1" }, new Category { Id = 2, Name = "C2" }]);
    }
}
