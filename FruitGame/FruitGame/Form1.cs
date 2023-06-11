using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitGame
{
    public partial class Form1 : Form
    {
        int rondas = 0;
        int jugador = 0; 
        static int j = Form2.j;
        bool allowClick = false;
        PictureBox firstGuess;
        Random rnd = new Random();
        Timer clickTimer = new Timer();
        int time = 60;
        Timer timer = new Timer { Interval = 1000 };
        int[] jugadores = new int[j];

        public Form1()
        {
            InitializeComponent();
        }

        private PictureBox[] pictureBoxes
        {
            get {  return Controls.OfType<PictureBox>().ToArray(); }
        }

        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[]
                {
                    Properties.Resources.img1,
                    Properties.Resources.img2,
                    Properties.Resources.img3,
                    Properties.Resources.img4,
                    Properties.Resources.img5,
                    Properties.Resources.img6,
                    Properties.Resources.img7,
                    Properties.Resources.img8,
                    Properties.Resources.img9,
                    Properties.Resources.img10,
                    Properties.Resources.img11,
                    Properties.Resources.img12,
                    Properties.Resources.img13,
                    Properties.Resources.img14,
                    Properties.Resources.img15,
                };
            }
        }

        private void startGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time < 0)
                {
                    timer.Stop();
                    MessageBox.Show("Se ha acabado el tiempo.");
                    ResetImages();
                }
                var ssTime = TimeSpan.FromSeconds(time);

                //label1.Text = "00:" + time.ToString();
            };
        }
        

        private void ResetImages()
        {
            rondas += 1;

            if (rondas == 4)
            {
                int maxpunt = 0;
                int ganador = 0;
                for (int i = 0; i < j; i++)
                {
                    if (jugadores[i] >= maxpunt)
                    {
                        maxpunt = jugadores[i];
                        ganador = i + 1;
                    }

                }
                this.Close();
                MessageBox.Show($"El jugador {ganador} ganó con {maxpunt} pares.");
                return;
            }

            foreach (var pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }

            HideImages();
            setRandomImages();
            time = 60;
            timer.Start();
        }

        private void HideImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Image = Properties.Resources.question;
            }
        }

        private PictureBox getFreeSlot()
        {
            int num;

            do
            {
                num = rnd.Next(0, pictureBoxes.Count());
            }
            while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }

        private void setRandomImages()
        {
            foreach (var image in images)
            {
                getFreeSlot().Tag = image;
                getFreeSlot().Tag = image;
            }
        }

        private void clickTimerCount(object sender, EventArgs e)
        {
            HideImages();
            allowClick = true;
            clickTimer.Stop();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clickImage(object sender, EventArgs e)
        {
            if (!allowClick) return;

            var pic = (PictureBox)sender;

            if (firstGuess == null)
            {
                firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }

            pic.Image = (Image)pic.Tag;

            if (pic.Image == firstGuess.Image && pic != firstGuess)
            {
                pic.Visible = firstGuess.Visible = false;
                {
                    firstGuess = pic;
                }
                jugadores[jugador] +=1;
                label1.Text = "Jugador " + (jugador + 1) + "\nPuntuación: " + jugadores[jugador];
                HideImages();
            }
            else
            {
                if (jugador == j - 1)
                {
                    jugador = 0;
                }
                else
                {
                    jugador+=1;
                }
                label1.Text = $"Jugador {(jugador+1)} \nPuntuación: {jugadores[jugador]}";
                allowClick = false;
                clickTimer.Start();
            }

            firstGuess = null;

            if (pictureBoxes.Any(p => p.Visible)) return;
            ResetImages();

            new Form1().Close();
        }

        private void startGame(object sender, EventArgs e)
        {
            label1.Text = "Jugador " + (jugador + 1) + "\nPuntuación: " + jugadores[jugador];
            allowClick =true;
            setRandomImages();
            HideImages();
            //startGameTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += clickTimerCount;
            button1.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
