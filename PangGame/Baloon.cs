using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pang
{
    class Baloon : GameObject
    {
        public const char BC = 'O';
        public const char BE = ' ';
        public BaloonSize Size { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }

        public Baloon(BaloonSize size, int xDir, int yDir)
        {
            this.Size = size;
            this.XDirection = xDir;
            this.YDirection = yDir;

            switch (size)
            {
                case BaloonSize.Small: 
                    this.Color = ConsoleColor.White;
                    this.Symbol = new char[,] 
                        { 
                            { BC }
                        };
                    break;
                case BaloonSize.Medium: 
                    this.Color = ConsoleColor.Green;
                    this.Symbol = new char[,] 
                        { 
                            { BE, BC, BE },
                            { BC, BC, BC },
                            { BE, BC, BE },
                        };
                    break;
                case BaloonSize.Large: 
                    this.Color = ConsoleColor.DarkYellow;
                    this.Symbol = new char[,] 
                        { 
                            { BE, BE, BC, BE, BE },
                            { BE, BC, BC, BC, BE },
                            { BC, BC, BC, BC, BC },
                            { BE, BC, BC, BC, BE },
                            { BE, BE, BC, BE, BE },
                        };
                    break;
                case BaloonSize.Giant: 
                    this.Color = ConsoleColor.Yellow;
                    this.Symbol = new char[,] 
                        { 
                            { BE, BE, BE, BC, BE, BE, BE },
                            { BE, BE, BC, BC, BC, BE, BE },
                            { BE, BC, BC, BC, BC, BC, BE },
                            { BC, BC, BC, BC, BC, BC, BC },
                            { BE, BC, BC, BC, BC, BC, BE },
                            { BE, BE, BC, BC, BC, BE, BE },
                            { BE, BE, BE, BC, BE, BE, BE },
                        };
                    break;
                default: break;
            }
        }
    }
}