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
						new Household {
							ID = 2,
							HouseIncome = 11730,
							LICO = true
						},
						new Household {
							ID = 3,
							HouseIncome = 16300,
							LICO = true
						},
						new Household {
							ID = 4,
							HouseIncome = 27300,
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
						new Gender { ID = 1, Name = "Male"},
						new Gender { ID = 2, Name = "Female"},
						new Gender { ID = 3, Name = "Non-Binary"},
						new Gender { ID = 4, Name = "Other"},
						new Gender { ID = 5, Name = "Prefer not to say"}
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
						},
						new Address {
							FullAddress = "28 King St.",
							City = "Niagara Falls",
							PostalCode = "T7K 2C3"
						},
						new Address {
							FullAddress = "47 Lakeshore Dr.",
							City = "Port Colborne",
							PostalCode = "H2Y 8H7"
						},
						new Address {
							FullAddress = "58 Chestnut Ave.",
							City = "Welland",
							PostalCode = "H3S 2W1"
						}
					};
					context.Addresses.AddRange(addresses);
					context.SaveChanges();
				}

				if (!context.Situations.Any())
				{
					var situations = new List<Situation>
					{
						new Situation { ID = 1, Name = "ODSP"},
						new Situation { ID = 2, Name = "Ontario Works"},
						new Situation { ID = 3, Name = "CPP-Disability"},
						new Situation { ID = 4, Name = "EI"},
						new Situation { ID = 5, Name = "GAINS (For Seniors)"},
						new Situation { ID = 6, Name = "Post-Sec. Student"},
						new Situation { ID = 7, Name = "Other"},
						new Situation { ID = 8, Name = "Volunteer"}
					};
					context.Situations.AddRange(situations);
					context.SaveChanges();
				}

				if (!context.Dietaries.Any())
				{
					var dietaries = new List<Dietary>
					{
						new Dietary { ID = 1, Name = "Diabetes"},
						new Dietary { ID = 2, Name = "Obesity"},
						new Dietary { ID = 3, Name = "Lactose Intolerance"},
						new Dietary { ID = 4, Name = "Gluten Intolerance/Sensitivity"},
						new Dietary { ID = 5, Name = "Cancer"},
						new Dietary { ID = 6, Name = "Heart Disease"},
						new Dietary { ID = 7, Name = "Osteoperosis"},
						new Dietary { ID = 8, Name = "Digestive Disorders"},
						new Dietary { ID = 9, Name = "Food Allergies"}
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
						},
						new Member
						{
							HouseholdID = 2,
							FirstName = "Gregory",
							LastName = "March",
							Age = 64,
							GenderID = 1,
							AddressID = 2,
							Telephone = "9028678043",
							Email = "GMarch@gmail.com",
							FamilySize = 1,
							Income = 11730,
							Notes = "N/A",
							Consent = true,
							CompletedBy = "John Doe",
							CompletedOn = DateTime.Parse("2021-09-01")
						},
						new Member
						{
							HouseholdID = 3,
							FirstName = "Anne",
							LastName = "Milton",
							Age = 51,
							GenderID = 2,
							AddressID = 3,
							Telephone = "9056726343",
							Email = "AnneMilton@gmail.com",
							FamilySize = 1,
							Income = 16300,
							Notes = "N/A",
							Consent = true,
							CompletedBy = "John Doe",
							CompletedOn = DateTime.Parse("2021-09-01")
						},
						new Member
						{
							HouseholdID = 4,
							FirstName = "Marry",
							LastName = "Queen",
							Age = 48,
							GenderID = 2,
							AddressID = 1,
							Telephone = "9051984043",
							Email = "MarryQ@gmail.com",
							FamilySize = 1,
							Income = 15300,
							Notes = "N/A",
							Consent = true,
							CompletedBy = "John Doe",
							CompletedOn = DateTime.Parse("2021-09-01")
						},
						new Member
						{
							HouseholdID = 4,
							FirstName = "Jerry",
							LastName = "Queen",
							Age = 46,
							GenderID = 1,
							AddressID = 1,
							Telephone = "9056759243",
							Email = "JerryQueen@gmail.com",
							FamilySize = 1,
							Income = 12000,
							Notes = "N/A",
							Consent = true,
							CompletedBy = "John Doe",
							CompletedOn = DateTime.Parse("2021-09-01")
						}
						);
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
						new MemberSituation {
							SituationID = context.Situations.FirstOrDefault(s => s.Name == "Ontario Works").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "March" && m.FirstName == "Gregory").ID
						},
						new MemberSituation {
							SituationID = context.Situations.FirstOrDefault(s => s.Name == "EI").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Milton" && m.FirstName == "Anne").ID
						},
						new MemberSituation {
							SituationID = context.Situations.FirstOrDefault(s => s.Name == "GAINS (For Seniors)").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Queen" && m.FirstName == "Marry").ID
						},
						new MemberSituation {
							SituationID = context.Situations.FirstOrDefault(s => s.Name == "ODSP").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Queen" && m.FirstName == "Jerry").ID
						}
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
						new MemberDietary {
							DietaryID = context.Dietaries.FirstOrDefault(s => s.Name == "Diabetes").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "March" && m.FirstName == "Gregory").ID
						},
						new MemberDietary {
							DietaryID = context.Dietaries.FirstOrDefault(s => s.Name == "Cancer").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Milton" && m.FirstName == "Anne").ID
						},
						new MemberDietary {
							DietaryID = context.Dietaries.FirstOrDefault(s => s.Name == "Heart Disease").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Queen" && m.FirstName == "Marry").ID
						},
						new MemberDietary {
							DietaryID = context.Dietaries.FirstOrDefault(s => s.Name == "Osteoperosis").ID,
							MemberID = context.Members.FirstOrDefault(m => m.LastName == "Queen" && m.FirstName == "Jerry").ID
						}
					};
					context.MemberDietaries.AddRange(memberDietaries);
					context.SaveChanges();
				}
			}
		}
	}
}
