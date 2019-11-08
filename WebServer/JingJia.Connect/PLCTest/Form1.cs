using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jingjia.Connect.RestFulService;

namespace PLCTest
{
    public partial class Form1 : Form
    {
        JingJia.PLCDriver.GR10 _plc = new JingJia.PLCDriver.GR10();
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _plc.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _plc.Open("COM4");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _plc.SetOn(int.Parse(textBox1.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _plc.SetOff(int.Parse(textBox1.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RestFulService.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RestFulService.Stop();
        }
    }
}
