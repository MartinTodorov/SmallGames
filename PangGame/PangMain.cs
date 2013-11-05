using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace Pang
{
    class PangMain
    {
        public const int PlayFieldWidth = 100;
        public const int PlayFieldHeight = 35;
        public const int GroundOffset = 4;
        private static Player playerCharacter;
        private static int level = 1;
        private static ShotType shotType = ShotType.Laser;
        private static bool canShoot = true;
        private static DateTime shotEnd = DateTime.Now;
        private static List<Baloon> baloons = new List<Baloon>();
        private static List<Shot> shots = new List<Shot>();

        static void Main()
        {
            // Setup console
            SetConsoleDimensions();

            //Start screen method
            StartPage();
            
            // Init player
            InitObjects(baloons);

            while (true)
            {
                // Move player (key pressed)
                if (Console.KeyAvailable)
                {
                    MovePlayer(playerCharacter, shots);
                }
                
                // Move baloons
                MoveBaloons(baloons);

                // Move shots
                MoveShots(shots);

                // Clear the console
                Console.Clear();

                // Draw baloons
                DrawBaloons(baloons);

                // Draw shots
                DrawShots(shots);

                // Draw player
                DrawPlayer(playerCharacter);

                // Draw info
                PrintInfo();

                // Check for collisions
                CheckForCollisionOfBaloonWithPlayer(playerCharacter);
                
                // Check for collisions shots with baloons
                CheckForCollisionOfBaloonWithShot();

                // Slow down the program
                Thread.Sleep(200);
            }
        }

        private static void CheckForCollisionOfBaloonWithShot()
        {
            bool breakLoop = false;

            foreach (Baloon baloon in baloons)
            {
                foreach (Shot shot in shots)
                {
                    if (shot.Type == ShotType.Normal || shot.Type == ShotType.Hook)
                    {
                        if (baloon.X + baloon.Symbol.GetLength(0) >= shot.X && baloon.X <= shot.X + shot.Symbol.GetLength(0)
                         && baloon.Y + baloon.Symbol.GetLength(1) >= shot.Y)
                        {
                            // Collision
                            Console.Beep();

                            if (baloon.Size != BaloonSize.Small)
                            {
                                SpawnTwoSmallerBaloons(baloon);
                            }

                            baloons.Remove(baloon);
                            shots.Remove(shot);
                            breakLoop = true;
                            break;
                        }
                    }
                    else if (shot.Type == ShotType.Laser)
                    {
                        if (baloon.X + baloon.Symbol.GetLength(0) >= shot.X && baloon.X <= shot.X + shot.Symbol.GetLength(0))
                        {
                            // Collision
                            Console.Beep();

                            if (baloon.Size != BaloonSize.Small)
                            {
                                SpawnTwoSmallerBaloons(baloon);
                            }

                            baloons.Remove(baloon);
                            shots.Remove(shot);
                            breakLoop = true;
                            break;
                        }
                    }
                    else if (shot.Type == ShotType.MachineGune)
                    {
                        if (baloon.X + baloon.Symbol.GetLength(0) >= shot.X && baloon.X <= shot.X + shot.Symbol.GetLength(0)
                         && baloon.Y + baloon.Symbol.GetLength(1) >= shot.Y && baloon.Y <= shot.Y + shot.Symbol.GetLength(1))
                        {
                            // Collision
                            Console.Beep();

                            if (baloon.Size != BaloonSize.Small)
                            {
                                SpawnTwoSmallerBaloons(baloon);
                            }

                            baloons.Remove(baloon);
                            shots.Remove(shot);
                            breakLoop = true;
                            break;
                        }
                    }
                }

                if (breakLoop)
                {
                    break;
                }
            }
        }
  
        private static void SpawnTwoSmallerBaloons(Baloon baloon)
        {
            BaloonSize newSize = BaloonSize.Giant;

            if (baloon.Size == BaloonSize.Giant)
            {
                newSize = BaloonSize.Large;
            }
            else if (baloon.Size == BaloonSize.Large)
            {
                newSize = BaloonSize.Medium;
            }
            else if (baloon.Size == BaloonSize.Medium)
            {
                newSize = BaloonSize.Small;
            }

            int newXOne = baloon.X - 10;
            int newXTwo = baloon.X + 10;
            Baloon newBaloonOne = new Baloon(newSize, 1, 3);
            newBaloonOne.X = newXOne;
            newBaloonOne.Y = baloon.Y;
            Baloon newBaloonTwo = new Baloon(newSize, 1, -3);
            newBaloonTwo.X = newXTwo;
            newBaloonTwo.Y = baloon.Y;
            baloons.Add(newBaloonOne);
            baloons.Add(newBaloonTwo);
        }
  
        private static void InitObjects(List<Baloon> baloons)
        {
            // Init player
            playerCharacter = new Player();
            playerCharacter.X = PlayFieldWidth / 2 - 1;
            playerCharacter.Y = PlayFieldHeight - GroundOffset - playerCharacter.Symbol.GetLength(1);
              
            // Init baloons
            Baloon newBaloon = new Baloon(BaloonSize.Large, 1, 3);
            newBaloon.X = 20;
            newBaloon.Y = 10;
            baloons.Add(newBaloon);
            newBaloon = new Baloon(BaloonSize.Medium, 1, 3);
            newBaloon.X = 30;
            newBaloon.Y = 10;
            baloons.Add(newBaloon);
            newBaloon = new Baloon(BaloonSize.Medium, -1, 3);
            newBaloon.X = 10;
            newBaloon.Y = 10;
            baloons.Add(newBaloon);
            newBaloon = new Baloon(BaloonSize.Giant, -1, 3);
            newBaloon.X = 10;
            newBaloon.Y = 10;
            baloons.Add(newBaloon);
        }

        private static void MoveShots(List<Shot> shots)
        {
            switch (shotType)
            {
                case ShotType.Normal:
                    foreach (Shot shot in shots)
                    {
                        if (shot.Y - 2 < 0)
                        {
                            shots.Clear();
                            break;
                        }
                        else
                        {
                            shot.Y -= 2;
                        }
                    }
                    break;
                case ShotType.MachineGune:
                    foreach (Shot shot in shots)
                    {
                        if (shot.Y == 0)
                        {
                            shot.Symbol = new char[,] { { ' ' } };
                            continue;
                        }

                        if (shot.Y - 2 == -1)
                        {
                            shot.Y = 0;                            
                        }
                        else if (shot.Y - 2 <= -2)
                        {
                            shots.Remove(shot);

                            if (shots.Count == 0)
                            {
                                break;
                            }
                        }

                        shot.Y -= 2;
                    }
                    break;
                case ShotType.Laser:
                    if (DateTime.Now >= shotEnd)
                    {
                        shots.Clear();
                        break;
                    }
                    break;
                case ShotType.Hook:
                    foreach (Shot shot in shots)
                    {
                        if (shot.Y - 2 < 0)
                        {
                            shot.Y = 0;

                            if (DateTime.Now >= shotEnd)
                            {
                                shots.Clear();
                            }

                            break;
                        }
                        else
                        {
                            shot.Y -= 2;
                        }
                    }
                    break;
                default: break;
            }
        }

        private static void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintStringOnPosition(5, PlayFieldHeight - 2, "Lives: " + playerCharacter.Lives);
            PrintStringOnPosition(30, PlayFieldHeight - 2, "Score: " + playerCharacter.Score);
            PrintStringOnPosition(55, PlayFieldHeight - 2, "Level: " + level);
            PrintStringOnPosition(80, PlayFieldHeight - 2, "Weapon: " + shotType);
            Console.ForegroundColor = ConsoleColor.Red;
            PrintStringOnPosition(0, PlayFieldHeight - GroundOffset, new string('=', PlayFieldWidth));
        }

        private static void PrintStringOnPosition(int x, int y, string info)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(info);
        }

        private static void CheckForCollisionOfBaloonWithPlayer(Player player)
        {
            foreach (Baloon baloon in baloons)
            {
                if (baloon.X + baloon.Symbol.GetLength(0) >= player.X && baloon.X <= player.X + player.Symbol.GetLength(0)
                 && baloon.Y + baloon.Symbol.GetLength(1) >= player.Y && baloon.Y <= player.Y + player.Symbol.GetLength(1))
                {
                    // Collision
                    player.Lives--;
                    Console.Beep();

                    if (player.Lives < 0)
                    {
                        GameOver();
                    }

                    player.X = 50;
                }
            }
        }

        private static void GameOver()
        {
            //TODO: Game Over Screen
            throw new NotImplementedException();
        }

        private static void DrawPlayer(Player playerCharacter)
        {
            // Draw player
            VisualizeGameObject(playerCharacter.X, playerCharacter.Y, playerCharacter.Symbol, playerCharacter.Color);
        }
  
        private static void DrawShots(List<Shot> shots)
        {
            // Draw shots
            if (shots.Count >= 1)
            {
                switch (shotType)
                {
                    case ShotType.Normal:
                        VisualizeGameObject(shots[0].X, shots[0].Y, shots[0].Symbol, shots[0].Color);
                        VisualizeGameObject(shots[0].X, shots[0].Y + 1, new char[,] { { '|' } }, shots[0].Color);

                        for (int y = shots[0].Y + 2; y < PlayFieldHeight - GroundOffset; y++)
                        {
                            if (y % 2 == 0)
                            {
                                VisualizeGameObject(shots[0].X, y, new char[,] { { '/' } }, shots[0].Color);
                            }
                            else
                            {
                                VisualizeGameObject(shots[0].X, y, new char[,] { { '\\' } }, shots[0].Color);
                            }
                        }
                        break;
                    case ShotType.MachineGune:
                        foreach (Shot shot in shots)
                        {
                            VisualizeGameObject(shot.X, shot.Y, shot.Symbol, shot.Color);
                        }
                        break;
                    case ShotType.Laser:
                        for (int y = 0; y < PlayFieldHeight - GroundOffset; y++)
                        {
                            VisualizeGameObject(shots[0].X, y, shots[0].Symbol, shots[0].Color);
                        }
                        break;
                    case ShotType.Hook:
                        VisualizeGameObject(shots[0].X, shots[0].Y, shots[0].Symbol, shots[0].Color);
                        VisualizeGameObject(shots[0].X, shots[0].Y + 1, new char[,] { { '|' } }, shots[0].Color);

                        for (int y = shots[0].Y + 2; y < PlayFieldHeight - GroundOffset; y++)
                        {
                            VisualizeGameObject(shots[0].X, y, new char[,] { { '|' } }, shots[0].Color);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
  
        private static void DrawBaloons(List<Baloon> baloons)
        {
            // Draw baloons
            foreach (Baloon baloon in baloons)
            {
                VisualizeGameObject(baloon.X, baloon.Y, baloon.Symbol, baloon.Color);
            }
        }

        private static void MoveBaloons(List<Baloon> baloons)
        {
            // Move baloons
            foreach (Baloon baloon in baloons)
            {
                baloon.Y += baloon.YDirection;
                baloon.X += baloon.XDirection;

                if (baloon.X + baloon.Symbol.GetLength(0) >= PlayFieldWidth)
                {
                    baloon.X = PlayFieldWidth - baloon.Symbol.GetLength(0);
                    baloon.XDirection *= -1;
                }

                if (baloon.X <= 0)
                {
                    baloon.X = 0;
                    baloon.XDirection *= -1;
                }

                if (baloon.Y + baloon.Symbol.GetLength(1) >= PlayFieldHeight - GroundOffset)
                {
                    baloon.Y = PlayFieldHeight - baloon.Symbol.GetLength(1) - GroundOffset;
                    baloon.YDirection *= -1;
                }

                if (baloon.Y <= 0)
                {
                    baloon.Y = 0;
                    baloon.YDirection *= -1;
                }
            }
        }

        private static void MovePlayer(Player user, List<Shot> shots)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                if (user.X - user.Symbol.GetLength(0) >= 0)
                {
                    user.X -= user.Symbol.GetLength(0);
                }
                else
                {
                    user.X = 0;
                }
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                if (user.X + user.Symbol.GetLength(0) <= Console.WindowWidth - user.Symbol.GetLength(0))
                {
                    user.X += user.Symbol.GetLength(0);
                }
                else
                {
                    user.X = Console.WindowWidth - user.Symbol.GetLength(0);
                }
            }
            else if (pressedKey.Key == ConsoleKey.Spacebar)
            {
                if (canShoot)
                {
                    Shot newShot = new Shot(shotType);
                    newShot.X = user.X + 1;
                    newShot.Y = user.Y - 1;
                    shots.Add(newShot);
                }

                if (shotType == ShotType.Hook)
                {
                    shotEnd = DateTime.Now.AddSeconds(PlayFieldHeight / 5);
                }
                else if (shotType == ShotType.Laser)
                {
                    shotEnd = DateTime.Now.AddSeconds(.5);
                }
            }
        }

        private static void VisualizeGameObject(int xPosition, int yPosition, char[,] symbol, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < symbol.GetLength(0); i++)
            {
                for (int j = 0; j < symbol.GetLength(1); j++)
                {
                    Console.SetCursorPosition(xPosition + i, yPosition + j);
                    Console.Write(symbol[i, j]);
                }
            }
        }

        private static void SetConsoleDimensions()
        {
            Console.WindowWidth = PlayFieldWidth;
            Console.WindowHeight = PlayFieldHeight;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void StartPage()
        {
            StreamReader startPageTxt = new StreamReader(@"..\..\startScreen.txt");

            using (startPageTxt)
            {
                for (int i = 0; i < PlayFieldHeight; i++)
                {
                    string read = startPageTxt.ReadLine();
                    Console.WriteLine(read);
                }

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    default: 
                        Console.WriteLine("Add 1, 2 or 3."); 
                        break;
                }
            }
        }
    }
}