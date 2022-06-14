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
            label17.Text = "";

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double d = 0;
                bool b = double.TryParse(textBox1.Text.Trim(), out d);
                if (b)
                {
                    if (d < 0)
                    {
                        MessageBox.Show("请输入高于0的浮点数!", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请输入正确的浮点数!", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                double d2 = 0;
                bool b2 = double.TryParse(textBox2.Text.Trim(), out d2);
                if (b2)
                {
                    if (d2 > 1)
                    {
                        MessageBox.Show("请输入不超过1的浮点数!", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请输入正确的浮点数!", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                IList<ProbabilityRecords> ls = new List<ProbabilityRecords>();
                using (var db = SqlSugarTool.GetDb())
                {
                    ls = db.Queryable<ProbabilityRecords>().ToList();
                }
                int total = ls.Count;
                ShowSearchResult(string.Format("查询成功, 共有 {0} 条记录。", CommonTool.FormatNumber(total)));
                int count = ls.Where(x => Math.Abs(x.ProbabilityDifference) >= d && Math.Abs(x.ProbabilityDifference) <= d2).Count();
                double p = (double)count / total;
                label17.Text = string.Format("[{0}, {1}]: {2}", d, d2, (Math.Round(p, 4) * 100) + "%");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
