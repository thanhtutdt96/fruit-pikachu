using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PikachuGame
{
    public partial class frmDiff : Form
    {
        public frmDiff()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (rbEasy.Checked == true)
            {
                frmTroChoi.level = 1;
                
            }
            else if (rbMedium.Checked == true)
            {
                frmTroChoi.level = 4;
            }
            else
            {
                frmTroChoi.level = 7;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }
    }
}
