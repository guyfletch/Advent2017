using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Advent2017
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbDaySelector.SelectedIndex = cbDaySelector.Items.Count - 1;
            var ChristmasDay = new DateTime(2017, 12, 25);
            TimeSpan t = ChristmasDay - DateTime.Today;
            this.Title = this.Title + ": " + t.Days + " days til Christmas!";
            ReColour(false);
            rbPart1.Checked += RbPart1_Checked;
            rbPart2.Checked += RbPart2_Checked;
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            if(cbDaySelector.SelectedIndex != -1)
            {
                string inputString = "";
                if(tbInput.Text != "")
                {
                    inputString = tbInput.Text;
                }
                bool part2 = (bool)rbPart2.IsChecked;
                switch (cbDaySelector.Text)
                {
                    case "Day1":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day1Input;
                        }
                        tbAnswer.Text = new Day1(inputString, part2).answer;
                        break;
                    case "Day2":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day2Input;
                        }
                        tbAnswer.Text = new Day2(inputString, part2).answer;
                        break;
                    case "Day3":
                        if (inputString == "")
                        {
                            inputString = "361527";
                        }
                        tbAnswer.Text = new Day3(inputString).answer;
                        break;
                    case "Day4":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day4Input;
                        }
                        tbAnswer.Text = new Day4(inputString).answer;
                        break;
                    case "Day5":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day5Input;
                        }
                        tbAnswer.Text = new Day5(inputString, part2).answer;
                        break;
                    case "Day6":
                        if(inputString == "")
                        {
                            inputString = "10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6";
                        }
                        tbAnswer.Text = new Day6(inputString, part2).answer;
                        break;
                    case "Day7":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day7Input;
                        }
                        tbAnswer.Text = new Day7(inputString, part2).answer;
                        break;
                    case "Day8":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day8Input;
                        }
                        tbAnswer.Text = new Day8(inputString, part2).answer;
                        break;
                    case "Day9":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day9Input;
                        }
                        tbAnswer.Text = new Day9(inputString, part2).answer;
                        break;
                    case "Day10":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day10Input;
                        }
                        tbAnswer.Text = new Day10(inputString, part2).answer;
                        break;
                    case "Day11":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day11Input;
                        }
                        tbAnswer.Text = new Day11(inputString, part2).answer;
                        break;
                    case "Day12":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day12Input;
                        }
                        tbAnswer.Text = new Day12(inputString, part2).answer;
                        break;
                    case "Day13":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day13Input;
                        }
                        tbAnswer.Text = new Day13(inputString, part2).answer;
                        break;
                    case "Day14":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day14Input;
                        }
                        tbAnswer.Text = new Day14(inputString, part2).answer;
                        break;
                    case "Day15":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day15Input;
                        }
                        tbAnswer.Text = new Day15(inputString, part2).answer;
                        break;
                    case "Day16":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day16Input;
                        }
                        tbAnswer.Text = new Day16(inputString, part2).answer;
                        break;
                    case "Day17":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day17Input;
                        }
                        tbAnswer.Text = new Day17(inputString, part2).answer;
                        break;
                    case "Day18":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day18Input;
                        }
                        tbAnswer.Text = new Day18(inputString, part2).answer;
                        break;
                    case "Day19":
                        if(inputString == "")
                        {
                            inputString = Properties.Resources.Day19Input;
                        }
                        tbAnswer.Text = new Day19(inputString, part2).answer;
                        break;
                }
            }
            else
            {
                lblFeedback.Content = "Select a day before running";
            }
        }

        private void ReColour(bool part2)
        {
            var b1 = new SolidColorBrush(Color.FromArgb(255, 189, 23, 49));
            var b2 = new SolidColorBrush(Color.FromArgb(255, 104, 185, 46));
            if (part2)
            {
                tbInput.Foreground = b2;
                tbAnswer.Foreground = b2;
                btnRun.Foreground = b2;
                cbDaySelector.Foreground = b2;
                rbPart1.Foreground = b2;
                rbPart2.Foreground = b2;
                lblAnswer.Foreground = b2;
                lblDay.Foreground = b2;
                lblFeedback.Foreground = b2;
                lblInput.Foreground = b2;
            }
            else
            {
                tbInput.Foreground = b1;
                tbAnswer.Foreground = b1;
                btnRun.Foreground = b1;
                cbDaySelector.Foreground = b1;
                rbPart1.Foreground = b1;
                rbPart2.Foreground = b1;
                lblAnswer.Foreground = b1;
                lblDay.Foreground = b1;
                lblFeedback.Foreground = b1;
                lblInput.Foreground = b1;
            }
        }

        private void RbPart1_Checked(object sender, RoutedEventArgs e)
        {
            ReColour(false);
        }

        private void RbPart2_Checked(object sender, RoutedEventArgs e)
        {
            ReColour(true);
        }
    }
}
