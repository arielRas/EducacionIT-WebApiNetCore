using System;
using System.Collections.Generic;
using BookStore.Data.Databases.BookStoreDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb;

public partial class BookStoreDbContext : DbContext
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Author { get; set; }

    public virtual DbSet<Book> Book { get; set; }

    public virtual DbSet<Edition> Edition { get; set; }

    public virtual DbSet<EditionPrice> EditionPrice { get; set; }

    public virtual DbSet<EditionType> EditionType { get; set; }

    public virtual DbSet<Editorial> Editorial { get; set; }

    public virtual DbSet<Genre> Genre { get; set; }

    public virtual DbSet<HistoryPrice> HistoryPrice { get; set; }

    public virtual DbSet<Isbn> Isbn { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:BookStoreDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BOOK_AUTHOR_AUTHOR"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BOOK_AUTHOR_BOOK"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("BOOK_AUTHOR");
                        j.IndexerProperty<int>("BookId").HasColumnName("BOOK_ID");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AUTHOR_ID");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BOOK_GENRE_GENRE"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BOOK_GENRE_BOOK"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreCode");
                        j.ToTable("BOOK_GENRE");
                        j.IndexerProperty<int>("BookId").HasColumnName("BOOK_ID");
                        j.IndexerProperty<string>("GenreCode")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .HasColumnName("GENRE_CODE");
                    });
        });

        modelBuilder.Entity<Edition>(entity =>
        {
            entity.HasKey(e => e.EditionId).HasName("EDITION_PK");

            entity.Property(e => e.EditionId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Book).WithMany(p => p.Editions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EDITION_BOOK");

            entity.HasOne(d => d.Editorial).WithMany(p => p.Edition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EDITION_EDITORIAL");

            entity.HasOne(d => d.EditionType).WithMany(p => p.Editions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EDITION_EDITION_TYPE");
        });

        modelBuilder.Entity<EditionPrice>(entity =>
        {
            entity.ToTable("EDITION_PRICE", tb => tb.HasTrigger("TRG_INSERT_HISTORY_PRICE"));

            entity.Property(e => e.EditionId).ValueGeneratedNever();

            entity.HasOne(d => d.Edition).WithOne(p => p.EditionPrice).HasConstraintName("PK_EDITION_PRICE_EDITION");
        });

        modelBuilder.Entity<HistoryPrice>(entity =>
        {
            entity.HasOne(d => d.Edition).WithMany(p => p.HistoryPrice).HasConstraintName("PK_HISTORY_PRICE_EDITION");
        });

        modelBuilder.Entity<Isbn>(entity =>
        {
            entity.HasOne(d => d.Edition).WithOne(p => p.Isbn).HasConstraintName("FK_ISBN_EDITION");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
