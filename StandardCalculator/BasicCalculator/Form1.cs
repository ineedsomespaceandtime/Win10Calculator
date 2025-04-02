using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();            
        }

     
        Double result = 0;
        string operation = string.Empty;
        string fstNum, secNum;
        bool enterValue = false;

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
            TxtDisplay2.Text = "0";
            result = 0;
            enterValue = false;
            RtBoxDisplayHistory.Clear();

        }

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            if (result != 0) BtnEqual.PerformClick();
            else if (!string.IsNullOrEmpty(TxtDisplay1.Text))
                result = Convert.ToDouble(TxtDisplay1.Text);

            Button button = (Button)sender;
            operation = button.Text;
            enterValue = true;

            if (TxtDisplay1.Text != "0")
            {
                TxtDisplay2.Text = fstNum = $"{result} {operation} ";
                TxtDisplay1.Text = string.Empty;
            }

        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            secNum = TxtDisplay1.Text;
            TxtDisplay2.Text = $"{TxtDisplay2.Text}{TxtDisplay1.Text}";
            if (TxtDisplay1.Text != string.Empty)
            {
                secNum = TxtDisplay1.Text;
                TxtDisplay2.Text = $"{TxtDisplay2.Text}{TxtDisplay1.Text}";

                if (!string.IsNullOrEmpty(TxtDisplay1.Text))
                {
                    try
                    {
                        switch (operation)
                        {
                            case "+":
                                TxtDisplay1.Text = (result + Convert.ToDouble(TxtDisplay1.Text)).ToString();
                                break;
                            case "-":
                                TxtDisplay1.Text = (result - Convert.ToDouble(TxtDisplay1.Text)).ToString();
                                break;
                            case "÷":
                                if (TxtDisplay1.Text == "0")
                                {
                                    MessageBox.Show("Cannot divide by zero.");
                                    return;
                                }
                                TxtDisplay1.Text = (result / Convert.ToDouble(TxtDisplay1.Text)).ToString();
                                break;
                            case "x":
                                TxtDisplay1.Text = (result * Convert.ToDouble(TxtDisplay1.Text)).ToString();
                                break;
                        }

                        RtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {TxtDisplay1.Text}\n");
                        result = Convert.ToDouble(TxtDisplay1.Text);
                        operation = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
              
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            PnlHistory.Height = (PnlHistory.Height == 5) ? 345 : 5;
            if (string.IsNullOrEmpty(RtBoxDisplayHistory.Text))
                RtBoxDisplayHistory.Text = "No History Yet";

        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text.Length > 0)
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length - 1, 1);
            if (string.IsNullOrEmpty(TxtDisplay1.Text)) TxtDisplay1.Text = "0";
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";

        }

        private void BtnOperations_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;

            try
            {
                switch (operation)
                {
                    case "√x":
                        if (Convert.ToDouble(TxtDisplay1.Text) < 0)
                        {
                            MessageBox.Show("Cannot calculate the square root of a negative number.");
                            return;
                        }
                        TxtDisplay2.Text = $"√({TxtDisplay1.Text})";
                        TxtDisplay1.Text = Math.Sqrt(Convert.ToDouble(TxtDisplay1.Text)).ToString();
                        break;
                    case "^2":
                        TxtDisplay2.Text = $"({TxtDisplay1.Text})²";
                        TxtDisplay1.Text = Math.Pow(Convert.ToDouble(TxtDisplay1.Text), 2).ToString();
                        break;
                    case "¹⁄ₓ":
                        if (TxtDisplay1.Text == "0")
                        {
                            MessageBox.Show("Cannot divide by zero.");
                            return;
                        }
                        TxtDisplay2.Text = $"1/({TxtDisplay1.Text})";
                        TxtDisplay1.Text = (1 / Convert.ToDouble(TxtDisplay1.Text)).ToString();
                        break;
                    case "±":
                        TxtDisplay1.Text = Convert.ToString(-1 * Convert.ToDouble(TxtDisplay1.Text));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void TxtDisplay1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text == "0" || enterValue) TxtDisplay1.Text = string.Empty;

            enterValue = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!TxtDisplay1.Text.Contains("."))
                    TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
            }
            else TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
        }
    } 
}
