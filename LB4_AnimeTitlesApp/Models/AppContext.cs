using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LB4_AnimeTitlesApp.Models;

public partial class AppContext : DbContext
{
    public AppContext()
    {
    }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnimeTitle> AnimeTitles { get; set; }

    public virtual DbSet<AnimeType> AnimeTypes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AnimeTitle;Username=postgres;Password=1111");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimeTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AnimeTitles_pkey");

            entity.ToTable("animeTitles");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"AnimeTitles_Id_seq\"'::regclass)");
            entity.Property(e => e.CountOfSeries).HasColumnName("countOfSeries");
            entity.Property(e => e.Decsription).HasColumnName("decsription");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IsComlete).HasColumnName("isComlete");
            entity.Property(e => e.OriginalName).HasColumnName("originalName");
            entity.Property(e => e.Poster).HasColumnName("poster");
            entity.Property(e => e.Studio).HasColumnName("studio");
            entity.Property(e => e.TitleNme).HasColumnName("titleNme");
            entity.Property(e => e.YearOfRelese).HasColumnName("yearOfRelese");

            entity.HasOne(d => d.IdAnimeTypeNavigation).WithMany(p => p.AnimeTitles)
                .HasForeignKey(d => d.IdAnimeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AnimeTitles_IdAnimeType_fkey");

            entity.HasOne(d => d.IdGenreNavigation).WithMany(p => p.AnimeTitles)
                .HasForeignKey(d => d.IdGenre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AnimeTitles_IdGenre_fkey");

            entity.HasOne(d => d.IdModeratorNavigation).WithMany(p => p.AnimeTitles)
                .HasForeignKey(d => d.IdModerator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AnimeTitles_IdModerator_fkey");
        });

        modelBuilder.Entity<AnimeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animeTypes_pkey");

            entity.ToTable("animeTypes");

            entity.Property(e => e.AnimeOfType).HasColumnName("animeOfType");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.Property(e => e.GenreName).HasColumnName("genreName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.DateOfRegistration).HasColumnName("dateOfRegistration");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
