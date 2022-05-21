using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Probability.Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = "";
            label5.Text = "";
            label6.Text = "";
            label8.Text = "";
            label10.Text = "";
            label12.Text = "";

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }

        private void ShowSearchResult(string text, bool bIsRedColor = false)
        {
            if (bIsRedColor)
            {
                label3.ForeColor = Color.Red;
            }
            else
            {
                label3.ForeColor = SystemColors.ControlText;
            }
            label3.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = SqlSugarTool.GetDb())
                {
                    var ls = db.Queryable<ProbabilityRecords>().ToList();
                    ShowSearchResult(string.Format("查询成功, 共有 {0} 条记录。", CommonTool.FormatNumber(ls.Count)));
                    double[] arr = Statistics(ls);
                    ShowStatistics(arr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private double[] Statistics(IList<ProbabilityRecords> ls)
        {
            int total = ls.Count;

            var count = ls.Where(x => Math.Abs(x.ProbabilityDifference) >= 0.2).Count();   // >= 0.2
            var count2 = ls.Where(x => Math.Abs(x.ProbabilityDifference) >= 0.1 && Math.Abs(x.ProbabilityDifference) < 0.2).Count();   // >= 0.1 & < 0.2
            var count3 = ls.Where(x => Math.Abs(x.ProbabilityDifference) >= 0.05 && Math.Abs(x.ProbabilityDifference) < 0.1).Count();  // >= 0.05 & < 0.1
            var count4 = ls.Where(x => Math.Abs(x.ProbabilityDifference) >= 0.01 && Math.Abs(x.ProbabilityDifference) < 0.05).Count(); // >= 0.05 & < 0.1
            var count5 = ls.Where(x => Math.Abs(x.ProbabilityDifference) < 0.01).Count();  // < 0.01

            double p = Math.Round((double)(count / total), 4);
            double p2 = Math.Round((double)count2 / total, 4);
            double p3 = Math.Round((double)count3 / total, 4);
            double p4 = Math.Round((double)count4 / total, 4);
            double p5 = Math.Round((double)count5 / total, 4);

            return new double[] { p, p2, p3, p4, p5 };
        }

        private void ShowStatistics(double[] arr)
        {
            if (arr.Length >= 0)
            {
                label5.Text = (arr[0] * 100) + "%";
            }
            if (arr.Length >= 1)
            {
                label6.Text = (arr[1] * 100) + "%";
            }
            if (arr.Length >= 2)
            {
                label8.Text = (arr[2] * 100) + "%";
            }
            if (arr.Length >= 3)
            {
                label10.Text = (arr[3] * 100) + "%";
            }
            if (arr.Length >= 4)
            {
                label12.Text = (arr[4] * 100) + "%";
            }
        }
    }
}
