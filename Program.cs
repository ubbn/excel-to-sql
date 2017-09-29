// ubbuyan, XLS2SQL convertor, version 1.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XLS2SQL_Converter
{
    static class Program
    {
        /// <summary>
        /// Here main entry point goes for app.
        /// </summary>
        

        // COM component communication
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmMain());
        }

    }

}
