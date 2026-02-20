using SpaceInvaders.Models;
using WpfAnimatedGif;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; // DispatcherTimerhez

namespace SpaceInvaders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameEngine engine;
        DispatcherTimer timer;

        // Ez a varázslat: összekötjük a logikai modellt a képernyőn lévő képpel
        Dictionary<GameObject, Image> sprites = new Dictionary<GameObject, Image>();
        public MainWindow()
        {
            InitializeComponent();
            engine = new GameEngine();

            // Időzítő beállítása (pl. 60 FPS -> kb 16ms)
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // A Játék Ciklus (Game Loop)
        private void Timer_Tick(object sender, EventArgs e)
        {
            engine.GameTick(); // Logika frissítése
            Draw();            // Képernyő frissítése
        }

        // Billentyűzet kezelés
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Left)
                engine.Player.MoveLeft();

            if (e.Key == System.Windows.Input.Key.Right)
                engine.Player.MoveRight(GameCanvas.ActualWidth);

            if (e.Key == System.Windows.Input.Key.Space)
            {
                // Lövés hozzáadása a motorhoz
                // A lövedék a játékos közepéről induljon
                double bulletX = engine.Player.X + engine.Player.Width / 2 - 2.5;
                engine.Bullets.Add(new SpaceInvaders.Models.Bullet(bulletX, engine.Player.Y));
            }
            if (e.Key == Key.D) // "D" mint Damage (Sérülés)
            {
                if (engine.Lives > 0) engine.Lives--;
            }
            if (e.Key == Key.S) // "S" mint Score
            {
                engine.Score += 100;
            }
        }

        // Kirajzolás
        private void Draw()
        {
            // 1. A meglévő sprite-kezelő kód (ez MARAD, ahogy volt)
            // ... (List<GameObject> allCurrentObjects = ...)
            // ... (foreach loop létrehozásra és frissítésre ...)
            // ... (foreach loop törlésre ...)


            // 2. ÚJ RÉSZ: A HUD frissítése (Pontszám és Életek)

            // Pontszám frissítése
            ScoreText.Text = $"Score: {engine.Score}";

            // Életek frissítése
            UpdateLivesDisplay();
        }

        // Ez az új metódus kezeli a bal felső sarokban lévő kis űrhajókat
        private void UpdateLivesDisplay()
        {
            // Csak akkor rajzoljuk újra, ha változott az életek száma a képernyőn lévőhöz képest
            // (Így nem villog, és nem fogyaszt feleslegesen erőforrást)
            if (LivesPanel.Children.Count != engine.Lives)
            {
                LivesPanel.Children.Clear(); // Töröljük a régieket

                for (int i = 0; i < engine.Lives; i++)
                {
                    Image lifeIcon = new Image();
                    lifeIcon.Width = 30;  // Kicsit kisebb legyen, mint a játékban
                    lifeIcon.Height = 20;
                    lifeIcon.Margin = new Thickness(5, 0, 0, 0); // Kis térköz

                    // Kép betöltése (Ugyanaz, mint a CreateSprite-nál)
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    // Itt a 'SpaceInvaders' helyére a TE projekted neve kell!
                    bmp.UriSource = new Uri($"pack://application:,,,/SpaceInvaders;component/Images/player.gif");
                    bmp.EndInit();

                    // Animáció bekapcsolása
                    WpfAnimatedGif.ImageBehavior.SetAnimatedSource(lifeIcon, bmp);

                    // Hozzáadjuk a felső panelhez
                    LivesPanel.Children.Add(lifeIcon);
                }
            }
        }

        // Segédfüggvény a képek betöltéséhez
        private Image CreateSprite(GameObject model)
        {
            Image img = new Image();

            // --- EZ A RÉSZ HIÁNYZOTT NÁLAD ---
            // 1. Beállítjuk a méretet a modell alapján
            img.Width = model.Width;
            img.Height = model.Height;

            // 2. Eldöntjük, mi legyen a fájl neve (ez az "imageName" változó)
            string imageName = "";

            if (model is Player)
                imageName = "player.gif";
            else if (model is Enemy)
                imageName = "enemy.gif";
            else if (model is Bullet)
                imageName = "bullet.gif";
            // ----------------------------------

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            // Most már ismeri az "imageName" változót:
            bmp.UriSource = new Uri($"pack://application:,,,/SpaceInvaders;component/Images/{imageName}");
            bmp.EndInit();

            // Animáció beállítása (WpfAnimatedGif csomaggal)
            ImageBehavior.SetAnimatedSource(img, bmp);

            return img;
        }

    }
}