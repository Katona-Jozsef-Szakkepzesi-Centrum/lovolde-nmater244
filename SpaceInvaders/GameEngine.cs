using System.Collections.Generic;
using SpaceInvaders.Models;
using System.Linq;
using SpaceInvaders;

public class GameEngine
{
    public Player Player { get; set; }
    public List<Enemy> Enemies { get; set; }
    public List<Bullet> Bullets { get; set; }

    public int Score { get; set; } = 0; 

    public GameEngine()
    {
        Player = new Player();
        Enemies = new List<Enemy>();
        Bullets = new List<Bullet>();

        // Ellenségek generálása (pl. egy sorban)
        for (int i = 0; i < 10; i++)
        {
            Enemies.Add(new Enemy(i * 60 + 50, 50));
        }
    }

    // Ez fut le minden "képkockánál" (Tick)
    public void GameTick()
    {
        //Ha robban a játékos
        if (Player.IsExploding) 
        {
            Player.ExplosionCounter--;
            //ha lejárt a robbanás ideje
            if (Player.ExplosionCounter <= 0)
            {
                Player.IsExploding = false;

                //visszarakjuk az új játékost középre
                Player.X = 350;
            }
        }
        // 1. Lövedékek mozgatása
        foreach (var bullet in Bullets)
        {
            bullet.MoveUp();
        }

        // 2. Töröljük, ami kiment a képernyőről (LINQ használata - jó tananyag!)
        Bullets.RemoveAll(b => b.Y < -100);

        // 3. Ellenségek mozgatása
        foreach (var enemy in Enemies)
        {
            Random random = new Random();
            Random rnd_esemeny = new Random();
            if (enemy.Y > 610)
            {
                enemy.Y = -10;
                enemy.X = random.Next(20, 780);
            }
            
            enemy.Enemy_Move(random.Next(0, 4), rnd_esemeny.Next(0, 100));
            //enemy.MoveDown();
        }

        // 4. Ütközésvizsgálat (Collision Detection)
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        // Egyszerű példa: Ha a lövedék eltalálja az ellenséget
        // Hátrafelé iterálunk, hogy törölni tudjunk a listából hiba nélkül
        for (int i = Bullets.Count - 1; i >= 0; i--)
        {
            for (int j = Enemies.Count - 1; j >= 0; j--)
            {
                if (Bullets[i].Bounds.IntersectsWith(Enemies[j].Bounds))
                {
                    Enemies.RemoveAt(j);
                    Bullets.RemoveAt(i);
                    break; // Egy golyó csak egyet talál el
                }
            }
        }
        //Játékos Ellenség ütközés ha a játékos nem halhatatlan
        if (Player.IsExploding)
        {
            foreach (var enemy in Enemies)
            {
                if (Player.Bounds.IntersectsWith(enemy.Bounds))
                {
                    TriggerPlayerDeath();
                    break;
                }
            }
        }
    }
    private void TriggerPlayerDeath()
    {
        Player.Lives--;

        if ( Player.Lives > 0)
        {
            Player.IsExploding = true;
            Player.ExplosionCounter = 150;
        }
        else
        {
            //játék vége
        }
    }
}
