using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pang
{
    class Player : GameObject
    {
        public int Lives { get; set; }
        public int Score { get; set; }

        public Player()
        {
            this.Lives = 5;
            this.Score = 0;
            this.Color = ConsoleColor.Yellow;
            this.Symbol = new char[,]
                {
                    { '\\', ' ', '/' },
                    { 'o', '|', ' ' },
                    { '/', ' ', '\\' }
                };
        }
    }
}
