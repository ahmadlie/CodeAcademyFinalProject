
using DataAccessLayer.Configurations;
using EntityLayer.Base;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
	{
		private readonly IConfiguration _configuration;
		public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
		{
			_configuration = configuration;
		}
		//public AppDbContext()
		//{

		//}
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<AppRole> AppRoles { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			//if (!optionsBuilder.IsConfigured)
			//{
			//	optionsBuilder.UseSqlServer(_configuration["DefaultConnection"]);
			//}
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{


			modelBuilder.ApplyConfiguration(new PostConfiguration());
			modelBuilder.ApplyConfiguration(new AppUserConfiguration());
			modelBuilder.ApplyConfiguration(new ImageConfiguration());
			modelBuilder.ApplyConfiguration(new CommentConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
