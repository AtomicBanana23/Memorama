﻿using System;
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
    public partial class Form2 : Form
    {
        public static int j = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            j = 2;
            Form1 juego = new Form1();
            juego.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            j = 3;
            Form1 juego = new Form1();
            juego.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            j = 4;
            Form1 juego = new Form1();
            juego.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            j = 5;
            Form1 juego = new Form1();
            juego.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            j = 6;
            Form1 juego = new Form1();
            juego.Show();
            this.Close();
        }
    }
}
