using Microsoft.EntityFrameworkCore;
using MQTTprism.Data.Entities;
using System;
using System.IO;

namespace MQTTprism.Data
{
	public class DataContext : DbContext
	{
		public DataContext() : base()
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<Sensor> Sensors { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "database.sqlite")}");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Sensor>(s =>
			{
				s.HasKey(en => en.Id);
				s.Property(en => en.Name).IsRequired();
			});
#if DEBUG
			modelBuilder.Entity<Sensor>()
				.HasData(
					new Sensor { Id = "1", Name = "First item" },
					new Sensor { Id = "2", Name = "Second item" },
					new Sensor { Id = "3", Name = "Third item" }
				);
#endif
		}
	}
}