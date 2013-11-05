using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pang
{
    class Shot : GameObject
    {
        public ShotType Type { get; set; }

        public Shot(ShotType type)
        {
            this.Type = type;

            switch (type)
            {
                case ShotType.Normal:
                    this.Color = ConsoleColor.White;
                    this.Symbol = new char[,] { { '^' } };
                    break;
                case ShotType.MachineGune:
                    this.Color = ConsoleColor.Red;
                    this.Symbol = new char[,] { { '\'' }, { ' ' } , { '\'' } };
                    break;
                case ShotType.Laser:
                    this.Color = ConsoleColor.Yellow;
                    this.Symbol = new char[,] { { '|' }, { '|' } };
                    break;
                case ShotType.Hook:
                    this.Color = ConsoleColor.Green;
                    this.Symbol = new char[,] { { '^' } };
                    break;
                default:
                    break;
            }
        }
    }
}