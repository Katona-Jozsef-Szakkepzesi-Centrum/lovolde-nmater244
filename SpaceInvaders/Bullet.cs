using SpaceInvaders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Models
{
    public class Bullet : GameObject
    {
        public Bullet(double x, double y) : base(x, y, 5, 15)
        {
        }

        public void MoveUp()
        {
            Y -= 15; // Gyorsabb, mint az ellenség
        }
    }
}
