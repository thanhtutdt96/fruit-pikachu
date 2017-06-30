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
    public partial class frmSaveScore : Form
    {
        public string path = frmTroChoi.path + @"\Score\Score.txt";
        
        public List<Player> playerList = new List<Player>();
        public frmSaveScore()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int age = 0;
            if (txtUserNameInput.Text.Equals("") | txtAge.Text.Equals(""))
            {
                MessageBox.Show("Hãy nhập đủ thông tin!");
            }
            
            else
            {
                bool tmp=false;
                bool flagException = false;
                try
                {
                    age = int.Parse(txtAge.Text);
                }
                catch (Exception)
                {
                    flagException = true;
                    MessageBox.Show("Hãy nhập lại đúng thông tin!");
                }

                if (age <= 0)
                {
                    flagException = true;
                    MessageBox.Show("Hãy nhập lại đúng thông tin!");
                }




                if (flagException == false)
                {
                    //foreach (Player item in playerList)
                    //{
                    //    if (item.name.Equals(txtUserNameInput.Text) && item.age.Equals(age))
                    //    {
                    //        tmp = true;
                    //        break;
                    //    }
                    //}
                    //if (tmp)
                    //{
                    //    MessageBox.Show("Đã có người chơi này!");
                    //}
                    //else
                    //{
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(txtUserNameInput.Text + ":" + txtAge.Text + ":" + frmTroChoi.score);
                        }
                        this.Close();
                    //}
                }
               
                
            }
        }

        private void frmSaveScore_Load(object sender, EventArgs e)
        {
            try
            {
                string line;
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    string[] arr = line.Split(':');
                    playerList.Add(new Player(arr[0], int.Parse(arr[1]), int.Parse(arr[2])));
                }
                file.Close();
            }
            catch (Exception)
            {
                
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
