using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Reader> Readers { get; set; } = null!;
    public DbSet<News> NewsItems { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Reaction> Reactions { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<News>().Property(p => p.Published).HasDefaultValue(false);
    // modelBuilder.Entity<News>().Property(p => p.PublicationDateTime).HasDefaultValue("GETDATE()");
    // modelBuilder.Entity<Comment>().Property(c => c.DatePublished).HasDefaultValueSql("GETDATE()");
    }
}