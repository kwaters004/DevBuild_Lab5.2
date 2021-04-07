using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab5._2
{

    enum Roshambo
    {
        rock,
        paper,
        scissors
    }

    abstract class Player
    {
        public string name { get; set; }
        //private Roshambo rkpprsci;



        public abstract Roshambo GenerateRoshambo(Random rnd);
    }

    class Brock : Player // This is the player that will always choose Rock
    {
        public string name = "Brock";

        public override Roshambo GenerateRoshambo(Random rnd)
        {
            return new Roshambo();
        }
    }

    class Misty : Player
    {
        public string name = "Misty";
        public override Roshambo GenerateRoshambo(Random rnd)
        {
            int rndNum = rnd.Next(0, 3); // needs to be 1 higher than the actual values returned
            return (Roshambo)rndNum;
        }
    }

    class Human : Player
    {
        public string name;
        public Human(string Name)
        {
            name = Name;
        }

        public override Roshambo GenerateRoshambo(Random rnd)
        {
            int humanNum = 0;
            bool valid = false;
            while (!valid)
            {

                Console.Write("Rock, Paper, or Scissors? ");
                string humanChoice = Console.ReadLine().ToLower();

                if (humanChoice == "rock" || humanChoice == "paper" || humanChoice == "scissors")
                {
                    valid = true;
                    if (humanChoice == "rock")
                    {
                        humanNum = 0;
                    }
                    else if (humanChoice == "paper")
                    {
                        humanNum = 1;
                    }
                    else
                    {
                        humanNum = 2;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, that wasn't a valid option. Please try again");
                }
            }


            return (Roshambo)humanNum;
        }
    }

    class Program
    {
        static Dictionary<string, int> WinTracker(Dictionary<string, int> winners, string winner)
        {
            if (winners.ContainsKey(winner))
            {
                winners[winner] += 1;
            }
            else
            {
                winners.Add(winner, 1);
            }
            return winners;
        }

        static bool Continue()
        {
            bool validAns = false;
            while (!validAns)
            {

                Console.Write("\nWould you like to continue? (y/n) ");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y" || ans == "n")
                {
                    validAns = true;
                    if (ans == "n")
                    {
                        return true;
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, that was not a valid option. Please try again.");
                }
            }
            return false;
        }
        static Roshambo GetOpponentPick(ref string oppName, Random rnd)
        {
            Brock opponent = new Brock();
            Misty opponent2 = new Misty();


            bool validName = false;
            while (!validName)
            {
                Console.Write("Would you like to play against Brock or Misty? ");
                oppName = Console.ReadLine().ToLower();

                if (oppName == opponent.name.ToLower() || oppName == opponent2.name.ToLower())
                {
                    validName = true;
                    if (oppName == opponent.name.ToLower())
                    {
                        return opponent.GenerateRoshambo(rnd);

                    }
                    else
                    {
                        return opponent2.GenerateRoshambo(rnd);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, that was not a valid option. Please try again");
                }
            }

            return new Roshambo();
        }
        static string DetermineWinner(Roshambo playerPick, Roshambo opponentPick, string oppName, string playerName)
        {
            if (playerPick == opponentPick)
            {
                return ("It's a tie!!");
            }
            else if (playerPick == Roshambo.rock)
            {

                if (opponentPick == Roshambo.paper)
                {
                    return ($"{oppName} Wins!");
                }
                else
                {
                    return ($"{playerName} Wins!");
                }
            }
            else if (playerPick == Roshambo.paper)
            {
                if (opponentPick == Roshambo.rock)
                {
                    return ($"{playerName} Wins!");
                }
                else
                {
                    return ($"{oppName} Wins!");
                }
            }
            else if (playerPick == Roshambo.scissors)
            {
                if (opponentPick == Roshambo.rock)
                {
                    return ($"{oppName} Wins!");
                }
                else
                {
                    return ($"{playerName} Wins!");
                }
            }
            else
            {
                return "";
            }

        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Dictionary<string, int> wins = new Dictionary<string, int>();

            Console.Write("What is your name? ");
            Human player = new Human(Console.ReadLine());


            bool done = false;
            while (!done)
            {
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;

                Roshambo opponentPick = (Roshambo)0;
                string oppName = "";
                opponentPick = GetOpponentPick(ref oppName, rnd);
                
                Roshambo playerPick = player.GenerateRoshambo(rnd);

                oppName = ti.ToTitleCase(oppName);
                Console.WriteLine($"\n{player.name} picked {playerPick}");
                Console.WriteLine($"{oppName} picked {opponentPick}\n");

                string winner = DetermineWinner(playerPick, opponentPick, oppName, player.name);
                WinTracker(wins, winner);
                Console.WriteLine(winner);

                done = Continue();

            }

            foreach (KeyValuePair<string, int> pairs in wins)
            {
                if (pairs.Key.Contains("tie"))
                {
                    Console.WriteLine($"Ties: {pairs.Value}");
                }
                else
                {
                    Console.WriteLine($"{pairs.Key.Substring(0, pairs.Key.Length - 1)}: {pairs.Value}");
                }
            }




        }
    }
}
