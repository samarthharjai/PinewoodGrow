using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PinewoodGrow.Models;

namespace PinewoodGrow.Data.SeedData
{
    public class SeedHouseHolds
    {
        #region Names
        internal static string[] FirstNames =
        {
            "Dwight", "Sailor", "Urijah", "Emaleigh", "Abraham", "Nailea", "Wilson", "Amoura", "Bronx", "Landrie",
            "Katherin", "Penny", "Bradley", "Tyleah", "Daisy", "Maximilliano", "Jaretzy", "Roderick", "Duane", "Hafsa",
            "Rahaf", "Lora", "Gionni", "Terrell", "Saniyah", "Sierra", "Keana", "Natania", "Jayon", "Mallorie", "Ryli",
            "Emilia", "Eiley", "Hana", "Caydon", "Addelyn", "Kavin", "Viyan", "Avarose", "Evyn", "Estevan", "Aarav",
            "Raul", "Josyah", "Chyanne", "Addy", "Kyrell", "Azaliah", "Roselynn", "Kasen", "Estelle", "Perrin", "Tamar",
            "Brysen", "Shyann", "Dierks", "Beckett", "Barbara", "Eowyn", "Gisela", "Scarleth", "Tracy", "Ariadne",
            "Rima", "Ivan", "Breann", "Adelle", "Zyra", "Estella", "Sariyah", "Azaiah", "Yamilet", "Porter", "Tenley",
            "Kynsleigh", "Channing", "Desean", "Janney", "Junia", "Fabio", "Pearson", "Jumana", "Joan", "Lilliann",
            "Damoni", "Madigan", "Kaydan", "Niya", "Shawn", "Desmond", "Callan", "Harleen", "Mandy", "Maris", "Winry",
            "Braxon", "Judy", "Hayley", "Ziona", "Sterling"
        };

        //50 Random Last Names
        internal static string[] LastNames =
        {
            "Osterman", "Spruill", "Oglesby", "Patti", "Burnell", "Minor", "Stedman", "Kapp", "Goodall", "Castano",
            "Petersen", "Wash", "Kincaid", "Leftwich", "Tellez", "Caceres", "Hedgepeth", "Linehan", "Pinson",
            "Maxfield", "Lomas", "Nay", "Hafner", "Caldera", "Blakey", "Guinn", "Blakeslee", "Correll", "Wittman",
            "Luong", "Blakeman", "Monge", "Moton", "Cardona", "Deen", "Rainer", "Nathan", "Mowery", "Mccraw", "Lucy",
            "Battaglia", "Alessi", "Lankford", "Tyler", "Landa", "Sorrell", "Dement", "Mahar", "Damian", "Griffis"
        };

        internal static string[] Volenteers =
        {
            "Ryleigh Farmer", "Kayson Sanford", "Claire Webb", "Nason Sigala", "Johnnie Schiff", "Dax Ritchie",
            "Clarity Prendergast", "Haizley Isley", "Klay Mallett", "Kalaya Mcclary",
        };

        internal static string[] EmailService =
            { "@gmail.com", "@outlook.com", "@gmail.ca", "@outlook.ca", "@Hotmail.com", "@inbox.com" };
        #endregion


        internal static Random rnd = new Random();
        internal static int[] GenderIDs;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new GROWContext(serviceProvider.GetRequiredService<DbContextOptions<GROWContext>>());

            //100 Random First Names

            //List of addresses
            var AddressIDs = context.Addresses.Select(a => a.ID).ToArray();
            GenderIDs = context.Genders.Select(g => g.ID).ToArray();
            foreach (var address in AddressIDs)
            {
                //Number of dependents
                var DependantCount = rnd.Next(0, 2);
                //Number of non Dependants
                var FamilyCount = rnd.Next(1, 5);
                var house = new Household()
                {
                    AddressID = address,
                    FamilySize = FamilyCount + DependantCount, // Total Family Size
                    FamilyName = "",
                    Dependants = DependantCount,
                    IsFixedAddress = true,
                    IsActive = true,
                };
                context.Add(house);
                context.SaveChanges();

                //Add members to household
                var (Members, Incomes) = GetMembers(house.ID, FamilyCount, GetHouseIncome(FamilyCount));

                house.FamilyName = Members.FirstOrDefault().LastName;

                context.AddRange(Members);
                context.SaveChanges();

                var MemberSituations = new List<MemberSituation>();

                for (var i = 0; i < Members.Count; i++)
                {
                    MemberSituations.AddRange(GetMemberSituations(MemberSituations, Incomes[i], Members[i]));
                }

                context.AddRange(MemberSituations);
                context.SaveChanges();

                context.AddRange(dependants(rnd.Next(1, 3), house.ID));
                context.SaveChanges();

                var h= context.Households.Include(a => a.Members).ThenInclude(a => a.MemberSituations).FirstOrDefault(a=> a.ID == house.ID);

                var LicoInfo = new LICOInfo()
                {
                    HouseholdID = house.ID,
                    FamilySize = house.Dependant.Count + house.Members.Count,
                    Income = h.HouseIncome,
                };
                LicoInfo.Verify();
                context.Add(LicoInfo);
                context.SaveChanges();
            }
        }

        private static List<Dependant> dependants(int count, int houseID)
        {
            var Dependant = new List<Dependant>();
            for (var i = 0; i < count; i++)
            {
                Dependant.Add(new Dependant
                {
                    DOB = RandomDate(),
                    HouseholdID = houseID
                });
            }

            return Dependant;
        }

        internal static List<MemberSituation> GetMemberSituations(List<MemberSituation> list,double income, Member member)
        {
       var situationIDs = getSituationIDs(rnd.Next(1,3));


            foreach (var t in situationIDs)
            {
                double div = rnd.Next(20, 70);
                div /= 100;
                var temp = income * div;
                income -= temp;
                list.Add(new MemberSituation
                {
                    MemberID = member.ID,
                    SituationID = t,
                    SituationIncome = Math.Round(temp,2),
                });
            }

            return list;

        }

        internal static List<int> getSituationIDs(int count)
        {
            var situations = new List<int>();
            var avalableSituations = new List<int>() { 1, 2, 3, 4, 5, 6,7,8};
            for (int i = 0; i < count; i++)
            {
                var sel = rnd.Next(1, avalableSituations.Count);
                situations.Add(avalableSituations[sel]);
                avalableSituations.RemoveAt(sel);

            }

            return situations;
        }


        internal static int GetHouseIncome(int size)
        {
            return size switch
            {
                int i when i == 1 => rnd.Next(15000, 22186),
                int i when i == 2 => rnd.Next(20000, 27619),
                int i when i == 3 => rnd.Next(25000, 33953),
                int i when i == 4 => rnd.Next(30000, 41225),
                int i when i == 5 => rnd.Next(35000, 46757),
                int i when i == 6 => rnd.Next(40000, 52734),
                _ => rnd.Next(40000, 58712)
            };
        }
        private static Tuple<List<Member>, List<double>> GetMembers(int houseID, int count, int HouseIncome)
        {
            var Members = new List<Member>();
            var FamilyName = GetRandomLastName();
            var Incomes = GetIncomes(count, HouseIncome);
            for (var i = 0; i < count; i++)
            {

                var firstName = GetRandomFirstName();
                var member = new Member()
                {
                    HouseholdID = houseID,
                    FirstName = firstName,
                    LastName = FamilyName,
                    Telephone = GetRandomPhone(),
                    Email = GetRandomEmail(firstName, FamilyName),
                    Notes = LoremIpsum(10, 50, 1, 4, 1),
                    Consent = true,
                    VolunteerID = getRandomVolunteer(),
                    CompletedOn = DateTime.Today,
                    GenderID = getRandomGender(),
                    DOB = RandomDay(),
                };
                Members.Add(member);
            }

            return Tuple.Create(Members, Incomes);
        }


        private static List<double> GetIncomes(int count, int HouseIncome)
        {
            var incomes = new double[count];

            var AvalIncome = HouseIncome / count;

            for (var i = 0; i < count; i++)
            {
                incomes[i] += rnd.Next(1000, AvalIncome);


            }

            if (incomes.Sum() * 1.5 < HouseIncome)
            {
                incomes[^1] += (int)(incomes.Sum() * .6);
            }

            return incomes.ToList();
        }



        private static string GetRandomFirstName()
        {
            return FirstNames[rnd.Next(0, FirstNames.Length)];
        }
        private static string GetRandomLastName()
        {
            return LastNames[rnd.Next(0, LastNames.Length)];
        }
        private static string GetRandomPhone()
        {
            var Phone = rnd.Next(0, 1) == 1 ? "905" : "289";
            for (var i = 0; i < 7; i++)
            {
                Phone += rnd.Next(0, 9);
            }

            return Phone;
        }

        private static string GetRandomNumberString(int count)
        {
            var y = "";
            while (y.Length < count)
            {
                y += rnd.Next(0, 9);
            }

            return y;
        }

        private static int getRandomAge()
        {
            return rnd.Next(0, 100) switch
            {
                int i when i <= 30 => rnd.Next(18, 24), //30% age range 18-24
                int i when i <= 70 => rnd.Next(25, 40), // 40% age range 25 - 40
                int i when i <= 90 => rnd.Next(41, 65),  // 20% age range 41 - 65
                int i when i <= 99 => rnd.Next(66, 85), // 9% age range 66- 85
                _ => rnd.Next(86, 100) // 1% age range 86 - 100
            };
        }

        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = (new DateTime(2001, 1, 1) - start).Days;
            return start.AddDays(rnd.Next(range));
        }
        private static DateTime RandomDate()
        {
            DateTime start = new DateTime(2002, 1, 1);
            int range = (new DateTime(2022, 1, 1) - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        private static int getRandomGender()
        {
            return rnd.Next(0, 100) switch
            {
                int i when i <= 5 => 5, //Perfer not to say 5%
                int i when i <= 10 => 4, //Other 5%
                int i when i <= 20 => 3, //Non-Bianary 10%
                int i when i <= 60 => 2, //Female 40%
                _ => 1 // Male 40%
            };
        }

        private static int getRandomVolunteer()
        {
            return rnd.Next(0, 100) switch
            {
                int i when i <= 5 => 5, //Perfer not to say 5%
                int i when i <= 10 => 4, //Other 5%
                int i when i <= 20 => 3, //Non-Bianary 10%
                int i when i <= 60 => 2, //Female 40%
                _ => 1 // Male 40%
            };
        }


        private static string GetRandomEmail(string first, string last)
        {

            var Email = rnd.Next(0, 5) switch
            {
                0 => GetRandomNumberString(2) + last[..3] + "-",
                1 => last + ".",
                2 => last[0] + ".",
                3 => last[..2] + ".",
                4 => last + GetRandomNumberString(2) + ".",

                _ => " "
            };


            //Email has full name + 3 random letters before ending with the addresses service provider
            Email += first + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9) + EmailService[rnd.Next(0, EmailService.Length)];

            return Email;
        }

        //I Did not make this
        private static string LoremIpsum(int minWords, int maxWords,
            int minSentences, int maxSentences,
            int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                               + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
            }

            return result.ToString();
        }
    }

}

