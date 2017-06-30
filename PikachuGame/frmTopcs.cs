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
    public partial class frmTopcs : Form
    {
        public string path = frmTroChoi.path + @"\Score\Score.txt";
        public List<Player> playerList = new List<Player>();
        public frmTopcs()
        {
            InitializeComponent();
        }

        private void frmTopcs_Load(object sender, EventArgs e)
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

                List<Player> SortedList = playerList.OrderByDescending(o => o.score).ToList();
                for (int i = 0; i <= 5 && i < SortedList.Count; i++)
                {
                    dgTop.Rows.Add(SortedList[i].name, SortedList[i].age, SortedList[i].score);
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Chưa có danh sách top!");
            }
        }

        private void dgTop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
