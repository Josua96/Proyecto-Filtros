using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProyectoFiltros
{
    public partial class Form2 : Form
    {
        public PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public Timer t = new Timer();

        public Form2()
        {
            InitializeComponent();
            label3.Text = CPUCounter.NextValue() + "%";
            t.Interval = 750;
            t.Enabled = true;
            timer1_Tick(null, null);

            t.Tick += new EventHandler(timer1_Tick);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("a");
            label3.Text = CPUCounter.NextValue() + "%";
        }

       
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Dispose();
        }
    }
}

