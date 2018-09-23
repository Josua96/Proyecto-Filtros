using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProyectoFiltros
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Console.WriteLine("Total # of processors: {0}", Environment.ProcessorCount);
            Console.WriteLine("Current processor affinity: {0}", Process.GetCurrentProcess().ProcessorAffinity);
            Console.WriteLine("*********************************");
            Console.WriteLine("Insert your selected processors, separated by comma (first CPU index is 1):");
            var input = "1";
            Console.WriteLine("*********************************");
            var usedProcessors = input.Split(',');

            //TODO: validate input
            int newAffinity = 0;
            foreach (var item in usedProcessors)
            {
                newAffinity = newAffinity | int.Parse(item);
                Console.WriteLine("Processor #{0} was selected for affinity.", item);
            }
            Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)newAffinity;
            Console.WriteLine("*********************************");
            Console.WriteLine("Current processor affinity is {0}", Process.GetCurrentProcess().ProcessorAffinity);

            
             Bitmask	Binary value	Eligible processors
            0x0001	00000000 00000001	1
            0x0003	00000000 00000011	1 and 2
            0x0007	00000000 00000111	1, 2 and 3
            0x0009	00000000 00001001	1 and 4
            0x007F	00000000 01111111	1, 2, 3, 4, 5, 6 and 7
             
             */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
