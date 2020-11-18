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
            Console.WriteLine("Guess Who?");
            Console.WriteLine("");
            string mode = ChooseMode();
            int totalClues = NumClues(mode);
            List<Card> Board = CardList();
            Card cpu = Board[new Random().Next(0, Board.Count)];
            while (totalClues > 0 || totalClues < 0)
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
                if (totalClues > 0)
                {
                    Console.WriteLine($"Remaining clues: {totalClues}");
                }
                if (totalClues == 1)
                {
                    Console.WriteLine("This is your last chance! Guess someone or you'll surely lose!");
                }
                int pick = Prompt();
                string property = Guess(pick);
                if (pick == 1 && property == cpu.Name.ToLower())
                {
                    Console.WriteLine("You did it! Great job!");
                    break;
                }
                Board = FilteredBoard(Board, pick, property, cpu);
                totalClues--;
            }
            Console.WriteLine("That's not who! You lose!");

        }
        static string ChooseMode()
        {
            string mode = "";
            while (mode != "easy" && mode != "medium" && mode != "hard" && mode != "cheat")
            {
                Console.Write("Please choose a difficulty setting (easy, medium, hard): ");
                mode = Console.ReadLine();
            }
            return mode;
        }
        static int NumClues(string mode)
        {
            int numOfClues = 0;
            switch (mode)
            {
                case "easy":
                    numOfClues = 8;
                    break;
                case "medium":
                    numOfClues = 6;
                    break;
                case "hard":
                    numOfClues = 4;
                    break;
                case "cheat":
                    numOfClues = -1;
                    break;
            }
            return numOfClues;
        }
        static int Prompt()
        {
            int selection = 0;
            while (selection < 1 || selection > 6)
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
                new Card("Rick", "Cap", "None", "Blue", "Blonde", "Beard"),
                new Card("Brady", "Fedora", "Triangle", "Brown", "Blonde", "Beard"),
                new Card("Mori", "Fedora", "Triangle", "Blue", "Red", "Goatee"),
                new Card("CJ", "Cap", "None", "Brown", "Black", "None") ,
                new Card("Joseph", "None", "None", "Blue", "Red", "Beard"),
                new Card("Matt", "None", "Triangle", "Brown", "Brown", "Beard"),
                new Card("Joe", "Cowboy", "Square", "Green", "Brown", "Goatee"),
                new Card("Faith", "None", "Circle", "Green", "Black", "None"),
                new Card("Sam", "Fedora", "Square", "Blue", "Brown", "Mustache"),
                new Card("Jerry", "Cowboy", "Square", "Brown", "Black", "Goatee"),
                new Card("Terra", "Fedora", "None", "Green", "Blonde", "None"),
                new Card("Tristan", "Cap", "Triangle", "Green", "Blonde", "Mustache"),
                new Card("Austin", "Cowboy", "Circle", "Blue", "Black", "Mustache"),
                new Card("Starkey", "Fedora", "Circle", "Blue", "Red", "None"),
                new Card("Abdu", "Cowboy", "None", "Brown", "Black", "Beard"),
                new Card("Travis", "None", "Circle", "Brown", "Brown", "Goatee"),
                new Card("Lacey", "None", "Square", "Green", "Red", "None"),
                new Card("Parker", "Cap", "Triangle", "Blue", "Blonde", "Goatee"),
                new Card("Braxton", "Cap", "Circle", "Brown", "Brown", "Mustache"),
                new Card("Ember", "Cowboy", "None", "Green", "Red", "Mustache"),
                new Card("Adam", "Fedora", "Circle", "Green", "None", "Goatee"),
                new Card("Rose", "Cap", "Square", "Brown", "None", "None"),
                new Card("Erik", "None", "Square", "Blue", "None", "Beard"),
                new Card("Sage", "Cowboy", "Triangle", "Green", "None", "Mustache")
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
            Name = name;
            Hat = hat;
            Glasses = glasses;
            EyeColor = eyeColor;
            HairColor = hairColor;
            FacialHair = facialHair;
        }
    }
}
