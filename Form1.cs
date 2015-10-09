using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var str =
            "Ехал древний Грека через реку, видит древний Грека в реке древний рак. Сунул древний Грека руку в реку. Древний рак за руку древнего Греку цап";
            var dictionary = StatisticCalculation.CountFrequencyWords(
                "Ехал Грека через реку, видит Грека в реке рак. Сунул Грека руку в реку. Рак за руку Греку цап​");
            dictionary.ToList().OrderBy(item => item.Value).Reverse().ToList().ForEach(x => textBox1.AppendText(x.Key + " : " + x.Value + Environment.NewLine));
            textBox1.AppendText(StatisticCalculation.MutualInformation(str, "древний грека").ToString() + Environment.NewLine);
            textBox1.AppendText(StatisticCalculation.TScore(str, "древний грека").ToString());
        }
    }
}
