using System;
using System.Collections.Generic;
namespace GuessWho
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Guess Who!");
            List<Card> Board = CardList();
            foreach (var item in Board)
            {
                Console.WriteLine(item.Name);
            }
        }

        static List<Card> CardList()
        {
            List<Card> list = new List<Card>() {
            new Card() {
                Name = "Rick",
                Hat = null,
                Glasses = null,
                EyeColor = "Green",
                HairColor = "Blonde",
                FacialHair = null,
                Gender = "Male"
            },
            new Card() {
                Name = "Mori",
                Hat = "Fedora",
                Glasses = "Circle",
                EyeColor = "Brown",
                HairColor = "Pink",
                FacialHair = null,
                Gender = "Female"
            },
            new Card() {
                Name = "Joseph",
                Hat = null,
                Glasses = "Square",
                EyeColor = "Blue",
                HairColor = "Brown",
                FacialHair = "Beard",
                Gender = "Male"
            }

            };
            return list;
        }
    }
    public class Card
    {
        public string Name { get; set; }
        public string Hat { get; set; }
        public string Glasses { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string FacialHair { get; set; }
        public string Gender { get; set; }
    }
}
