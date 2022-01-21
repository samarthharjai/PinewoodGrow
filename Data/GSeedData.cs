using PinewoodGrow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Data
{
	public static class GSeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{

			using (var context = new GROWContext(
				serviceProvider.GetRequiredService<DbContextOptions<GROWContext>>()))
			{
				if (!context.Households.Any())
				{
					var households = new List<Household>
					{
						new Household {
							ID = 1,
							HouseIncome = 10000,
							LICO = true
						},
					};
					context.Households.AddRange(households);
					context.SaveChanges();
				}

				if (!context.Genders.Any())
				{
					var genders = new List<Gender>
					{
						new Gender { Name = "Male"},
						new Gender { Name = "Female"},
						new Gender { Name = "Non-Binary"},
						new Gender { Name = "Other"},
						new Gender { Name = "Prefer not to say"}
					};
					context.Genders.AddRange(genders);
					context.SaveChanges();
				}

				if (!context.Addresses.Any())
				{
					var addresses = new List<Address>
					{
						new Address {
							FullAddress = "58 Chestnut Ave.",
							City = "Welland",
							PostalCode = "H4K 2V7"
						}
					};
					context.Addresses.AddRange(addresses);
					context.SaveChanges();
				}

				if (!context.Situations.Any())
				{
					var situations = new List<Situation>
					{
						new Situation { Name = "ODSP"},
						new Situation { Name = "Ontario Works"},
						new Situation { Name = "CPP-Disability"},
						new Situation { Name = "EI"},
						new Situation { Name = "GAINS (For Seniors)"},
						new Situation { Name = "Post-Sec. Student"},
						new Situation { Name = "Other"},
						new Situation { Name = "Volunteer"}
					};
					context.Situations.AddRange(situations);
					context.SaveChanges();
				}

				if (!context.Dietaries.Any())
				{
					var dietaries = new List<Dietary>
					{
						new Dietary { Name = "Diabetes"},
						new Dietary { Name = "Obesity"},
						new Dietary { Name = "Lactose Intolerance"},
						new Dietary { Name = "Gluten Intolerance/Sensitivity"},
						new Dietary { Name = "Cancer"},
						new Dietary { Name = "Heart Disease"},
						new Dietary { Name = "Osteoperosis"},
						new Dietary { Name = "Digestive Disorders"},
						new Dietary { Name = "Food Allergies"}
					};
					context.Dietaries.AddRange(dietaries);
					context.SaveChanges();
				}

				if (!context.Members.Any())
				{
					context.Members.AddRange(
						new Member
						{
							HouseholdID = 1,
							FirstName = "Jacob",
							LastName = "Striker",
							Age = 22,
							GenderID = 1,
							AddressID = 1,
							Telephone = "9056778043",
							Email = "JStriker@gmail.com",
							FamilySize = 1,
							Income = 10000,
							Notes = "N/A",
							Consent = true,
							CompletedBy = "John Doe",
							CompletedOn = DateTime.Parse("2021-09-01")
						});
					context.SaveChanges();
				}

				if (!context.MemberSituations.Any())
				{
					var memberSituations = new List<MemberSituation>
					{
						new MemberSituation {
							SituationID = context.Situations.FirstOrDefault(s => s.Name == "ODSP").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Striker" && m.FirstName == "Jacob").ID
						},
					};
					context.MemberSituations.AddRange(memberSituations);
					context.SaveChanges();
				}

				if (!context.MemberDietaries.Any())
				{
					var memberDietaries = new List<MemberDietary>
					{
						new MemberDietary {
							DietaryID = context.Dietaries.FirstOrDefault(d => d.Name == "Obesity").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Striker" && m.FirstName == "Jacob").ID
						},
					};
					context.MemberDietaries.AddRange(memberDietaries);
					context.SaveChanges();
				}

			}
		}
	}
}
