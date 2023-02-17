using Contracts.DAL.Base;
using Contracts.Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF;

public class AppDbContext : DbContext
{
    private readonly IUserNameProvider _userNameProvider;
    public DbSet<Company> Company { get; set; } = default!;
    public DbSet<Location> Location { get; set; } = default!;
    public DbSet<Order> Order { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<PriceList> PriceList { get; set; } = default!;
    public DbSet<ProvidedRoute> ProvidedRoute { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
        : base(options)
    {
        _userNameProvider = userNameProvider;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var relationship in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
    
    private void SaveChangesMetadataUpdate()
    {
        // update the state of ef tracked objects
        ChangeTracker.DetectChanges();
            
        var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
        foreach (var entityEntry in markedAsAdded)
        {
            if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

            entityWithMetaData.CreatedAt = DateTime.Now;
            entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
            entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
            entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
        }

        var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (var entityEntry in markedAsModified)
        {
            // check for IDomainEntityMetadata
            if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

            entityWithMetaData.ChangedAt = DateTime.Now;
            entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

            // do not let changes on these properties get into generated db sentences - db keeps old values
            entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
            entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
        }
    }
    
    public override int SaveChanges()
    {
        SaveChangesMetadataUpdate();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SaveChangesMetadataUpdate();
        return base.SaveChangesAsync(cancellationToken);
    }
}