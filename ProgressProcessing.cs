using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace XLS2SQL_Converter
{
    public partial class ProgessProcessing : Form
    {

        public bool Value { get; set; }
        public long Min { get { return 0; } }
        public long Max { get { return 100; } }

        public ProgessProcessing()
        {
            InitializeComponent();            

        }

        public void progresso()
        {
            progressBar1.Maximum = (int) this.Max;
            progressBar1.Minimum = (int) this.Min;
            Show();
            while (Value)
            {
                if (progressBar1.Value >= this.Max)
                {
                    progressBar1.Value = (int)this.Min;
                }
                else
                {
                    progressBar1.Value += 10;
                }
                
                Thread.Sleep(100);
                Application.DoEvents();

            }
            this.Dispose();
        }
    }
}
