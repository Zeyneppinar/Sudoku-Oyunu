using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        #region global tanimlamalar
        public int n = 9;
        public Button[][] b;
        public Button[][] z;
        public Button[][] y;
        public Button[][] t;
        int[,] dizi = new int[9, 9];
        int[,] dizi1 = new int[9, 9];
        int[,] dizi2 = new int[9, 9];
        int[,] dizi3 = new int[9, 9];
        Stopwatch sw1 = new Stopwatch();
        Stopwatch sw2 = new Stopwatch();
        Stopwatch sw3 = new Stopwatch();
        public int sz = 30; // button boyutu
        TimeSpan x, x1, x2;
        #endregion
        #region Form1-Form1Load
        Thread mythread1;
        Thread mythread2;
        Thread mythread3;

        ThreadStart mythreadstarDelegete1;
        ThreadStart mythreadstarDelegete2;
        ThreadStart mythreadstarDelegete3;

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            createSudoku4();
            createSudoku3();
            createSudoku2();
            createSudoku();
        }
        #endregion
        #region Buttons Click
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Width = 1084;
            this.Height = 1228;
            this.StartPosition = FormStartPosition.CenterScreen;
            Baslangic();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Export();

        }
        #endregion
        #region Clear Function
        private void Clear()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    b[i][j].Text = " ";
                    z[i][j].Text = " ";
                    y[i][j].Text = " ";
                    t[i][j].Text = " ";
                }
            }
        }
        #endregion
        #region Import Function
        private void Import()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "txt file|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                Clear();
                string filename = op.FileName;
                string[] filelines = File.ReadAllLines(filename);

                if (filelines.Length == 9)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        char[] karakter = filelines[i].ToCharArray();
                        for (int j = 0; j < 9; j++)
                        {
                            karakter[j] = filelines[i][j];
                            if (karakter[j] == '*')
                            {
                                karakter[j] = '0';
                                karakter[j] = Convert.ToChar(karakter[j]);
                                b[j][i].Text = " ";
                            }
                            else
                            {
                                b[j][i].Text = filelines[i][j].ToString();

                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region Export Function
        private void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                string[] contents = new string[9];
                for (int j = 0; j < 9; j++)
                {
                    contents[j] = "";
                    for (int i = 0; i < 9; i++)
                    {
                        string anum = "";
                        if (t[i][j].Text == " ")
                        {
                            anum = "0";
                        }
                        else
                        {
                            anum = t[i][j].Text;
                        }
                        if (i < 8)
                        {
                            contents[j] += anum + " ";
                        }
                        else
                        {
                            contents[j] += anum;
                        }
                    }
                }
                File.WriteAllLines(filename, contents);
            }
        }
        #endregion
        #region create Sudoku


        public void createSudoku()
        {
            b = new Button[n][];
            for (int i = 0; i < n; i++)
            {
                b[i] = new Button[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    b[i][j] = new Button();
                    b[i][j].Size = new Size(sz, sz);
                    if (i < 3 && j < 3)
                    {
                        b[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 3 && i < 6 && j >= 3 && j < 6)
                    {
                        b[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6 && j < 9)
                    {
                        b[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j < 3)
                    {
                        b[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6)
                    {
                        b[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i < 3 && j >= 6 && j < 9)
                    {
                        b[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else
                    {
                        b[i][j].BackColor = Color.FromName("Khaki");
                    }
                    b[i][j].Text = " ";
                    b[i][j].ForeColor = Color.FromName("black");
                    b[i][j].Location = new Point(i * sz + sz, j * sz + sz);

                    groupBox1.Controls.Add(b[i][j]);

                    //groupBox2.Controls.Add(b[i][j]);
                }
            }
        }
        public void createSudoku2()
        {
            z = new Button[n][];
            for (int i = 0; i < n; i++)
            {
                z[i] = new Button[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    z[i][j] = new Button();
                    z[i][j].Size = new Size(sz, sz);
                    if (i < 3 && j < 3)
                    {
                        z[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 3 && i < 6 && j >= 3 && j < 6)
                    {
                        z[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6 && j < 9)
                    {
                        z[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j < 3)
                    {
                        z[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6)
                    {
                        z[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i < 3 && j >= 6 && j < 9)
                    {
                        z[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else
                    {
                        z[i][j].BackColor = Color.FromName("Khaki");
                    }
                    z[i][j].Text = " ";
                    z[i][j].ForeColor = Color.FromName("black");
                    z[i][j].Location = new Point(i * sz + sz, j * sz + sz);
                    groupBox2.Controls.Add(z[i][j]);





                }
            }
        }
        public void createSudoku3()
        {
            y = new Button[n][];
            for (int i = 0; i < n; i++)
            {
                y[i] = new Button[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    y[i][j] = new Button();
                    y[i][j].Size = new Size(sz, sz);
                    if (i < 3 && j < 3)
                    {
                        y[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 3 && i < 6 && j >= 3 && j < 6)
                    {
                        y[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6 && j < 9)
                    {
                        y[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j < 3)
                    {
                        y[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6)
                    {
                        y[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i < 3 && j >= 6 && j < 9)
                    {
                        y[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else
                    {
                        y[i][j].BackColor = Color.FromName("Khaki");
                    }
                    y[i][j].Text = " ";
                    y[i][j].ForeColor = Color.FromName("black");
                    y[i][j].Location = new Point(i * sz + sz, j * sz + sz);

                    groupBox3.Controls.Add(y[i][j]);

                }
            }
        }
        public void createSudoku4()
        {
            t = new Button[n][];
            for (int i = 0; i < n; i++)
            {
                t[i] = new Button[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    t[i][j] = new Button();
                    t[i][j].Size = new Size(sz, sz);
                    if (i < 3 && j < 3)
                    {
                        t[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 3 && i < 6 && j >= 3 && j < 6)
                    {
                        t[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6 && j < 9)
                    {
                        t[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j < 3)
                    {
                        t[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i >= 6 && i < 9 && j >= 6)
                    {
                        t[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else if (i < 3 && j >= 6 && j < 9)
                    {
                        t[i][j].BackColor = Color.FromName("Cornsilk");
                    }
                    else
                    {
                        t[i][j].BackColor = Color.FromName("Khaki");
                    }
                    t[i][j].Text = " ";
                    t[i][j].ForeColor = Color.FromName("black");
                    t[i][j].Location = new Point(i * sz + sz, j * sz + sz);

                    groupBox4.Controls.Add(t[i][j]);

                }
            }
        }
        
        #endregion
        #region Thread Tanimlamalari ve Threadler
        public void Baslangic() // thread tanimlamalari
        {
            mythreadstarDelegete1 = new ThreadStart(islem);
            mythreadstarDelegete2 = new ThreadStart(islem2);
            mythreadstarDelegete3 = new ThreadStart(islem3);

            mythread1 = new Thread(mythreadstarDelegete1);
            mythread2 = new Thread(mythreadstarDelegete2);
            mythread3 = new Thread(mythreadstarDelegete3);
            mythread1.Start();
            mythread2.Start();
            mythread3.Start();
           
        }
     
        #endregion
        #region Sudoku Cozumu
        private void islem()
        {
            sw1.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {
                        islem_();
                    }
                    else {
                        /*   Control.CheckForIllegalCrossThreadCalls = false; */// Cross-thread operation not valid hatası cözümü

                         if(b[i][j].Text == " ") {    
                        b[i][j].Text = "0";
                        dizi1[i, j] = int.Parse(b[i][j].Text);
                    }
                    else
                    {
                        dizi1[i, j] = int.Parse(b[i][j].Text);
                    }
                    //dizi[i, j] = int.Parse(b[i][j].Text);
                }
                }
            }
            if (SolveSudoku(dizi1) == true) { 
                printGrid(dizi1);
            x = sw1.Elapsed;
            threadkontrol();
                label1.Text = "ilk Thread " + x.ToString() + " sürede bitti ";

            }
        }
        private void islem_()
        {
            sw1.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {

                        /*   Control.CheckForIllegalCrossThreadCalls = false; */// Cross-thread operation not valid hatası cözümü
                        b[i][j].Text = "0";
                        dizi1[i, j] = int.Parse(b[i][j].Text);
                    }
                    else
                    {
                        dizi1[i, j] = int.Parse(b[i][j].Text);
                    }
                    dizi1[i, j] = int.Parse(b[i][j].Text);
                }
            }
            if (SolveSudoku(dizi1) == true)
                printGrid(dizi1);
            x = sw1.Elapsed;
        
            threadkontrol();
            label1.Text = "ilk Thread " + x.ToString() + " sürede bitti ";

        }
        private void islem_2()
        {
            sw2.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {

                        /*   Control.CheckForIllegalCrossThreadCalls = false; */// Cross-thread operation not valid hatası cözümü
                        b[i][j].Text = "0";
                        dizi2[i, j] = int.Parse(b[i][j].Text);
                    }
                    else
                    {
                        dizi2[i, j] = int.Parse(b[i][j].Text);
                    }
                    dizi2[i, j] = int.Parse(b[i][j].Text);
                }
            }
            if (SolveSudoku2(dizi2) == true)
                printGrid1(dizi2);
            x1 = sw2.Elapsed;
           
            threadkontrol();
            label2.Text = "ilk Thread " + x1.ToString() + " sürede bitti ";

        }
        private void islem_3()
        {
            sw3.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {

                        /*   Control.CheckForIllegalCrossThreadCalls = false; */// Cross-thread operation not valid hatası cözümü
                        b[i][j].Text = "0";
                        dizi3[i, j] = int.Parse(b[i][j].Text);
                    }
                    else
                    {
                        dizi3[i, j] = int.Parse(b[i][j].Text);
                    }
                    dizi3[i, j] = int.Parse(b[i][j].Text);
                }
            }
            if (SolveSudoku2(dizi3) == true)
                printGrid2(dizi3);
  x2 = sw3.Elapsed;
            threadkontrol();
            label3.Text = "üçüncü Thread " + x2.ToString() + " sürede bitti ";
        }
        private void islem2()
        {
            sw2.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {
                        islem_2();
                    }
                    else
                    {
                        /*   Control.CheckForIllegalCrossThreadCalls = false; */// Cross-thread operation not valid hatası cözümü

                        if (b[i][j].Text == " ")
                        {
                            b[i][j].Text = "0";
                            dizi2[i, j] = int.Parse(b[i][j].Text);
                        }
                        else
                        {
                            dizi1[i, j] = int.Parse(b[i][j].Text);
                        }
                        //dizi1[i, j] = int.Parse(b[i][j].Text);
                   
                }
            }
            if (SolveSudoku2(dizi2) == true)
            {
                printGrid1(dizi2);
                x1 = sw2.Elapsed;
                   
 threadkontrol();
                    label2.Text = "ilk Thread " + x1.ToString() + " sürede bitti ";
                } }

        }
        private void islem3()
        {
            sw3.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {
                        islem_3();
                    }
                    else
                    {
                        /*   Control.CheckForIllegalCrossThreadCalls = false; */// Cross-thread operation not valid hatası cözümü

                        if (b[i][j].Text == " ")
                        {
                            b[i][j].Text = "0";
                            dizi3[i, j] = int.Parse(b[i][j].Text);
                        }
                        else
                        {
                            dizi3[i, j] = int.Parse(b[i][j].Text);
                        }
                        //dizi2[i, j] = int.Parse(b[i][j].Text);
                    }
                }
            }
            if (SolveSudoku2(dizi3) == true)
            {
                printGrid2(dizi3);
                x2 = sw3.Elapsed;
                label2.Text = "ilk Thread " + x2.ToString() + " sürede bitti ";
                threadkontrol();
            }

        }
        private void islemasama()
        {

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i][j].Text == " ")
                    {

                        b[i][j].Text = " 0";
                        dizi[i, j] = int.Parse(b[i][j].Text);


                    }
                    else
                    {

                        dizi[i, j] = int.Parse(b[i][j].Text);
                    }
                    dizi[i, j] = int.Parse(b[i][j].Text);
                }

            }

            if (SolveSudoku1(dizi) == true)
            {

                printGrid1(dizi);
                threadkontrol();


            }

        }
        bool SolveSudoku1(int[,] dizi)
        {

            int row = 8, col = 8;
            if (!FindUnassignedLocation2(dizi, ref row, ref col))
                return true;
            for (int num = 1; num <= 9; num++)
            {
                if (isSafe2(dizi, row, col, num))
                {
                    dizi[row, col] = num;
                    y[row][col].Text = num.ToString();
                    System.Threading.Thread.Sleep(80);

                    if (SolveSudoku1(dizi))
                        return true;
                    dizi[row, col] = 0;
                }
                if (b[row][col].Text == "0")

                    b[row][col].Text = " ";

                //b[row][col].ForeColor = Color.FromName("red");
                //z[row][col].ForeColor = Color.FromName("red");
                y[row][col].ForeColor = Color.FromName("purple");
                //t[row][col].ForeColor = Color.FromName("green");
            }

            return false;
        }
        bool SolveSudoku2(int[,] dizi)
        {

            int row = 8, col = 0;
            if (!FindUnassignedLocation3(dizi, ref row, ref col))
                return true;
            for (int num = 1; num <= 9; num++)
            {
                if (isSafe3(dizi, row, col, num))
                {
                    dizi[row, col] = num;
                    t[row][col].Text = num.ToString();
                    System.Threading.Thread.Sleep(80);

                    if (SolveSudoku2(dizi))
                        return true;
                    dizi[row, col] = 0;
                }
                if (b[row][col].Text == "0")

                    b[row][col].Text = " ";

                //b[row][col].ForeColor = Color.FromName("red");
                //z[row][col].ForeColor = Color.FromName("red");
                //y[row][col].ForeColor = Color.FromName("purple");
                t[row][col].ForeColor = Color.FromName("green");
            }

            return false;
        }
        bool FindUnassignedLocation3(int[,] dizi, ref int row, ref int col)
        {
            for (row = 8; row >= 0; row--)
                for (col = 0; col<n; col++)
                    if (dizi[row, col] == 0)
                        return true;
            return false;
        }
        bool FindUnassignedLocation2(int[,] dizi, ref int row, ref int col)
        {
            for (row = 8; row >= 0; row--)
                for (col = 8; col >= 0; col--)
                    if (dizi[row, col] == 0)
                        return true;
            return false;
        }
        bool UsedInRow2(int[,] dizi, int row, int num)
        {
            for (int col = 8; col >= 0; col--)
                if (dizi[row, col] == num)
                    return true;
            return false;
        }
        bool UsedInRow3(int[,] dizi, int row, int num)
        {
            for (int col = 0; col<n ; col++)
                if (dizi[row, col] == num)
                    return true;
            return false;
        }
        bool UsedInCol2(int[,] dizi, int col, int num)
        {
            for (int row = 8; row >= 0; row--)
                if (dizi[row, col] == num)
                    return true;
            return false;
        }
        bool UsedInCol3(int[,] dizi, int col, int num)
        {
            for (int row = 8; row >= 0; row--)
                if (dizi[row, col] == num)
                    return true;
            return false;
        }
        bool UsedInBox2(int[,] dizi, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 8; row > 5; row--)
                for (int col = 8; col > 5; col--)
                    if (dizi[row - boxStartRow, col - boxStartCol] == num)
                        return true;
            return false;
        }
        bool UsedInBox3(int[,] dizi, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 8; row > 5; row--)
                for (int col = 0; col > 3; col--)
                    if (dizi[row - boxStartRow, col + boxStartCol] == num)
                        return true;
            return false;
        }
        bool isSafe2(int[,] dizi, int row, int col, int num)
        {
            return !UsedInRow2(dizi, row, num) &&
                   !UsedInCol2(dizi, col, num) &&
                   !UsedInBox(dizi, row - row % 3, col - col % 3, num);
        }
        bool isSafe3(int[,] dizi, int row, int col, int num)
        {
            return !UsedInRow3(dizi, row, num) &&
                   !UsedInCol3(dizi, col, num) &&
                   !UsedInBox(dizi, row - row % 3, col - col % 3, num);
        }



        bool SolveSudoku(int[,] dizi)
        {
            int row = 0, col = 0;
            if (!FindUnassignedLocation(dizi, ref row, ref col))
                return true;
            for (int num = 1; num <= 9; num++)
            {
                if (isSafe(dizi, row, col, num))
                {
                    dizi[row, col] = num;
                    z[row][col].Text = num.ToString();
                    System.Threading.Thread.Sleep(80);

                    if (SolveSudoku(dizi))
                        return true;
                    dizi[row, col] = 0;
                }
                if (b[row][col].Text == "0")
                {
                    b[row][col].Text = " ";
                    z[row][col].ForeColor = Color.Blue;
                }
                
            }


            return false;
        }

        bool FindUnassignedLocation(int[,] dizi, ref int row, ref int col)
        {
            for (row = 0; row < n; row++)
                for (col = 0; col < n; col++)
                    if (dizi[row, col] == 0)
                        return true;
            return false;
        }
        bool UsedInRow(int[,] dizi, int row, int num)
        {
            for (int col = 0; col < n; col++)
                if (dizi[row, col] == num)
                    return true;
            return false;
        }
        bool UsedInCol(int[,] dizi, int col, int num)
        {
            for (int row = 0; row < n; row++)
                if (dizi[row, col] == num)
                    return true;
            return false;
        }
        bool UsedInBox(int[,] dizi, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (dizi[row + boxStartRow, col + boxStartCol] == num)
                        return true;
            return false;
        }
        bool isSafe(int[,] dizi, int row, int col, int num)
        {
            return !UsedInRow(dizi, row, num) &&
                   !UsedInCol(dizi, col, num) &&
                   !UsedInBox(dizi, row - row % 3, col - col % 3, num);
        }
        void printGrid(int[,] dizi)
        {
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                   z[row][col].Text = (dizi[row, col]).ToString();

                }

            }
        }
        void printGrid1(int[,] dizim)
        {

            for (int row = 8; row >= 0; row--)
            {
                for (int col = 8; col >= 0; col--)
                {
                    //Control.CheckForIllegalCrossThreadCalls = false;
                    y[row][col].Text = (dizim[row, col]).ToString();
                }

            }

        }
    
     
        
     
        void printGrid2(int[,] dzm)
        {

            for (int row = 8; row >= 0; row--)
            {
                for (int col = 0; col < n; col++)
                {
                    //Control.CheckForIllegalCrossThreadCalls = false;
                    t[row][col].Text = (dzm[row, col]).ToString();


                }

            }

        }

        #endregion
        public void threadkontrol()
        {
            if (!mythread1.IsAlive)
            {
              

                mythread2.Suspend();
                mythread3.Suspend();
           
                


            }
            else if(!mythread2.IsAlive)
            {
                
                mythread1.Suspend();
                mythread3.Suspend();
              
                label2.Text = "ikinci Thread " + x1.ToString() + " sürede bitti ";
            }

            else
            {

              
                mythread1.Suspend();
                mythread2.Suspend();
          
                label3.Text = "üçüncü Thread " + x2.ToString() + " sürede bitti ";






            }




        }
    }
}