using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Models
{
    public class Player : GameObject
    {
        public int Lives { get; set; } = 3;
        public bool IsExploding { get; set; } = false;
        public int ExplosionCounter { get; set; } = 0;
        public int XP { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int SkillPoints { get; set; } = 0;
        public Player() : base(350, 300, 40, 30) // Kezdőpozíció és méret
        {
        }

        public void MoveLeft()
        {
            if (!IsExploding && X > 0) X -= 10;
        }

        public void MoveRight(double screenWidth)
        {
            if (!IsExploding && X < screenWidth - Width) X += 10;
        }
    }    
}
