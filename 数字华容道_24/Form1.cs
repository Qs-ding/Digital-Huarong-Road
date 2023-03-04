﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 数字华容道_24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int N = 5;
        Button[,] buttons = new Button[N, N];
        int count;
        private void Form1_Load(object sender, EventArgs e)
        {
            //产生所有按钮
            GenerateAllButtons();
            label2.Text = "0" + " 秒"; //初始时间
            timer1.Start();  //启动计算器
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //默认100毫秒刷新一次
            count += 1;
            label2.Text = (count / 10).ToString() + " 秒";
            if (ResultIsOK())
            {
                timer1.Stop();
                string s = label2.Text;
                if (!((count / 10) == 0))
                {
                    MessageBox.Show("挑战成功,你耗时" + s);
                }
            }
        }

        void Shuffle()
        {
            //多次随机交换两个按钮
            Random rnd = new Random();
            timer1.Stop();
            for (int i = 0; i < 100; i++)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                Swap(buttons[a, b], buttons[c, d]);
            }
        }
        //生成所有按钮
        void GenerateAllButtons()
        {
            int x0 = 50, y0 = 20, w = 95, d = 100;
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = r * N + c;//这个数据用来表示它所在的行列位置
                    //注册事件
                    btn.Click += new EventHandler(btn_Click);
                    buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
            }
            buttons[N - 1, N - 1].Visible = false;  //最后一个不可见
        }
        //交换两个按钮
        void Swap(Button btna, Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;

            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;
        }
        //按钮点击事件处理
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;  //当前点中按钮
            Button blank = FindHiddenButton();  //空白按钮
            //判断与空白按钮是否相邻，如果是，交换
            if (IsNeighbor(btn, blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }
        }
        //查找要隐藏的按钮
        Button FindHiddenButton()
        {
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if (!buttons[r, c].Visible)
                    {
                        return buttons[r, c];
                    }
                }
            }
            return null;
        }
        //判断是否相邻
        bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag;  //Tag中记录行列位置
            int b = (int)btnB.Tag;
            int r1 = a / N, c1 = a % N;
            int r2 = b / N, c2 = b % N;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
            {
                return true;
            }
            return false;
        }
        //检查是否完成
        bool ResultIsOK()
        {
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if (buttons[r, c].Text != (r * N + c + 1).ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Shuffle();
            count = 0;
            timer1.Start();
        }

    }
}
