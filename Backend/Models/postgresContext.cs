using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Relmonitor.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aplicatii> Aplicatii { get; set; }
        public virtual DbSet<Detaliirelease> Detaliirelease { get; set; }
        public virtual DbSet<Duraterelease> Duraterelease { get; set; }
        public virtual DbSet<Impulse> Impulse { get; set; }
        public virtual DbSet<Medii> Medii { get; set; }
        public virtual DbSet<Release> Release { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Utilizatori> Utilizatori { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=afaceri1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United Kingdom.1252");

            modelBuilder.Entity<Aplicatii>(entity =>
            {
                entity.HasKey(e => e.Idaplicatie)
                    .HasName("aplicatii_pkey");

                entity.ToTable("aplicatii", "relmonitor");

                entity.Property(e => e.Idaplicatie).HasColumnName("idaplicatie");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Denumire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("denumire");

                entity.Property(e => e.Emails)
                    .HasMaxLength(200)
                    .HasColumnName("emails");

                entity.Property(e => e.Managerproiect)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("managerproiect");

                entity.Property(e => e.Ownerproiect)
                    .HasMaxLength(100)
                    .HasColumnName("ownerproiect");
                entity.Property(e => e.Codaplicatie)
                    .HasMaxLength(100)
                    .HasColumnName("codaplicatie");
            });

            modelBuilder.Entity<Detaliirelease>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("detaliirelease", "relmonitor");

                entity.Property(e => e.Brvpath)
                    .HasMaxLength(1000)
                    .HasColumnName("brvpath");

                entity.Property(e => e.Bugs).HasColumnName("bugs");

                entity.Property(e => e.Contentrelease)
                    .HasMaxLength(1000)
                    .HasColumnName("contentrelease");

                entity.Property(e => e.Creator)
                    .HasMaxLength(150)
                    .HasColumnName("creator");
                entity.Property(e => e.Idrelease).HasColumnName("idrelease"); 

                entity.Property(e => e.Idaplicatie).HasColumnName("idaplicatie");

                entity.Property(e => e.Iddurata).HasColumnName("iddurata");

                entity.Property(e => e.Idmediu).HasColumnName("idmediu");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.Property(e => e.Idutilizator).HasColumnName("idutilizator");

                entity.Property(e => e.Dataend).HasColumnName("dataend");

                entity.Property(e => e.Datarelease).HasColumnName("datarelease");
                entity.Property(e => e.Relstatus).HasColumnName("relstatus");

                entity.Property(e => e.Datastart).HasColumnName("datastart");

                entity.Property(e => e.Denumireaplicatie)
                    .HasMaxLength(100)
                    .HasColumnName("denumireaplicatie");
                entity.Property(e => e.Codaplicatie)
                  .HasMaxLength(100)
                  .HasColumnName("codaplicatie");

                entity.Property(e => e.Denumiremediu)
                    .HasMaxLength(50)
                    .HasColumnName("denumiremediu");

                entity.Property(e => e.Denumirestatus)
                    .HasMaxLength(100)
                    .HasColumnName("denumirestatus");

                entity.Property(e => e.Downtime).HasColumnName("downtime");

                entity.Property(e => e.Duratarelease).HasColumnName("duratarelease");

                entity.Property(e => e.Esteurgenta).HasColumnName("esteurgenta");

                entity.Property(e => e.Idimpulse)
                    .HasMaxLength(300)
                    .HasColumnName("idimpulse");

                entity.Property(e => e.Imbunatatiri).HasColumnName("imbunatatiri");

                entity.Property(e => e.Luna)
                    .HasMaxLength(50)
                    .HasColumnName("luna");

                entity.Property(e => e.Saptamana)
                    .HasMaxLength(50)
                    .HasColumnName("saptamana");

                entity.Property(e => e.Sysidimpulse)
                    .HasMaxLength(200)
                    .HasColumnName("sysidimpulse");

                entity.Property(e => e.Testpath)
                    .HasMaxLength(1000)
                    .HasColumnName("testpath");
            });

            modelBuilder.Entity<Duraterelease>(entity =>
            {
                entity.HasKey(e => e.Iddurata)
                    .HasName("duraterelease_pkey");

                entity.ToTable("duraterelease", "relmonitor");

                entity.Property(e => e.Iddurata).HasColumnName("iddurata");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Dataend).HasColumnName("dataend");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Datarelease).HasColumnName("datarelease");

                entity.Property(e => e.Datastart).HasColumnName("datastart");

                entity.Property(e => e.Downtime).HasColumnName("downtime");

                entity.Property(e => e.Durata).HasColumnName("durata");

                entity.Property(e => e.Luna)
                    .HasMaxLength(50)
                    .HasColumnName("luna");

                entity.Property(e => e.Saptamana)
                    .HasMaxLength(50)
                    .HasColumnName("saptamana");
            });

            modelBuilder.Entity<Impulse>(entity =>
            {
                entity.HasKey(e => e.Idrelease)
                    .HasName("impulse_pkey");

                entity.ToTable("impulse", "relmonitor");

                entity.Property(e => e.Idrelease)
                    .ValueGeneratedNever()
                    .HasColumnName("idrelease");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Idimpulse)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("idimpulse");

                entity.Property(e => e.Idtask)
                    .HasMaxLength(200)
                    .HasColumnName("idtask");

                entity.Property(e => e.Sysidimpulse)
                    .HasMaxLength(200)
                    .HasColumnName("sysidimpulse");

                entity.HasOne(d => d.IdreleaseNavigation)
                    .WithOne(p => p.Impulse)
                    .HasForeignKey<Impulse>(d => d.Idrelease)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("impulse_idrelease_fkey");
            });

            modelBuilder.Entity<Medii>(entity =>
            {
                entity.HasKey(e => e.Idmediu)
                    .HasName("mediu_pkey");

                entity.ToTable("medii", "relmonitor");

                entity.Property(e => e.Idmediu)
                    .HasColumnName("idmediu")
                    .HasDefaultValueSql("nextval('relmonitor.mediu_idmediu_seq'::regclass)");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Denumire)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("denumire");
            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.HasKey(e => e.Idrelease)
                    .HasName("releases_pkey");

                entity.ToTable("releases", "relmonitor");

                entity.Property(e => e.Idrelease).HasColumnName("idrelease");

                entity.Property(e => e.Brvpath)
                    .HasMaxLength(1000)
                    .HasColumnName("brvpath");

                entity.Property(e => e.Bugs).HasColumnName("bugs");

                entity.Property(e => e.Comentarii)
                    .HasMaxLength(1500)
                    .HasColumnName("comentarii");

                entity.Property(e => e.Contentrelease)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("contentrelease");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Esteurgenta).HasColumnName("esteurgenta");

                entity.Property(e => e.Idaplicatie).HasColumnName("idaplicatie");

                entity.Property(e => e.Iddurata).HasColumnName("iddurata");

                entity.Property(e => e.Idmediu).HasColumnName("idmediu");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.Property(e => e.Idutilizator).HasColumnName("idutilizator");

                entity.Property(e => e.Imbunatatiri).HasColumnName("imbunatatiri");
                entity.Property(e => e.Relstatus).HasColumnName("relstatus");

                entity.Property(e => e.Testpath)
                    .HasMaxLength(1000)
                    .HasColumnName("testpath");

                entity.HasOne(d => d.IdaplicatieNavigation)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.Idaplicatie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_aplicatii");

                entity.HasOne(d => d.IddurataNavigation)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.Iddurata)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_duratarelease");

                entity.HasOne(d => d.IdmediuNavigation)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.Idmediu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_medii");

                entity.HasOne(d => d.IdstatusNavigation)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.Idstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_status");

                entity.HasOne(d => d.IdutilizatorNavigation)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.Idutilizator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_utilizatori");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Idstatus)
                    .HasName("status_pkey");

                entity.ToTable("status", "relmonitor");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Denumire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("denumire");
            });

            modelBuilder.Entity<Utilizatori>(entity =>
            {
                entity.HasKey(e => e.Idutilizator)
                    .HasName("utilizatori_pkey");

                entity.ToTable("utilizatori", "relmonitor");

                entity.Property(e => e.Idutilizator).HasColumnName("idutilizator");

                entity.Property(e => e.Datacreare).HasColumnName("datacreare");

                entity.Property(e => e.Datamodificare).HasColumnName("datamodificare");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Esteadmin).HasColumnName("esteadmin");

                entity.Property(e => e.Nume)
                    .HasMaxLength(100)
                    .HasColumnName("nume");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
