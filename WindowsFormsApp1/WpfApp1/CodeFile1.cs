using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program.EnableVisualStyles();
            Program.SetCompatibleTextRenderingDefault(false);
            Program.Run(new Form1());
        }
    }
}
