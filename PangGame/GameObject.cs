using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pang
{
    abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public char[,] Symbol { get; set; }
    }
}