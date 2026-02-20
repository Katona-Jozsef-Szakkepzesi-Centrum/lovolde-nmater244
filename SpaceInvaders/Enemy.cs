using SpaceInvaders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Enemy : GameObject
    {        
        public Enemy(double x, double y) : base(x, y, 40, 30)
        {
        }

        public void Enemy_Move(int irany, double Esemeny)
        {
            switch (irany)
            {
                case 0:
                    Y -= 4;
                    break;
                case 1:
                    Y += 4;
                    break;
                case 2:
                    X += 4;
                    break;
                case 3:
                    X -= 4;
                    break;
                default: return;
            }
            if (Esemeny > 0 && Esemeny < 30  )             
            {
                MoveDown();
            }

        }
        public void MoveDown()
        {
            if (Y <= 600)
            {
                Y += 4; // Sebesség
            }
        }
    }
}
