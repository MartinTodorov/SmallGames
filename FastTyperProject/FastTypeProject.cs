using System;
using System.Diagnostics;
using System.Threading;

class FastTypeProject
{
    static void Main()
    {
        string upLines = "===============================";
        string greeting = "  Welcome to The Fast Typing!  ";
        string lowLines = "===============================";
        Console.WriteLine("{0, 55}", upLines);
        Console.WriteLine("{0, 55}", greeting);
        Console.WriteLine("{0, 55}", lowLines);
        Console.WriteLine("\tYou think you're the fastest typewriter? Let's put that on test!\r\n");
        Console.WriteLine("    You will see a sentence. Remember it, then start typing as fast as you can. Your goal is to repeat the same sentence including capital letters and punctuation marks. As soon as you finish, press Enter again to see the results.\r\n");
        Console.WriteLine("  To begin, press Enter...");

        string firstSentence = "C# is intended to be a simple, modern, general-purpose, object-oriented programming language.";
        ConsoleKeyInfo enter = Console.ReadKey();
        if (enter.Key == ConsoleKey.Enter)
        {
            Console.WriteLine("You have 30 seconds to remember the following Sentence: ");

            Console.WriteLine(firstSentence);
        }

        byte time = 1;
        while (time < 30)
        {
            time++;
            Thread.Sleep(1000);
        }
        if (time == 30)
        {
            Console.Clear();
        }

        ConsoleKeyInfo again;
        byte gameNumber = 1;
        do
        {
            Console.WriteLine("\r\nWhen you're ready, press Enter and start typing...");
            Console.ReadLine();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            string userType = Console.ReadLine();
            timer.Stop();
            object userTime = timer.Elapsed;

            Console.WriteLine();
            if (userType == firstSentence)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Game# {0}. Well done!", gameNumber);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your time: {0}", userTime);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game# {0}. Sorry, but you didn't retype exactly the same sentence.", gameNumber);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your time is: {0}\r\n", userTime);
                Console.ResetColor();
            }

            gameNumber++;
            Console.Write("Wanna try again? (y/n)");
            again = Console.ReadKey();
        }
        while (again.KeyChar == 'y');
    }
}