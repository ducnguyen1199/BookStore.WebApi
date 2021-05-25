using BookStore.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.Database
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, int>
	{
		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{ }

		/*public ApplicationDbContext():base()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
			.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStore;integrated security=true;Trusted_Connection=True")
			.LogTo(Console.WriteLine);*/

		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<BooksInCart> BooksInCarts { get; set; }
		public DbSet<DetailOrder> DetailOrders { get; set; }
		public DbSet<FavoriteBook> FavoriteBooks { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Author>()
				.HasMany(a => a.Books)
				.WithOne(b => b.Author)
				.HasForeignKey(b => b.IdAuthor)
				.IsRequired();


			modelBuilder.Entity<Book>()
				.HasOne(b => b.Category)
				.WithMany()
				.HasForeignKey(b => b.IdCategory)
				.IsRequired();

			modelBuilder.Entity<Book>()
				.Property(b => b.DateCreated)
				.HasDefaultValueSql("getDate()");

			modelBuilder.Entity<FavoriteBook>()
				.HasOne(fb => fb.Book)
				.WithMany(b => b.FavoriteBooks)
				.HasForeignKey(fb => fb.IdBook)
				.IsRequired();

			modelBuilder.Entity<BooksInCart>()
				.HasOne(b => b.Book)
				.WithMany()
				.HasForeignKey(bic => bic.IdBook)
				.IsRequired();
			modelBuilder.Entity<BooksInCart>()
				.Property(b => b.DateUpdated)
				.HasDefaultValueSql("getDate()");

			modelBuilder.Entity<Order>()
				.Property(o => o.DateCreated)
				.HasDefaultValueSql("getDate()");

			modelBuilder.Entity<Order>()
				.HasMany(o => o.DetailOrders)
				.WithOne(dor => dor.Order)
				.HasForeignKey(dor => dor.IdOrder)
				.IsRequired();
			


			modelBuilder.Entity<DetailOrder>()
				.HasKey(d => new { d.IdBook, d.IdOrder });


			modelBuilder.Entity<Role>()
				.HasMany(r => r.UserRoles)
				.WithOne()
				.HasForeignKey(ur => ur.RoleId)
				.IsRequired();


			modelBuilder.Entity<User>()
				.Property(u=> u.Avatar)
				.HasDefaultValueSql("https://localhost:44369/avatars/defaultAvatar.jpg");
			modelBuilder.Entity<User>()
				.HasMany(u => u.UserRoles)
				.WithOne()
				.HasForeignKey(ur => ur.UserId)
				.IsRequired();

			modelBuilder.Entity<User>()
				.HasMany(u => u.FavoriteBooks)
				.WithOne()
				.HasForeignKey(fb => fb.IdUser)
				.IsRequired();

		}

	}
}
