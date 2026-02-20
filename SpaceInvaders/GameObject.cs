using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Models
{
    // Absztrakt, mert önmagában "GameObject"-et nem akarunk létrehozni, csak konkrét leszármazottakat
    public abstract class GameObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        // Segédfüggvény ütközésvizsgálathoz
        public Rect Bounds
        {
            get { return new Rect(X, Y, Width, Height); }
        }

        protected GameObject(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
    }
}
