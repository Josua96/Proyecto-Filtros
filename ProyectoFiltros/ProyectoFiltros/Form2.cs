using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProyectoFiltros
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            setCores();
        }

        private void setCores() {
            int coreCount = Environment.ProcessorCount;
            for (int i = 0; i < coreCount; i++)
            {
                coreList.Items.Add(i + 1);
            }
            label3.Text = Process.GetCurrentProcess().ProcessorAffinity + "";
        }

        private void changeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            var v = new Form1();
            v.Closed += (s, args) => this.Close();
            v.Show();
        }

        private void coreList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (coreList.SelectedIndex + 1)
            {
                case 1:
                    Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)1;
                    break;
                case 2:
                    Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)3;
                    break;
                case 3:
                    Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)7;
                    break;
                case 4:
                    Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)9;
                    break;
                default:
                    Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)127;
                    break;
            }
            label3.Text = Process.GetCurrentProcess().ProcessorAffinity + "";

        }
    }
}

