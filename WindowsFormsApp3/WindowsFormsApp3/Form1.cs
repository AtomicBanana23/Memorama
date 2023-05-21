using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// He notado un error y una característica a arreglar: La carta de la uva (img11) se repite tres veces en el tablero o de plano aparece en el tablero cuando no debería estar. Es decir...
// Si existe un par de uvas, va a existir una tercera uva, y si no hay par de uvas, va a existir una uva aún así. También falta ajustar el código para que pueda ser capaz de tener 30
// cartas en el tablero. Este tablero es una matriz cuadrada, por lo que solamente admite el mismo número de filas y columnas. Me parece que la clave de todo esto está en la variable
// "TMColumnasFilas", ya que esta variable será la que defina el número de filas y columnas. En este caso tenemos "5", lo que crea 5 filas y 5 columnas (25 cartas totales). Si aumenta en
// 6, terminará creando un tablero de 36 cartas. El juego por defecto termina cuando se consiguen 7 pares (al menos dentro de la actual modalidad de 25 cartas), pero se puede cambiar eso.
namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int TMColumnasFilas = 5; // Este es el número que crea las filas y columnas.
        int Movimientos = 0;
        int ContadorCartasVolteadas = 0;
        List<string> CartasNumeradas;
        List<string> CartasRevueltas;
        ArrayList CartasSeleccionadas;
        PictureBox CartaTemporal1;
        PictureBox CartaTemporal2;
        int CartaActual = 0;
        public Form1()
        {
            InitializeComponent();
            IniciarJuego();
        }

        public void IniciarJuego()
        {
            timer1.Enabled = false;
            timer1.Stop();
            lblRecord.Text = "0";
            ContadorCartasVolteadas = 0;
            Movimientos = 0;
            panelGame.Controls.Clear();
            CartasNumeradas = new List<string>();
            CartasRevueltas = new List<string>();
            CartasSeleccionadas = new ArrayList();
            for (int i = 0; i < 15; i++)
            {
                CartasNumeradas.Add(i.ToString());
                CartasNumeradas.Add(i.ToString());
            }
            var nRandom = new Random();
            var Resultado = CartasNumeradas.OrderBy(item => nRandom.Next());

            foreach (string ValorCarta in Resultado)
            {
                CartasRevueltas.Add(ValorCarta);
            }
            var tablaPanel = new TableLayoutPanel();
            tablaPanel.RowCount = TMColumnasFilas;
            tablaPanel.ColumnCount = TMColumnasFilas;
            for (int i = 0; i < TMColumnasFilas; i++)
            {
                var Porcentaje = 150f / (float)TMColumnasFilas - 10;
                tablaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, Porcentaje));
                tablaPanel.RowStyles.Add(new RowStyle(SizeType.Percent, Porcentaje));
            }
            int ContadorFichas = 1;
            for (var i = 0; i < TMColumnasFilas; i++) // Aquí se van creando las filas y columnas dependiendo del número puesto en TMColumnasFilas
            {
                for (var j = 0; j < TMColumnasFilas; j++)
                {
                    var CartasJuego = new PictureBox();
                    CartasJuego.Name = String.Format("{0}", ContadorFichas);
                    CartasJuego.Dock = DockStyle.Fill;
                    CartasJuego.SizeMode = PictureBoxSizeMode.StretchImage;
                    CartasJuego.Image = Properties.Resources.Girada;
                    CartasJuego.Cursor = Cursors.Hand;
                    CartasJuego.Click += bttnCard_Click;
                    tablaPanel.Controls.Add(CartasJuego, j, i);
                    ContadorFichas++;
                }
            }
            tablaPanel.Dock = DockStyle.Fill;
            panelGame.Controls.Add(tablaPanel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bttnRestart_Click(object sender, EventArgs e)
        {
            IniciarJuego();
        }
        private void bttnCard_Click(object sender, EventArgs e)
        {
            if (CartasSeleccionadas.Count < 2)
            {
                Movimientos++;
                lblRecord.Text = Convert.ToString(Movimientos);
                var CartasSeleccionadasUsuario = (PictureBox)sender;
                CartaActual = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartasSeleccionadasUsuario.Name) - 1]);
                CartasSeleccionadasUsuario.Image = RecuperarImagen(CartaActual);
                CartasSeleccionadas.Add(CartasSeleccionadasUsuario);
                
                // Validación
                if (CartasSeleccionadas.Count == 2)
                {
                    CartaTemporal1 = (PictureBox)CartasSeleccionadas[0];
                    CartaTemporal2 = (PictureBox)CartasSeleccionadas[1];
                    int Carta1 = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaTemporal1.Name) - 1]);
                    int Carta2 = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaTemporal2.Name) - 1]);

                    if (Carta1 != Carta2)
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                    else
                    {
                        ContadorCartasVolteadas++;
                        if (ContadorCartasVolteadas == 7) // Aquí debería ser un número diferente a "7", pues nosotros estamos trabajando con 30 cartas (15 y sus pares).
                        {
                            MessageBox.Show("El juego ha terminado.");

                        }
                        CartaTemporal1.Enabled = false;
                        CartaTemporal2.Enabled = false;
                        CartasSeleccionadas.Clear();
                    }
                }
            }


        }
        public Bitmap RecuperarImagen(int NumeroImagen)
        {
            Bitmap TmpImg = new Bitmap(200, 100);
            switch (NumeroImagen)
            {
                case 0: TmpImg = Properties.Resources.img11;
                    break;
                default: TmpImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("img" + NumeroImagen);
                    break;
            }
            return TmpImg;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int TiempoViradoCarta = 1;
            if (TiempoViradoCarta == 1)
            {
                CartaTemporal1.Image = Properties.Resources.Girada;
                CartaTemporal2.Image = Properties.Resources.Girada;
                CartasSeleccionadas.Clear();
                TiempoViradoCarta = 0;
                timer1.Stop();

            }
        }
    }
}
