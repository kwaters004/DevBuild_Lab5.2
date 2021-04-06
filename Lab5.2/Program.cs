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
        //private string name;
        //private Roshambo rkpprsci;

        public abstract Roshambo GenerateRoshambo(Random rnd);
    }

    class Brock : Player // This is the player that will always choose Rock
    {
        public override Roshambo GenerateRoshambo(Random rnd)
        {
            return new Roshambo();
        }
    }

    class Misty : Player
    {
        public override Roshambo GenerateRoshambo(Random rnd)
        {
            int rndNum = rnd.Next(0, 3); // needs to be 1 higher than the actual values returned
            return (Roshambo)rndNum;
        }
    }

    class Human : Player
    {
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
        static Roshambo GetOpponentPick(ref string oppName, Player opponent, Player opponent2, Random rnd)
        {

            bool validName = false;
            while (!validName)
            {
                Console.Write("Would you like to play against Brock or Misty? ");
                oppName = Console.ReadLine().ToLower();

                if (oppName == "brock" || oppName == "misty")
                {
                    validName = true;
                    if (oppName == "brock")
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
            else if (playerPick == 0)
            {

                if ((int)opponentPick == 1)
                {
                    return ($"{oppName} Wins!");
                }
                else
                {
                    return ($"{playerName} Wins!");
                }
            }
            else if ((int)playerPick == 1)
            {
                if (opponentPick == 0)
                {
                    return ($"{playerName} Wins!");
                }
                else
                {
                    return ($"{oppName} Wins!");
                }
            }
            else if ((int)playerPick == 2)
            {
                if (opponentPick == 0)
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
            string playerName = Console.ReadLine();
            Brock opponent = new Brock();
            Misty opponent2 = new Misty();

            bool done = false;
            while (!done)
            {
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;

                Roshambo opponentPick = (Roshambo)0;
                string oppName = "";
                opponentPick = GetOpponentPick(ref oppName, opponent, opponent2, rnd);
                Human player = new Human();
                Roshambo playerPick = player.GenerateRoshambo(rnd);

                oppName = ti.ToTitleCase(oppName);
                Console.WriteLine($"\n{playerName} picked {playerPick}");
                Console.WriteLine($"{oppName} picked {opponentPick}\n");

                string winner = DetermineWinner(playerPick, opponentPick, oppName, playerName);
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
