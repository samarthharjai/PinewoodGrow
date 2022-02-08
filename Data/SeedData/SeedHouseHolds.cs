using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            var DependantCount = rnd.Next(0, 5);
            //Number of non Dependants
            var FamilyCount = rnd.Next(1, 4);
            var house = new Household()
            {
                HouseIncome = 10000,
                AddressID = address,
                FamilySize = FamilyCount + DependantCount, // Total Family Size
                Dependents = DependantCount,
                LICO = true,
            };
            context.Add(house);
            context.SaveChanges();

            //Add members to household
            var Members = GetMembers(house.ID, FamilyCount); 

            context.AddRange(Members);
            context.SaveChanges();

        }

        }

        private static List<Member> GetMembers(int houseID, int count)
        {
            var Members = new List<Member>();
            var FamilyName = GetRandomLastName();

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
                    Income = getRandomIncome() / count,
                    Notes = LoremIpsum(10, 50, 1, 4, 1),
                    Consent = true,
                    CompletedBy = Volenteers[rnd.Next(0, Volenteers.Length)],
                    CompletedOn = DateTime.Today,
                    GenderID = getRandomGender(),
                    Age = getRandomAge(),
                };
                
                Members.Add(member);
            }

            return Members;
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
                int i when i <= 30 => rnd.Next(18,24), //30% age range 18-24
                int i when i <= 70 => rnd.Next(25, 40), // 40% age range 25 - 40
                int i when i <= 90 => rnd.Next(41,65),  // 20% age range 41 - 65
                int i when i <= 99 => rnd.Next(66, 85), // 9% age range 66- 85
                _ => rnd.Next(86, 100) // 1% age range 86 - 100
            };
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
        private static int getRandomIncome()
        {
            return rnd.Next(0, 100) switch
            {
                int i when i <= 5 => 0, // 5% income of 0
                int i when i <= 15 => rnd.Next(0,4999), // 10% income of 0 - 5k
                int i when i <= 45 => rnd.Next(5000, 9999), // 30% 5-10k
                int i when i <= 75 => rnd.Next(10000, 14999),  // 30% 10 - 15k
                int i when i <= 95 => rnd.Next(15000, 19999), // 20% 15k - 20k
                _ => rnd.Next(20000, 24999) // 5% 20k-25k
            };
        }


        private static string GetRandomEmail(string first, string last)
        {
         
            var Email  = rnd.Next(0,5) switch
            {
                0 => GetRandomNumberString(2) + last[..3] +"-",
                1 => last +".",
                2 => last[0] + ".",
                3 => last[..2] +".",
                4 =>  last + GetRandomNumberString(2) + ".",

                _ => " "
            };


            //Email has full name + 3 random letters before ending with the addresses service provider
            Email += first + rnd.Next(0,9) + rnd.Next(0, 9)+ rnd.Next(0,9) + EmailService[rnd.Next(0, EmailService.Length)];

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





