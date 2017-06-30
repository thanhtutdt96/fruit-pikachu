using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PikachuGame
{
    public partial class frmLoad : Form
    {
        public frmLoad()
        {
            InitializeComponent();
        }
      
    
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbUserName.SelectedItem!=null)
            {

                string map = File.ReadAllText(frmTroChoi.path + @"\Map\" + cmbUserName.SelectedItem.ToString() + ".txt");
                string [] array = map.Split(' ');
                frmTroChoi.level = int.Parse(array[0]);
                frmTroChoi.score= int.Parse(array[1]);
                frmTroChoi.numRefresh = int.Parse(array[2]);
                frmTroChoi.numHelp= int.Parse(array[3]);
                frmTroChoi.progressBarValue= int.Parse(array[4]);
                int index = 5;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 17; j++)
                    {                       
                            Matrix.arr[i, j] = int.Parse(array[index]);
                        index++;
                    }
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Hãy chọn lại tên", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }


        private void frmLoad_Load(object sender, EventArgs e)
        {
       
          
            DirectoryInfo di = new DirectoryInfo(frmTroChoi.path + @"\Map");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                cmbUserName.Items.Add(file.Name.Substring(0, file.Name.Length - 4));
            }         
        }
    }
}
