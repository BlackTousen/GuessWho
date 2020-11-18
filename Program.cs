using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
namespace GuessWho
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Guess Who!");
            Console.WriteLine("");
            List<Card> Board = CardList();
            Card cpu = Board[new Random().Next(0, Board.Count)];
            while (true)
            {
                foreach (Card card in Board)
                {
                    Console.WriteLine($@"Name: {card.Name}
Hat: {card.Hat}
Glasses: {card.Glasses}
Eye Color: {card.EyeColor}
Hair Color: {card.HairColor}
Facial Hair: {card.FacialHair}
");
                }
                int pick = Prompt();
                string property = Guess(pick);
                if (pick == 1 && property == cpu.Name.ToLower())
                {
                    Console.WriteLine("You did it! Great job!");
                    break;
                }
                Board = FilteredBoard(Board, pick, property, cpu);
            }

        }
        static int Prompt()
        {
            int selection = 0;
            while (selection < 1 || selection > 7)
            {
                Console.WriteLine(@"Choose an attribute to inquire about:
                1) Name
                2) Hat
                3) Glasses
                4) Eye Color
                5) Hair Color
                6) Facial Hair");
                if (!int.TryParse(Console.ReadLine(), out selection))
                {
                    Prompt();
                }
            }
            return selection;
        }
        static string Guess(int selection)
        {
            string choice = "";
            switch (selection)
            {
                case 1:
                    Console.Write("Who do you guess? ");
                    choice = Console.ReadLine().ToLower();
                    break;
                case 2:
                    while (choice != "cap" && choice != "fedora" && choice != "cowboy" && choice != "none")
                    {
                        Console.Write("What kind of hat do you think they might have? (cap, beanie, cowboy, none) ");
                        choice = Console.ReadLine().ToLower();
                    }
                    break;
                case 3:
                    while (choice != "circle" && choice != "square" && choice != "triangle" && choice != "none")
                    {
                        Console.Write("What kind of glasses do you think they might have? (circle, square, triangle, none) ");
                        choice = Console.ReadLine().ToLower();
                    }
                    break;
                case 4:
                    while (choice != "blue" && choice != "green" && choice != "brown")
                    {
                        Console.Write("What kind of eye color do you think they might have? (blue, green, brown) ");
                        choice = Console.ReadLine().ToLower();
                    }
                    break;
                case 5:
                    while (choice != "black" && choice != "brown" && choice != "blonde" && choice != "red" && choice != "none")
                    {
                        Console.Write("What kind of hair color do you think they might have? (black, brown, blonde, red, none) ");
                        choice = Console.ReadLine().ToLower();
                    }
                    break;
                case 6:
                    while (choice != "mustache" && choice != "beard" && choice != "goatee" && choice != "none")
                    {
                        Console.Write("What kind of facial hair do you think they might have? (mustache, beard, goatee, none) ");
                        choice = Console.ReadLine().ToLower();
                    }
                    break;
            }
            return choice;
        }
        static List<Card> FilteredBoard(List<Card> board, int pick, string property, Card cpu)
        {
            string key;
            switch (pick)
            {
                case 1:
                    key = "Name";
                    break;
                case 2:
                    key = "Hat";
                    break;
                case 3:
                    key = "Glasses";
                    break;
                case 4:
                    key = "EyeColor";
                    break;
                case 5:
                    key = "HairColor";
                    break;
                case 6:
                    key = "FacialHair";
                    break;
                default:
                    key = string.Empty;
                    break;

            }
            var test = cpu.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == key);
            var value = (test.GetValue(cpu)?.ToString() ?? string.Empty).ToLower();
            if (value == property)
            {
                return board.Where(card => test.GetValue(card)?.ToString().ToLower() == property).ToList();
            }
            return board.Where(card => test.GetValue(card)?.ToString().ToLower() != property).ToList();
        }
        static List<Card> CardList()
        {
            List<Card> list = new List<Card>() {
                new Card("Rick", "Cap", "None", "Blue", "Blonde", "None"),
                new Card("Brady", "Fedora", "Triangle", "Blue", "Blonde", "None"),
                new Card("Mori", "Fedora", "Triangle", "Brown", "Red", "None"),
                new Card("Hailey", "Cowboy", "None", "Brown", "Red", "None"),
                new Card("Hanako", "None", "Square", "Blue", "Brown", "None"),
                new Card("Marci", "Cap", "Circle", "Green", "Red", "None"),
                new Card("CJ", "None", "None", "Brown", "Black", "None") ,
                new Card("Joseph", "None", "Square", "Blue", "Brown", "Beard"),
                new Card("Matt", "None", "Circle", "Brown", "Brown", "Beard"),
                new Card("Joe", "Cowboy", "None", "Green", "Black", "Mustache")
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
        public Card(string name, string hat, string glasses, string eyeColor, string hairColor, string facialHair)
        {
            this.Name = name;
            this.Hat = hat;
            this.Glasses = glasses;
            this.EyeColor = eyeColor;
            this.HairColor = hairColor;
            this.FacialHair = facialHair;
        }
    }
}
