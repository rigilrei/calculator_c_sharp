using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        private double firstNumber = 0;
        private double secondNumber = 0;
        private string operation = "";
        private bool isOperationPerformed = false;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            if (textBoxResult.Text == "0" || isOperationPerformed)
            {
                textBoxResult.Clear();
                isOperationPerformed = false;
            }
            
            if (button.Text == ".")
            {
                if (!textBoxResult.Text.Contains("."))
                {
                    textBoxResult.Text += button.Text;
                }
            }
            else
            {
                textBoxResult.Text += button.Text;
            }
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            if (firstNumber != 0)
            {
                buttonEquals.PerformClick();
            }
            
            firstNumber = double.Parse(textBoxResult.Text);
            operation = button.Text;
            isOperationPerformed = true;
            labelCurrentOperation.Text = $"{firstNumber} {operation}";
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(operation)) return;
            
            secondNumber = double.Parse(textBoxResult.Text);
            labelCurrentOperation.Text = $"{firstNumber} {operation} {secondNumber} =";
            
            try
            {
                switch (operation)
                {
                    case "+":
                        textBoxResult.Text = (firstNumber + secondNumber).ToString();
                        break;
                    case "-":
                        textBoxResult.Text = (firstNumber - secondNumber).ToString();
                        break;
                    case "×":
                        textBoxResult.Text = (firstNumber * secondNumber).ToString();
                        break;
                    case "÷":
                        if (secondNumber == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        textBoxResult.Text = (firstNumber / secondNumber).ToString();
                        break;
                }
                
                firstNumber = double.Parse(textBoxResult.Text);
                operation = "";
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Нельзя делить на ноль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearAll();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            textBoxResult.Text = "0";
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            labelCurrentOperation.Text = "";
            isOperationPerformed = false;
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text.Length > 1)
            {
                textBoxResult.Text = textBoxResult.Text.Remove(textBoxResult.Text.Length - 1);
            }
            else
            {
                textBoxResult.Text = "0";
            }
        }

        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text != "0")
            {
                if (!textBoxResult.Text.Contains("-"))
                {
                    textBoxResult.Text = "-" + textBoxResult.Text;
                }
                else
                {
                    textBoxResult.Text = textBoxResult.Text.Substring(1);
                }
            }
        }
    }
}
