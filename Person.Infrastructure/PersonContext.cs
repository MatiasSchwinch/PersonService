using Microsoft.EntityFrameworkCore;
using Person.Domain.PersonAggregate;
using Person.Domain.SeedWork;

namespace Person.Infrastructure
{
    public partial class PersonContext : DbContext, IUnitOfWork
    {
        public virtual DbSet<PersonEntity> BasicData { get; set; } = null!;
        public virtual DbSet<Coordinate> Coordinates { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<Registered> Registereds { get; set; } = null!;
        public virtual DbSet<Timezone> Timezones { get; set; } = null!;

        public PersonContext() { }

        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.HasIndex(e => e.LocationId, "IX_BasicData_LocationID")
                    .IsUnique();

                entity.HasIndex(e => e.LoginId, "IX_BasicData_LoginID")
                    .IsUnique();

                entity.HasIndex(e => e.PictureId, "IX_BasicData_PictureID")
                    .IsUnique();

                entity.HasIndex(e => e.RegisteredId, "IX_BasicData_RegisteredID")
                    .IsUnique();

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Cell).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(320);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Nationality).HasMaxLength(4);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.PictureId).HasColumnName("PictureID");

                entity.Property(e => e.RegisteredId).HasColumnName("RegisteredID");

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<PersonEntity>(d => d.LocationId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Login)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<PersonEntity>(d => d.LoginId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Picture)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<PersonEntity>(d => d.PictureId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Registered)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<PersonEntity>(d => d.RegisteredId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Coordinate>(entity =>
            {
                entity.HasKey(e => e.CoordinatesId);

                entity.Property(e => e.CoordinatesId).HasColumnName("CoordinatesID");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.HasIndex(e => e.CoordinatesId, "IX_Location_CoordinatesID")
                    .IsUnique();

                entity.HasIndex(e => e.TimezoneId, "IX_Location_TimezoneID")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.City).HasMaxLength(60);

                entity.Property(e => e.CoordinatesId).HasColumnName("CoordinatesID");

                entity.Property(e => e.Country).HasMaxLength(60);

                entity.Property(e => e.Postcode).HasMaxLength(30);

                entity.Property(e => e.State).HasMaxLength(60);

                entity.Property(e => e.StreetName).HasMaxLength(90);

                entity.Property(e => e.TimezoneId).HasColumnName("TimezoneID");

                entity.HasOne(d => d.Coordinates)
                    .WithOne(p => p.Location)
                    .HasForeignKey<Location>(d => d.CoordinatesId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Timezone)
                    .WithOne(p => p.Location)
                    .HasForeignKey<Location>(d => d.TimezoneId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Md5).HasMaxLength(32);

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.Salt).HasMaxLength(10);

                entity.Property(e => e.Sha1).HasMaxLength(40);

                entity.Property(e => e.Sha256).HasMaxLength(64);

                entity.Property(e => e.Username).HasMaxLength(25);

                entity.Property(e => e.Uuid).HasMaxLength(36);
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("Picture");

                entity.Property(e => e.PictureId).HasColumnName("PictureID");

                entity.Property(e => e.Large).HasMaxLength(60);

                entity.Property(e => e.Medium).HasMaxLength(60);

                entity.Property(e => e.Thumbnail).HasMaxLength(60);
            });

            modelBuilder.Entity<Registered>(entity =>
            {
                entity.ToTable("Registered");

                entity.Property(e => e.RegisteredId).HasColumnName("RegisteredID");
            });

            modelBuilder.Entity<Timezone>(entity =>
            {
                entity.ToTable("Timezone");

                entity.Property(e => e.TimezoneId).HasColumnName("TimezoneID");

                entity.Property(e => e.Description).HasMaxLength(120);

                entity.Property(e => e.Offset).HasMaxLength(6);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
