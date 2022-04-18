using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;
using PinewoodGrow.Models.Temp;

namespace PinewoodGrow.Data
{
	public class GROWContext : DbContext
	{
        //To give access to IHttpContextAccessor for Audit Data with IAuditable
        private readonly IHttpContextAccessor _httpContextAccessor;

		public string UserName
        {
            get; private set;
        }
		public GROWContext(DbContextOptions<GROWContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            UserName ??= "Unknown";
        }

		public GROWContext(DbContextOptions<GROWContext> options)
			: base(options)
		{
		}


		public DbSet<Member> Members { get; set; }
        public DbSet<LICOInfo> LICOInfos { get; set; }
        public DbSet<Dependant> Dependents { get; set; }
        public DbSet<Household> Households { get; set; }
		public DbSet<Gender> Genders { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<MemberSituation> MemberSituations { get; set; }
		public DbSet<Situation> Situations { get; set; }
		public DbSet<MemberDietary> MemberDietaries { get; set; }
		public DbSet<Dietary> Dietaries { get; set; }
		public DbSet<MemberIllness> MemberIllnesses { get; set; }
		public DbSet<Illness> Illnesses { get; set; }
		public DbSet<Volunteer> Volunteers { get; set; }
		public DbSet<Sale> Sales { get; set; }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
		public DbSet<ProductUnitPrice> ProductUnitPrices { get; set; }

		public DbSet<Payment> Payments { get; set; }
		public DbSet<SaleDetail> SaleDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<MemberDocument> MemberDocuments { get; set; }
		public DbSet<UploadedFile> UploadedFiles { get; set; }
		public DbSet<MemberHousehold> MemberHouseholds { get; set; }
        public DbSet<GroceryStore> GroceryStores { get; set; }
		public DbSet<TravelDetail> TravelDetails { get; set; }




        #region Temp Tables

        public DbSet<TempHousehold> TempHouseholds { get; set; }
        public DbSet<TempMember> TempMembers { get; set; }
        public DbSet<TempMemberDietary> TempMemberDietaries { get; set; }
        public DbSet<TempMemberDocument> TempMemberDocuments { get; set; }
        public DbSet<TempMemberHousehold> TempMemberHouseholds { get; set; }
        public DbSet<TempMemberIllness> TempMemberIllnesses { get; set; }
        public DbSet<TempMemberSituation> TempMemberSituations { get; set; }
		public DbSet<TempAddress> TempAddresses { get; set; }


		#endregion







		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{



            //Store details
            modelBuilder.Entity<GroceryStore>()
                .HasMany(g=> g.TravelDetails)
                .WithOne(t=> t.GroceryStore)
                .HasForeignKey(t=> t.GroceryID)
                .OnDelete(DeleteBehavior.Restrict);

			//Travel Details
            modelBuilder.Entity<Address>()
                .HasOne(c => c.TravelDetail)
                .WithOne(c => c.Address)
                .HasForeignKey<TravelDetail>(c=> c.AddressID);

            modelBuilder.Entity<Address>()
                .HasIndex(a => a.PlaceID)
                .IsUnique();

			modelBuilder.Entity<Member>()
				.HasIndex(a => a.Email)
				.IsUnique();

			modelBuilder.Entity<Volunteer>()
				.HasIndex(a => a.Email)
				.IsUnique();

			//Prevent Cascade Delete from Household to Member
			modelBuilder.Entity<Household>()
				.HasMany<Member>(m => m.Members)
				.WithOne(h => h.Household)
				.HasForeignKey(h => h.HouseholdID)
				.OnDelete(DeleteBehavior.Restrict);

			//Add this so you don't get Cascade Delete
			modelBuilder.Entity<Member>()
				.HasMany<MemberHousehold>(p => p.MemberHouseholds)
				.WithOne(c => c.Member)
				.HasForeignKey(c => c.MemberID)
				.OnDelete(DeleteBehavior.Restrict);

			//Add a composite primary key to MemberHousehold
			modelBuilder.Entity<MemberHousehold>()
			.HasKey(p => new { p.MemberID, p.HouseholdID });

			//Add a unique index to the Household Number
			modelBuilder.Entity<Household>()
				.HasIndex(h => h.ID)
				.IsUnique();

			//Adds One? to many Household to address
            modelBuilder.Entity<Household>()
                .HasOne(h => h.Address)
                .WithMany(a => a.Households)
                .HasForeignKey(h => h.AddressID)
                .IsRequired(false);

            modelBuilder.Entity<LICOInfo>().HasOne(h => h.Household)
                .WithMany(h => h.LICOHistory)
                .HasForeignKey(a => a.HouseholdID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Dependant>().HasOne(h => h.Household).WithMany(h => h.Dependant)
                .HasForeignKey(h => h.HouseholdID).OnDelete(DeleteBehavior.Restrict);


            /*
                        modelBuilder.Entity<Household>()
                            .HasOne(a => a.Address)
                            .WithOne(h => h.Household)
                            .HasForeignKey<Address>(a => a.ID);
            */

            modelBuilder.Entity<Product>()
                .HasMany<ProductUnitPrice>(d => d.ProductUnitPrices)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>().HasOne(p => p.ProductType)
                .WithMany(p => p.Products)
                .HasForeignKey(a => a.ProductTypeID)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Invoice>().HasOne(a=> a.Product)
                .WithMany(a=> a.Invoices)
                .HasForeignKey(a=> a.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

			//Many to Many Intersection
			modelBuilder.Entity<MemberDietary>()
			.HasKey(t => new { t.DietaryID, t.MemberID });

			//Many to Many Intersection
			modelBuilder.Entity<MemberSituation>()
			.HasIndex(t => new { t.SituationID, t.MemberID })
			.IsUnique();

			//Many to Many Intersection
			modelBuilder.Entity<MemberIllness>()
			.HasKey(t => new { t.IllnessID, t.MemberID });

			//Many to Many Intersection
			modelBuilder.Entity<SaleDetail>()
			.HasKey(t => new { t.SaleID, t.ProductID });

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
                .IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			//Prevent Cascade Delete
			modelBuilder.Entity<MemberIllness>()
				.HasOne(mi => mi.Illness)
				.WithMany(i => i.MemberIllnesses)
				.HasForeignKey(mi => mi.IllnessID)
				.OnDelete(DeleteBehavior.Restrict);

			//Prevent Cascade Delete
			modelBuilder.Entity<Gender>()
				.HasMany<Member>(m => m.Members)
				.WithOne(g => g.Gender)
				.HasForeignKey(g => g.GenderID)
				.OnDelete(DeleteBehavior.Restrict);

			/*//Prevent Cascade Delete
			modelBuilder.Entity<Volunteer>()
				.HasMany<Member>(m => m.Members)
				.WithOne(v => v.Volunteer)
				.HasForeignKey(v => v.VolunteerID)
				.OnDelete(DeleteBehavior.Restrict);*/

			//Prevent Cascade Delete
			modelBuilder.Entity<Sale>()
				.HasMany<SaleDetail>(s => s.SaleDetails)
				.WithOne(s => s.Sale)
				.HasForeignKey(v => v.SaleID)
				.OnDelete(DeleteBehavior.Restrict);

			//Prevent Cascade Delete
			modelBuilder.Entity<ProductType>()
				.HasMany<Product>(p => p.Products)
				.WithOne(p => p.ProductType)
				.HasForeignKey(p => p.ProductTypeID)
				.OnDelete(DeleteBehavior.Restrict);

			//Add a unique index to the Product Number
			modelBuilder.Entity<Product>()
				.HasIndex(p => p.ID)
				.IsUnique();


			#region Temp Data

	

            modelBuilder.Entity<TempAddress>()
                .HasIndex(a => a.PlaceID)
                .IsUnique();

            //Prevent Cascade Delete from Household to Member
            modelBuilder.Entity<TempHousehold>()
                .HasMany(m => m.Members)
                .WithOne(h => h.TempHousehold)
                .HasForeignKey(h => h.TempHouseholdID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false); 

            modelBuilder.Entity<Household>()
                .HasMany(m => m.Members)
                .WithOne(h => h.Household)
                .HasForeignKey(h => h.HouseholdID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<TempMember>().HasMany(a => a.MemberSituations)
                .WithOne(a => a.Member).HasForeignKey(a => a.MemberID)
                .OnDelete(DeleteBehavior.Cascade);


            //Adds One? to many Household to address
            modelBuilder.Entity<TempAddress>()
                .HasMany(h => h.Households)
                .WithOne(a => a.Address)
                .HasForeignKey(h=> h.AddressID)
                .IsRequired(false);

            //Many to Many Intersection
            modelBuilder.Entity<TempMemberDietary>()
                .HasKey(t => new { t.DietaryID, t.MemberID });

            //Many to Many Intersection
            modelBuilder.Entity<TempMemberSituation>()
                .HasIndex(t => new { t.SituationID, t.MemberID })
                .IsUnique();
           
            //Many to Many Intersection
            modelBuilder.Entity<TempMemberIllness>()
                .HasKey(t => new { t.IllnessID, t.MemberID });

 


			#endregion


		}

		#region Audit Functions
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }


		#endregion






	}
}