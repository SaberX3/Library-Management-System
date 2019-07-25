﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDemo.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryDemo.Data
{
    public class LendingInfoDbContext:DbContext
    {
        public LendingInfoDbContext(DbContextOptions<LendingInfoDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BooksDetail { get; set; }
        public DbSet<Bookshelf> Bookshelves { get; set; }
        public DbSet<RecommendedBook> RecommendedBooks { get; set; }
        public DbSet<StudentInfo> Students { get; set; }
        public DbSet<AppointmentOrLending> AppointmentOrLendings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppointmentOrLending>()
                .HasKey(c => new { c.BookId, c.StudentId });
        }
    }
}
