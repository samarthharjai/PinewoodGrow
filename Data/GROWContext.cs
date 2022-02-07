using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Data
{
	public class GROWContext : DbContext
	{
		

		public GROWContext(DbContextOptions<GROWContext> options)
			: base(options)
		{
		}

		public DbSet<Member> Members { get; set; }
		public DbSet<Household> Households { get; set; }
		public DbSet<Gender> Genders{ get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<MemberSituation> MemberSituations { get; set; }
		public DbSet<Situation> Situations { get; set; }
		public DbSet<MemberDietary> MemberDietaries { get; set; }
		public DbSet<Dietary> Dietaries { get; set; }
		public DbSet<MemberDocument> MemberDocuments { get; set; }
		public DbSet<UploadedFile> UploadedFiles { get; set; }
		public DbSet<MemberHousehold> MemberHouseholds { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Prevent Cascade Delete from Household to Member
            modelBuilder.Entity<Household>()
                .HasMany<Member>(m => m.Members)
                .WithOne(h => h.Household)
                .HasForeignKey(h => h.HouseholdID)
                .OnDelete(DeleteBehavior.Restrict);

			//Add a composite primary key to MemberHousehold
			modelBuilder.Entity<MemberHousehold>()
			.HasKey(p => new { p.MemberID, p.HouseholdID });

			//Add a unique index to the Household Number
			modelBuilder.Entity<Household>()
			.HasIndex(h => h.ID)
			.IsUnique();

			//Many to Many Intersection
			modelBuilder.Entity<MemberDietary>()
			.HasKey(t => new { t.DietaryID, t.MemberID });

			//Many to Many Intersection
			modelBuilder.Entity<MemberSituation>()
			.HasKey(t => new { t.SituationID, t.MemberID });

			//Prevent Cascade Delete
			modelBuilder.Entity<MemberDietary>()
				.HasOne(md => md.Dietary)
				.WithMany(d => d.MemberDietaries)
				.HasForeignKey(md => md.DietaryID)
				.OnDelete(DeleteBehavior.Restrict);

			//Prevent Cascade Delete
			modelBuilder.Entity<MemberSituation>()
				.HasOne(ms => ms.Situation)
				.WithMany(s => s.MemberSituations)
				.HasForeignKey(ms => ms.SituationID)
				.OnDelete(DeleteBehavior.Restrict);

			//Prevent Cascade Delete
			modelBuilder.Entity<Gender>()
				.HasMany<Member>(m => m.Members)
				.WithOne(g => g.Gender)
				.HasForeignKey(g => g.GenderID)
				.OnDelete(DeleteBehavior.Restrict);
		}
    }
}
