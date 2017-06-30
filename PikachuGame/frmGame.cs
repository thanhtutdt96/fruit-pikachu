using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using WMPLib;
namespace PikachuGame
{
    public partial class frmTroChoi : Form
    {
        List<Button> btnlist = new List<Button>();
        List<TwoPoint> linelist = new List<TwoPoint>();
        Random random = new Random();
        Point p1 = new Point(0, 0);
        Point p2 = new Point(0, 0);
        Point nullp = new Point(0, 0);
        TwoPoint result;
        bool paint = false;
        public static int level = 2;
        public static int score = 0;
        public static int numRefresh = 2;
        public static int numHelp = 3;
        public static int progressBarValue = 0;
        public static string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        Boolean isFruitImage = true;
        Boolean isBigImage = false;
        WindowsMediaPlayer wmplayer = new WindowsMediaPlayer();
        double timeMusic = 0;
        public frmTroChoi()
        {
            InitializeComponent();
            lblStage.Text = level + "";
            lblNumRefresh.Text = numRefresh + "";
            lblNumHelp.Text = numHelp + "";
            Matrix.InitMatrix(level);
            progressBar1.Maximum = 400;
            progressBar1.Minimum = 0;
            progressBar1.Value = progressBarValue;
            pbResume.Hide();
            wmplayer.URL = path + @"\Resources\background.wav";

            wmplayer.controls.play();
            wmplayer.settings.setMode("Loop", true);
            pbSound.Hide();
            foreach (Button btn in panel1.Controls)
            {
                btn.Text = null;
            }
            foreach (Button btn in panel1.Controls)
            {
                btnlist.Add(btn);
            }

            btnlist = btnlist.OrderBy(o => int.Parse(o.Name.Substring(6))).ToList();
          
            LoadImage();

            if (isFruitImage)
            {
                pictureBoxImage.Image = Properties.Resources.pokemon;
            }
            else
            {
                pictureBoxImage.Image = Properties.Resources.fruit;
            }


            CheckPath();
            timer1.Enabled = true;

            panel1.Paint += PaintPanelOrButton;

            foreach (var btn in btnlist)
            {
                btn.Paint += PaintPanelOrButton;
            }




            //for(int i=0;i<btnlist.Count;i++)
            //{
            //    int x, y;
            //    int tmp = i + 1;
            //    x = (tmp % 15 == 0) ? (tmp / 15) : (tmp / 15 + 1);
            //    y = tmp - (x - 1) * 15;
            //    btnlist[i].Text = Matrix.arr[x,y]+"";
            //}
        }
        private void CheckPath()
        {
            result = Matrix.CheckAvailablePath();
            if (!result.p1.Equals(nullp) && !result.p2.Equals(nullp))
            {

            }
            else
            {
                while (result.p1.Equals(nullp) && result.p2.Equals(nullp))
                {
                    Matrix.RefreshMatrix();
                    result = Matrix.CheckAvailablePath();
                }
                LoadImage();
            }

        }
        private void PaintPanelOrButton(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 3);
            if (paint == true)
            {
                if (linelist.Count == 1)
                {
                    int btn1 = (linelist[0].p1.X - 1) * 15 + linelist[0].p1.Y - 1;
                    int btn2 = (linelist[0].p2.X - 1) * 15 + linelist[0].p2.Y - 1;
                    // center the line endpoints on each button
                    Point pt1 = new Point(btnlist[btn1].Left + (btnlist[btn1].Width / 2),
                            btnlist[btn1].Top + (btnlist[btn1].Height / 2));
                    Point pt2 = new Point(btnlist[btn2].Left + (btnlist[btn2].Width / 2),
                            btnlist[btn2].Top + (btnlist[btn2].Height / 2));

                    if (sender is Button)
                    {
                        // offset line so it's drawn over the button where
                        // the line on the panel is drawn
                        Button btn = (Button)sender;
                        pt1.X -= btn.Left;
                        pt1.Y -= btn.Top;
                        pt2.X -= btn.Left;
                        pt2.Y -= btn.Top;
                    }

                    e.Graphics.DrawLine(pen, pt1, pt2);
                }

                else if (linelist.Count == 2)
                {
                    int btn1 = (linelist[0].p1.X - 1) * 15 + linelist[0].p1.Y - 1;
                    int btn2 = (linelist[0].p2.X - 1) * 15 + linelist[0].p2.Y - 1;
                    int btn4 = (linelist[1].p2.X - 1) * 15 + linelist[1].p2.Y - 1;
                    Point pt1 = new Point(btnlist[btn1].Left + (btnlist[btn1].Width / 2),
                            btnlist[btn1].Top + (btnlist[btn1].Height / 2));
                    Point pt2 = new Point(btnlist[btn2].Left + (btnlist[btn2].Width / 2),
                            btnlist[btn2].Top + (btnlist[btn2].Height / 2));

                    Point pt4 = new Point(btnlist[btn4].Left + (btnlist[btn4].Width / 2),
                          btnlist[btn4].Top + (btnlist[btn4].Height / 2));


                    if (sender is Button)
                    {
                        // offset line so it's drawn over the button where
                        // the line on the panel is drawn
                        Button btn = (Button)sender;
                        pt1.X -= btn.Left;
                        pt1.Y -= btn.Top;
                        pt2.X -= btn.Left;
                        pt2.Y -= btn.Top;
                        pt4.X -= btn.Left;
                        pt4.Y -= btn.Top;

                    }

                    e.Graphics.DrawLine(pen, pt1, pt2);
                    e.Graphics.DrawLine(pen, pt4, pt2);

                }
                else if (linelist.Count == 3)
                {
                    int btn2 = 0;
                    int btn4 = 0;
                    Point pt2 = nullp;
                    Point pt4 = nullp;
                    if (linelist[0].p2.X == 0)
                    {
                        btn2 = (linelist[0].p2.X) * 15 + linelist[0].p2.Y - 1;

                        pt2 = new Point(btnlist[btn2].Left + (btnlist[btn2].Width / 2),
                          btnlist[btn2].Top - (btnlist[btn2].Height / 2));

                        btn4 = (linelist[1].p2.X) * 15 + linelist[1].p2.Y - 1;
                        pt4 = new Point(btnlist[btn4].Left + (btnlist[btn4].Width / 2),
                             btnlist[btn4].Top - (btnlist[btn4].Height / 2));
                    }
                    else if (linelist[0].p2.X == 9)
                    {
                        btn2 = (linelist[0].p2.X - 2) * 15 + linelist[0].p2.Y - 1;

                        pt2 = new Point(btnlist[btn2].Left + (btnlist[btn2].Width / 2),
                          btnlist[btn2].Top + (btnlist[btn2].Height / 2 + btnlist[btn2].Height));

                        btn4 = (linelist[1].p2.X - 2) * 15 + linelist[1].p2.Y - 1;
                        pt4 = new Point(btnlist[btn4].Left + (btnlist[btn4].Width / 2),
                             btnlist[btn4].Top + (btnlist[btn4].Height / 2 + btnlist[btn4].Height));
                    }
                    else if (linelist[0].p2.Y == 0)
                    {
                        btn2 = (linelist[0].p2.X - 1) * 15 + linelist[0].p2.Y;
                        btn4 = (linelist[1].p2.X - 1) * 15 + linelist[1].p2.Y;

                        pt2 = new Point(btnlist[btn2].Left - (btnlist[btn2].Width / 2),
                           btnlist[btn2].Top + (btnlist[btn2].Height / 2));

                        pt4 = new Point(btnlist[btn4].Left - (btnlist[btn4].Width / 2),
                             btnlist[btn4].Top + (btnlist[btn4].Height / 2));
                    }

                    else if (linelist[0].p2.Y == 16)
                    {
                        btn2 = (linelist[0].p2.X - 1) * 15 + linelist[0].p2.Y - 2;
                        btn4 = (linelist[1].p2.X - 1) * 15 + linelist[1].p2.Y - 2;

                        pt2 = new Point(btnlist[btn2].Left + (btnlist[btn2].Width / 2 + btnlist[btn2].Width),
                           btnlist[btn2].Top + (btnlist[btn2].Height / 2));

                        pt4 = new Point(btnlist[btn4].Left + (btnlist[btn4].Width / 2 + btnlist[btn4].Width),
                             btnlist[btn4].Top + (btnlist[btn4].Height / 2));
                    }
                    else
                    {
                        btn2 = (linelist[0].p2.X - 1) * 15 + linelist[0].p2.Y - 1;
                        btn4 = (linelist[1].p2.X - 1) * 15 + linelist[1].p2.Y - 1;

                        pt2 = new Point(btnlist[btn2].Left + (btnlist[btn2].Width / 2),
                           btnlist[btn2].Top + (btnlist[btn2].Height / 2));

                        pt4 = new Point(btnlist[btn4].Left + (btnlist[btn4].Width / 2),
                             btnlist[btn4].Top + (btnlist[btn4].Height / 2));

                    }
                    int btn1 = (linelist[0].p1.X - 1) * 15 + linelist[0].p1.Y - 1;

                    int btn6 = (linelist[2].p2.X - 1) * 15 + linelist[2].p2.Y - 1;
                    Point pt1 = new Point(btnlist[btn1].Left + (btnlist[btn1].Width / 2),
                            btnlist[btn1].Top + (btnlist[btn1].Height / 2));

                    Point pt6 = new Point(btnlist[btn6].Left + (btnlist[btn6].Width / 2),
                         btnlist[btn6].Top + (btnlist[btn6].Height / 2));

                    if (sender is Button)
                    {
                        // offset line so it's drawn over the button where
                        // the line on the panel is drawn
                        Button btn = (Button)sender;
                        pt1.X -= btn.Left;
                        pt1.Y -= btn.Top;
                        pt2.X -= btn.Left;
                        pt2.Y -= btn.Top;
                        pt4.X -= btn.Left;
                        pt4.Y -= btn.Top;
                        pt6.X -= btn.Left;
                        pt6.Y -= btn.Top;
                    }

                    e.Graphics.DrawLine(pen, pt1, pt2);
                    e.Graphics.DrawLine(pen, pt4, pt2);
                    e.Graphics.DrawLine(pen, pt6, pt4);
                    e.Dispose();
                }
            }
        }


        private void ButtonMethod(object sender)
        {
            using (var soundPlayer = new SoundPlayer(PikachuGame.Properties.Resources.clickButton))
            {
                soundPlayer.Play();
            }

            var btn = sender as Button;
            int tmp = int.Parse(btn.Name.Substring(6).ToString());
            int x, y;
            x = (tmp % 15 == 0) ? (tmp / 15) : (tmp / 15 + 1);
            y = tmp - (x - 1) * 15;

            if (p1 == nullp)
            {
                p1 = new Point(x, y);


                btnlist[(p1.X - 1) * 15 + p1.Y - 1].FlatStyle = FlatStyle.Flat;
                btnlist[(p1.X - 1) * 15 + p1.Y - 1].FlatAppearance.BorderColor = Color.Red;

            }
            else
            {

                p2 = new Point(x, y);
                //kiem tra co giong hinh hay k
                if (p1.Equals(p2))
                {

                }
                else if (Matrix.arr[p1.X, p1.Y] != Matrix.arr[x, y])
                {

                }
                else// kiem tra duong di
                {
                    CheckMethod();

                }
                btnlist[(p1.X - 1) * 15 + p1.Y - 1].FlatStyle = FlatStyle.Popup;
                btnlist[(p1.X - 1) * 15 + p1.Y - 1].FlatAppearance.BorderSize = 1;

                p1 = nullp;
                p2 = nullp;

            }
        }
        private void pbNewGame_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            timer1.Enabled = false;
            frmDiff f = new frmDiff();

            DialogResult dr = new DialogResult();
            dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                linelist.Clear();
                progressBar1.Value = progressBar1.Minimum;
                score = 0;
                lblScore.Text = "0";
                lblStage.Text = level + "";
                numHelp = 5;
                lblNumHelp.Text = numHelp + "";
                numRefresh = 5;
                lblNumRefresh.Text = numRefresh + "";
                Matrix.InitMatrix(level);

                CheckPath();
                LoadImage();
            }
            else if (dr == DialogResult.Ignore)
            {
                linelist.Clear();

                frmLoad frmlogin = new frmLoad();
                DialogResult dr1 = new DialogResult();
                dr1 = frmlogin.ShowDialog();

                if (dr1 == DialogResult.OK)
                {
                    progressBar1.Value = progressBarValue;
                    CheckPath();
                    LoadImage();
                    lblScore.Text = score.ToString();
                    lblStage.Text = level + "";
                    lblNumHelp.Text = numHelp + "";
                    lblNumRefresh.Text = numRefresh + "";
                }


            }

            if (progressBar1.Value < 200)
            {
                panel1.Enabled = true;
                timer1.Enabled = true;
            }
        }
        private void CheckMethod()
        {
            linelist = Matrix.findpath(p1, p2);
            if (linelist.Count > 0)
            {

                score = score + 150;
                lblScore.Text = score.ToString();

                using (var soundPlayer = new SoundPlayer(PikachuGame.Properties.Resources.Success))
                {
                    soundPlayer.Play();
                }
                PaintMethod();
                if (level == 1 | level == 4 | level == 7)
                {
                    Matrix.arr[p1.X, p1.Y] = -1;
                    Matrix.arr[p2.X, p2.Y] = -1;
                    btnlist[(p1.X - 1) * 15 + p1.Y - 1].Visible = false;
                    btnlist[(p2.X - 1) * 15 + p2.Y - 1].Visible = false;

                }//di chuyen hinh ve ben trai
                else if (level == 2 | level == 5 || level == 8)
                {
                    if (p1.Y == p2.Y)
                    {
                        int max, min;
                        if (p1.X > p2.X)
                        {
                            max = p1.X;
                            min = p2.X;
                        }
                        else
                        {
                            min = p1.X;
                            max = p2.X;
                        }
                        int d1 = Matrix.ChangeArrayLevel2(min, p1.Y);
                        int d2 = Matrix.ChangeArrayLevel2(max - 1, p1.Y);
                        btnlist[(d1 - 1) * 15 + p1.Y - 1].Visible = false;
                        btnlist[(d2 - 1) * 15 + p2.Y - 1].Visible = false;
                    }
                    else
                    {
                        int d = Matrix.ChangeArrayLevel2(p1.X, p1.Y);
                        btnlist[(d - 1) * 15 + p1.Y - 1].Visible = false;
                        d = Matrix.ChangeArrayLevel2(p2.X, p2.Y);
                        btnlist[(d - 1) * 15 + p2.Y - 1].Visible = false;
                    }
                    LoadImage();
                }//di chuyen hinh len tren //level 3 6 9
                else
                {
                    if (p1.X == p2.X)
                    {
                        int max, min;
                        if (p1.Y > p2.Y)
                        {
                            max = p1.Y;
                            min = p2.Y;
                        }
                        else
                        {
                            min = p1.Y;
                            max = p2.Y;
                        }
                        int d1 = Matrix.ChangeArrayLevel3(p1.X, min);
                        int d2 = Matrix.ChangeArrayLevel3(p1.X, max - 1);
                        btnlist[(p1.X - 1) * 15 + d1 - 1].Visible = false;
                        btnlist[(p1.X - 1) * 15 + d2 - 1].Visible = false;
                    }
                    else
                    {
                        int d = Matrix.ChangeArrayLevel3(p1.X, p1.Y);
                        btnlist[(p1.X - 1) * 15 + d - 1].Visible = false;
                        d = Matrix.ChangeArrayLevel3(p2.X, p2.Y);
                        btnlist[(p2.X - 1) * 15 + d - 1].Visible = false;
                    }
                    LoadImage();
                }


                if (Matrix.CheckVictory())
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Bạn đã chiến thắng màn chơi", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    level++;
                    lblStage.Text = level + "";
                    progressBar1.Value = 0;
                    Matrix.InitMatrix(level);
                    CheckPath();
                    LoadImage();

                    timer1.Enabled = true;
                }
                else
                {
                    CheckPath();
                }
            }
        }

        private void PaintMethod()
        {
            paint = true;
            panel1.Refresh();//call PaintPanelOrButton
            Thread.Sleep(500);
            panel1.CreateGraphics().Clear(panel1.BackColor);
            paint = false;

        }


        private void LoadImage()
        {
            int tmp;
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    tmp = (i - 1) * 15 + j - 1;
                    if (Matrix.arr[i, j] != -1)
                    {
                        btnlist[tmp].Visible = true;
                        if (isFruitImage)
                        {
                            if (isBigImage)
                            {
                                btnlist[tmp].Image = imageListBigFruit.Images[Matrix.arr[i, j]];
                            }
                            else
                            {
                                btnlist[tmp].Image = imageListSmallFruit.Images[Matrix.arr[i, j]];
                            }
                           
                        }
                        else
                        {
                            if (isBigImage)
                            {
                                btnlist[tmp].Image = imageListBigPokemon.Images[Matrix.arr[i, j]];
                            }
                            else
                            {
                                btnlist[tmp].Image = imageListSmallPokemon.Images[Matrix.arr[i, j]];
                            }
                        }
                    }
                    else
                    {
                        btnlist[tmp].Visible = false;
                    }
                }
            }
        }

 

       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (panel1.Enabled == true)
            {
                Matrix.RefreshMatrix();
                LoadImage();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeBar();

        }
        private void pbSave_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            panel1.Enabled = false;
            frmSave saveGame = new frmSave();
            DialogResult dr = new DialogResult();
            dr = saveGame.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string map = level + " " + score +" "+numRefresh+" " +numHelp+" "+ progressBar1.Value + " ";
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 17; j++)
                    {
                        if (i == 9 && j == 16)
                        {
                            map += Matrix.arr[i, j].ToString() + "";
                        }
                       else
                        {
                            map += Matrix.arr[i, j].ToString() + " ";
                        }
                    }
                }
                string filePath = path + @"\Map\" + saveGame.txtUserNameInput.Text + ".txt";
                if (!File.Exists(path))
                {
                    File.WriteAllText(filePath, map);
                }
                saveGame.Dispose();
            }
            if (progressBar1.Value != progressBar1.Maximum)
            {
                panel1.Enabled = true;
                timer1.Enabled = true;
            }
        }

        private void pbLoad_Click(object sender, EventArgs e)
        {
            linelist.Clear();
            timer1.Enabled = false;
            panel1.Enabled = false;
            frmLoad frmlogin = new frmLoad();
            DialogResult dr = new DialogResult();
            dr = frmlogin.ShowDialog();

            if (dr == DialogResult.OK)
            {
                progressBar1.Value = progressBarValue;
                CheckPath();
                LoadImage();
                lblScore.Text = score.ToString();
                lblStage.Text = level + "";
                lblNumHelp.Text = numHelp + "";
                lblNumRefresh.Text = numRefresh + "";
            }
            if(progressBar1.Value != progressBar1.Maximum)
            {
                panel1.Enabled = true;
                timer1.Enabled = true;
            }
         
        }
        private void TimeBar()
        {          
            if(panel1.Enabled==true&& timer1.Enabled == true)
            {
                if (progressBar1.Value == progressBar1.Maximum)
                {
                    panel1.Enabled = false;
                    timer1.Enabled = false;
                    MessageBox.Show("Bạn đã thua cuộc!", "Hết thời gian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmSaveScore f = new frmSaveScore();
                    f.ShowDialog();
                }
                else
                {
                    progressBar1.Value += 1;
                }
            }
          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button9_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button11_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button14_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button15_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button16_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button17_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button18_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button19_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button20_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button21_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button22_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button23_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button24_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button25_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button26_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button27_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button28_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button29_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button30_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button31_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button32_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button33_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button34_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            ButtonMethod(sender);
        }

        private void button36_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button37_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            ButtonMethod(sender);
        }

        private void button39_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            ButtonMethod(sender);
        }

        private void button41_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button42_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button43_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }
        private void button44_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button45_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }


        private void button46_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button47_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button48_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button49_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button50_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button51_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button52_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button53_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button54_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button55_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button56_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button57_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button58_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button59_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button60_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button61_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button62_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button63_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button64_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button65_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button66_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button67_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button68_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button69_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button70_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button71_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button72_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button73_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button74_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button75_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button76_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button77_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button78_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button79_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button80_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button81_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button82_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button83_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button84_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button85_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button86_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button87_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button88_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button89_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button90_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button91_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button92_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button93_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button94_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button95_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button96_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button97_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button98_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button99_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button100_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button101_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button102_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button103_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button104_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }
        private void button105_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }
        private void button106_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button107_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button108_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button109_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button110_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button111_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button112_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button113_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button114_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button115_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button116_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button117_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button118_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button119_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }

        private void button120_Click(object sender, EventArgs e)
        {

            ButtonMethod(sender);
        }




        private void btnTop_Click(object sender, EventArgs e)
        {
            frmTopcs f = new frmTopcs();
            f.ShowDialog();
        }



        private void pbSound_Click(object sender, EventArgs e)
        {
            wmplayer.controls.currentPosition = timeMusic;
            wmplayer.controls.play();
            pbMute.Show();
            pbSound.Hide();
        }

        private void pbMute_Click(object sender, EventArgs e)
        {
            wmplayer.controls.pause();
            timeMusic = wmplayer.controls.currentPosition;
            pbMute.Hide();
            pbSound.Show();
        }

        private void frmTroChoi_Load(object sender, EventArgs e)
        {
            ToolTip ttMute = new ToolTip();
            ttMute.SetToolTip(pbMute, "Turn off sound");
            ttMute.InitialDelay = 0;
            ToolTip ttSound = new ToolTip();
            ttSound.SetToolTip(pbSound, "Turn on sound");
            ttSound.InitialDelay = 0;
            ToolTip ttSave = new ToolTip();
            ttSave.SetToolTip(pbSave, "Save current game");
            ttSave.InitialDelay = 0;
            ToolTip ttLoad = new ToolTip();
            ttLoad.SetToolTip(pbLoad, "Load saved game");
            ttLoad.InitialDelay = 0;
            ToolTip ttHS = new ToolTip();
            ttHS.SetToolTip(pbTopScore, "High Scores");
            ttHS.InitialDelay = 0;
        }

        private void pbTopScore_Click(object sender, EventArgs e)
        {
            frmTopcs f = new frmTopcs();
            f.ShowDialog();
        }

   

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void pbHint_Click(object sender, EventArgs e)
        {
            if (panel1.Enabled == true)
            {
                if (numHelp >= 1)
                {
                    numHelp--;
                    lblNumHelp.Text = numHelp + "";
                    p1 = result.p1;
                    p2 = result.p2;
                    CheckMethod();
                    p1 = nullp;
                    p2 = nullp;
                }
               
            }
          
        }

        private void pbPause_Click(object sender, EventArgs e)
        {
            
            if (panel1.Enabled == true)
            {
                lblPR.Text = "Chơi Tiếp";
                timer1.Stop();
                pbPause.Hide();
                pbResume.Show();
                panel1.Enabled = false;
            }
        }

        private void pbResume_Click(object sender, EventArgs e)
        {
            
            if (panel1.Enabled == false && progressBar1.Value != progressBar1.Maximum)
            {
                lblPR.Text = "Dừng";
                timer1.Start();
                panel1.Enabled = true;
                pbResume.Hide();
                pbPause.Show();
            }
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            if (panel1.Enabled == true)
            {
                if (numRefresh >= 1)
                {
                    numRefresh--;
                    lblNumRefresh.Text = numRefresh + "";
                    Matrix.RefreshMatrix();
                    CheckPath();
                    LoadImage();
                }
            }
        }

      

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            if (isFruitImage)
            {
                isFruitImage = false;
                pictureBoxImage.Image = Properties.Resources.fruit;
                LoadImage();
            }
            else
            {
                isFruitImage = true;
                pictureBoxImage.Image = Properties.Resources.pokemon;
                LoadImage();
            }
        }

    

        private void frmTroChoi_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Size == new Size(840, 480))
            {
                
                panel1.Size = new Size(1070, 610);
                isBigImage = true;
                LoadImage();
                foreach (var item in btnlist)
                {
                    item.Size=new Size(60,60);
                }


                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 16; j++)
                    {
                        int index = j + (i - 1) * 15 - 1;
                        if (i == 1 && j == 1)
                        {

                        }
                        else if (i == 1&&j!=1)
                        {
                            btnlist[index].Location = new Point(btnlist[index - 1].Right + 6, btnlist[index].Location.Y);
                        }
                        else if (j == 1&&i!=1)
                        {
                            btnlist[index].Location = new Point(btnlist[index].Location.X, btnlist[index - 15].Bottom + 6);
                        }
                        else
                        {                           
                            btnlist[index].Location = new Point(btnlist[index - 1].Right + 6, btnlist[index - 15].Bottom + 6);
                        }
                      
                    }
                }


            }
            else
            {
                panel1.Size = new Size(840, 480);
                isBigImage = false;
                LoadImage();

                foreach (var item in btnlist)
                {
                    item.Size = new Size(45, 45);
                }


                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 16; j++)
                    {
                        int index = j + (i - 1) * 15 - 1;
                        if (i == 1 && j == 1)
                        {

                        }
                        else if (i == 1 && j != 1)
                        {
                            btnlist[index].Location = new Point(btnlist[index - 1].Right + 6, btnlist[index].Location.Y);
                        }
                        else if (j == 1 && i != 1)
                        {
                            btnlist[index].Location = new Point(btnlist[index].Location.X, btnlist[index - 15].Bottom + 6);
                        }
                        else
                        {
                            btnlist[index].Location = new Point(btnlist[index - 1].Right + 6, btnlist[index - 15].Bottom + 6);
                        }

                    }
                }
            }

        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            frmHelp f = new frmHelp();
            f.ShowInTaskbar = false;
            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                return;
            }
        }

        private void lblNew_Click(object sender, EventArgs e)
        {
            pbNewGame_Click(sender,e);
        }

        private void lblPR_Click(object sender, EventArgs e)
        {
            if (lblPR.Text.Equals("Dừng"))
            {
                pbPause_Click(sender, e);
            }
            else
            {
                pbResume_Click(sender, e);
            }
        }

        private void lblRefresh_Click(object sender, EventArgs e)
        {
            pbRefresh_Click(sender, e);
        }

        private void lblImage_Click(object sender, EventArgs e)
        {
            pictureBoxImage_Click(sender, e);
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            pbHint_Click(sender, e);
        }

        private void lblHD_Click(object sender, EventArgs e)
        {
            pbHelp_Click(sender, e);
        }

        private void lblQuit_Click(object sender, EventArgs e)
        {
            pbExit_Click(sender, e);
        }
    }
}
