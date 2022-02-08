using PinewoodGrow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace PinewoodGrow.Data
{
	public static class GSeedData
    {
        public static Random rnd = new Random();
		public static void Initialize(IServiceProvider serviceProvider)
		{

			using (var context = new GROWContext(
				serviceProvider.GetRequiredService<DbContextOptions<GROWContext>>()))
			{
				

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

				if (!context.Addresses.Any())
				{
					var addresses = new List<Address>
					{
						new Address {							
							FullAddress = "4352 5th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4R2",
							Latitude = 43.1105654,
							Longitude = -79.0797108,
						},
						new Address {
							FullAddress = "4300 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N2",
							Latitude = 43.1110322,
							Longitude = -79.0786221,
						},
						new Address {
							FullAddress = "4322 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S8",
							Latitude = 43.110500,
							Longitude = -79.0807247,
						},
						new Address {
							FullAddress = "4447 3rd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4L1",
							Latitude = 43.1110133,
							Longitude = -79.0807247,
						},
						new Address {
							FullAddress = "4447 3rd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4L1",
							Latitude = 43.1110133,
							Longitude = -79.0807247,
						},
						new Address {
							FullAddress = "5125 Bridge St",
							City = "Niagara Falls",
							PostalCode = "L2E 2S9",
							Latitude = 43.1087576,
							Longitude = -79.0795157,
						},
						new Address {
							FullAddress = "4467 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N4",
							Latitude = 43.1090393,
							Longitude = -79.078987,
						},
						new Address {
							FullAddress = "4342 3rd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4K6",
							Latitude = 43.1108061,
							Longitude = -79.0772197,
						},
						new Address {
							FullAddress = "5155 Bridge St",
							City = "Niagara Falls",
							PostalCode = "L2E 2T2",
							Latitude = 43.108767,
							Longitude = -79.080085,
						},
						new Address {
							FullAddress = "4407 2nd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4H1",
							Latitude = 43.1099135,
							Longitude = -79.0763052,
						},
						new Address {
							FullAddress = "4677 Buttrey St",
							City = "Niagara Falls",
							PostalCode = "L2E 2X5",
							Latitude = 43.112119,
							Longitude = -79.070966,
						},
						new Address {
							FullAddress = "4472 2nd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4H2",
							Latitude = 43.1090012,
							Longitude = -79.0757509,
						},new Address {
							FullAddress = "4462 2nd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4H2",
							Latitude = 43.1091381,
							Longitude = -79.0757251,
						},
						new Address {
							FullAddress = "4340 3rd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4K6",
							Latitude = 43.1109053,
							Longitude = -79.0771677,
						},
						new Address {
							FullAddress = "4351 3rd Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4K6",
							Latitude = 43.1105922,
							Longitude = -79.077649,
						},
						new Address {
							FullAddress = "4399 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S7",
							Latitude = 43.1099483,
							Longitude = -79.0812512,
						},
						new Address {
							FullAddress = "4329 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S7",
							Latitude = 43.1109074,
							Longitude = -79.0812878,
						},
						new Address {
							FullAddress = "4322 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S8",
							Latitude = 43.1110133,
							Longitude = -79.0807247,
						},
						new Address {
							FullAddress = "4330 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S8",
							Latitude = 43.1109018,
							Longitude = -79.0807447,
						},
						new Address {
							FullAddress = "4338 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S8",
							Latitude = 43.1108024,
							Longitude = -79.0807499,
						},
						new Address {
							FullAddress = "4282 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S8",
							Latitude = 43.11156,
							Longitude = -79.0807837,
						},
						new Address {
							FullAddress = "4271 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S7",
							Latitude = 43.1116959,
							Longitude = -79.0812982,
						},
						new Address {
							FullAddress = "4281 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S7",
							Latitude = 43.1115584,
							Longitude = -79.0813122,
						},
						new Address {
							FullAddress = "4289 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S7",
							Latitude = 43.1144691,
							Longitude = -79.081271,
						},
						new Address {
							FullAddress = "4322 5th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4R2",
							Latitude = 43.1110134,
							Longitude = -79.0796185,
						},
						new Address {
							FullAddress = "4309 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N1",
							Latitude = 43.1112308,
							Longitude = -79.0791501,
						},
						new Address {
							FullAddress = "4307 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N1",
							Latitude = 43.1112813,
							Longitude = -79.0791171,
						},
						new Address {
							FullAddress = "4295 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N1",
							Latitude = 43.1113849,
							Longitude = -79.0910137,
						},
						new Address {
							FullAddress = "4354 5th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4R2",
							Latitude = 43.1105826,
							Longitude = -79.079664,
						},
						new Address {
							FullAddress = "4352 5th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4R2",
							Latitude = 43.1105654,
							Longitude = -79.0797108,
						},
						new Address {
							FullAddress = "4370 5th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4R2",
							Latitude = 43.1103517,
							Longitude = -79.0796343,
						},
						new Address {
							FullAddress = "4398 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N3",
							Latitude = 43.1099746,
							Longitude = -79.0784874,
						},
						new Address {
							FullAddress = "4380 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N2",
							Latitude = 43.1102633,
							Longitude = -79.0785068,
						},
						new Address {
							FullAddress = "4386 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N2",
							Latitude = 43.110365,
							Longitude = -79.0785821,
						},
						new Address {
							FullAddress = "4398 Fourth Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4N3",
							Latitude = 43.1099746,
							Longitude = -79.0784874,
						},
						new Address {
							FullAddress = "4416 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4T1",
							Latitude = 43.1097783,
							Longitude = -79.0807468,
						},
						new Address {
							FullAddress = "4412 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4T1",
							Latitude = 43.1097642,
							Longitude = -79.080814,
						},
						new Address {
							FullAddress = "4425 6th Ave",
							City = "Niagara Falls",
							PostalCode = "L2E 4S9",
							Latitude = 43.1096581,
							Longitude = -79.0812546,
						},
					};
					context.Addresses.AddRange(addresses);
					context.SaveChanges();
				}

				if (!context.Households.Any())
				{
					/*var households = new List<Household>
					{
						new Household {
							ID = 1,
                            FamilySize = 1,
							Dependants = 0,
							AddressID = context.Addresses.FirstOrDefault(a => a.PostalCode == "L2E 4R2").ID,
							LICO = true
						},
						new Household {
							ID = 2,
                            FamilySize = 1,
							Dependants = 0,
							AddressID = context.Addresses.FirstOrDefault(a => a.PostalCode == "L2E 4N2").ID,
							LICO = true
						},
						new Household {
							ID = 3,
                            FamilySize = 1,
							Dependants = 0,
							AddressID = context.Addresses.FirstOrDefault(a => a.PostalCode == "L2E 4S8").ID,
							LICO = true
						},
						new Household {
							ID = 4,
                            FamilySize = 2,
							Dependants = 0,
							AddressID = context.Addresses.FirstOrDefault(a => a.PostalCode == "L2E 4L1").ID,
							LICO = true
						},
					};
					context.Households.AddRange(households);
					context.SaveChanges();*/
					SeedData.SeedHouseHolds.Initialize(serviceProvider);

				}

				

				/*if (!context.Members.Any())
				{
					var members = new List<Member>
					{
						new Member
						{
							HouseholdID = 1,
							FirstName = "Jacob",
							LastName = "Striker",
							Age = 22,
							GenderID = 1,
							Telephone = "9056778043",
							Email = "JStriker@gmail.com",
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
							Telephone = "9028678043",
							Email = "GMarch@gmail.com",
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
							Telephone = "9056726343",
							Email = "AnneMilton@gmail.com",
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
							Telephone = "9051984043",
							Email = "MarryQ@gmail.com",
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
							Telephone = "9056759243",
							Email = "JerryQueen@gmail.com",
							Income = 12000,
							Notes = "N/A",
							Consent = true,
							CompletedBy = "John Doe",
							CompletedOn = DateTime.Parse("2021-09-01")
						}
					};
					context.Members.AddRange(members);
					context.SaveChanges();
				}*/

				if (!context.MemberSituations.Any())
				{


                    var Members = context.Members.Select(a=> a).ToList();
                    var Situations = context.Situations.Select(a => a).ToList();
                    var SituationIDs = Situations.Where(a => a.Name != "Volunteer" && a.Name != "Other" && a.Name != "GAINS (For Seniors)").Select(a=> a.ID).ToArray();
                    var ToAdd = new List<MemberSituation>();

                    var Gains = context.Situations.FirstOrDefault(a => a.Name == "GAINS (For Seniors)").ID;

                    foreach (var member in Members)
                    {
                        if (Int32.Parse(member.Age) > 65)
                        {
							ToAdd.Add(new MemberSituation
                            {
                                MemberID = member.ID, 
                                SituationID = Gains
                            });
                        }

                        if (rnd.Next(1, 3) == 2)
                        {
                            ToAdd.Add(new MemberSituation
                            {
                                MemberID = member.ID,
                                SituationID = SituationIDs[rnd.Next(0, SituationIDs.Length)]
                            });
                        }
                    }
					context.AddRange(ToAdd);
                    context.SaveChanges();

                }

				if (!context.MemberDietaries.Any())
				{


                    var Members = context.Members.Select(a => a).ToList();
                    var Dietaries = context.Dietaries.Select(a => a.ID).ToArray();
                    var ToAdd = new List<MemberDietary>();

                    var Gains = context.Situations.FirstOrDefault(a => a.Name == "GAINS (For Seniors)").ID;

                    foreach (var member in Members)
                    {
                      
                        if (rnd.Next(1, 3) == 2)
                        {
                            ToAdd.Add(new MemberDietary
							{
                                MemberID = member.ID,
                                DietaryID= Dietaries[rnd.Next(0, Dietaries.Length)]
                            });
                        }
                    }
                    context.AddRange(ToAdd);
                    context.SaveChanges();

				}
            }
		}
	
	}
}