using System;
using System.Collections;
using System.Collections.Generic;
namespace GuessWho
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Guess Who!");
            List<Card> Board = CardList();
            Card cpu = Board[new Random().Next(0, Board.Count)];
            while (true)
            {
                int pick = Prompt();
                string property = Guess(pick);
                if (pick == 1 && property == cpu.Attributes["Name"].ToLower())
                {
                    Console.WriteLine("You did it! Great job!");
                    break;
                }
                Board = FilteredBoard(Board, pick, property, cpu);
                foreach (Card card in Board)
                {
                    Console.WriteLine(card.Attributes["Name"]);
                }
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
                6) Facial Hair
                7) Gender");
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
                        Console.Write("What kind of glasses do you think they might have? (round, square, oval, none) ");
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
                case 7:
                    while (choice != "male" && choice != "female")
                    {
                        Console.Write("What gender do you think they might be? (male, female) ");
                        choice = Console.ReadLine().ToLower();
                    }
                    break;
            }
            return choice;
        }
        static List<Card> FilteredBoard(List<Card> board, int pick, string property, Card cpu)
        {
            string key = "";
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
                case 7:
                    key = "Gender";
                    break;

            }
            if (cpu.Attributes[key].ToLower() == property)
            {
                List<Card> filtered = board.FindAll(card => card.Attributes[key].ToLower() == property);
                return filtered;
            }
            else
            {
                List<Card> filtered = board.FindAll(card => card.Attributes[key].ToLower() != property);
                return filtered;
            }
        }
        static List<Card> CardList()
        {
            List<Card> list = new List<Card>() {
                new Card("Rick", "Cap", "None", "Blue", "Blonde", "None", "Male"),
                new Card("Mori", "Fedora", "Triangle", "Brown", "Red", "None", "Female"),
                new Card("Joseph", "None", "Square", "Blue", "Brown", "Beard", "Male")
            };
            return list;
        }
    }
    public class Card
    {
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        public Card(string name, string hat, string glasses, string eyeColor, string hairColor, string facialHair, string gender)
        {
            Attributes.Add("Name", name);
            Attributes.Add("Hat", hat);
            Attributes.Add("Glasses", glasses);
            Attributes.Add("EyeColor", eyeColor);
            Attributes.Add("HairColor", hairColor);
            Attributes.Add("FacialHair", facialHair);
            Attributes.Add("Gender", gender);
        }
    }
}
