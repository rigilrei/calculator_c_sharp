using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_05_308;

public partial class MainWindow : Window
{
    private decimal x = 0;
    private decimal y = 0;
    private string z = "";
    private int cnt = 0;
    private bool PointFlag = false;
    private bool SignFlag = false;
    public MainWindow()
    {
        InitializeComponent();
    }

    void onClickNumber(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        var number = (string)btn.Content;

        if (SignFlag)
        {
            PointFlag = false;
            SignFlag = false;
            cnt = 0;
        }

        if (number == ",")
        {
            if (!PointFlag)
            {
                Calc.Text += number;
                PointFlag = true;
            }
            return;
        }


        Calc.Text += number;
        if (!PointFlag)
        {
            y = y * 10 + decimal.Parse(number);
        }
        else
        {
            cnt++;
            y = y + decimal.Parse(number) / (decimal)Math.Pow(10, cnt);

        }
    }

    void onClickSign(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        var sign = (string)btn.Content;

        if (z == "")
        {
            x = y;
        }
        else
        {
            switch (z)
            {
                case "+": x = x + y; break;
                case "-": x = x - y; break;
                case "x": x = x * y; break;
                case "/":
                    if (y == 0)
                    {
                        MessageBox.Show("ватафак, делить на ноль нельзя!");
                        return;
                    }
                    x = x / y;
                    break;
            }
        }
        z = sign;
        y = 0;
        if (z == "+" || z == "-" || z == "x" || z == "/")
        {
            Calc.Text = FormatNumber(x) + z;
            SignFlag = true;
        }
        else if (z == "=")
        {
            Calc.Text = FormatNumber(x);
        }
        else if (z == "+/-")
        {
            if (y != 0)
            {
                y = -y;
                Calc.Text = FormatNumber(y);
            }
            else
            {
                x = -x;
                Calc.Text = FormatNumber(x);
            }
        }
    }
    void onClickDelete(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        var dell = (string)btn.Content;
        if (dell == "C")
        {
            Calc.Text = "";
            x = 0;
            y = 0;
            z = "";
            PointFlag = false;
            SignFlag = false;
            cnt = 0;
        }
        else if (dell == "del")
        {
            if (!string.IsNullOrEmpty(Calc.Text))
            {
                Calc.Text = Calc.Text.Substring(0, Calc.Text.Length - 1);

                if (Calc.Text.Length > 0 && !Calc.Text.Contains(","))
                {
                    PointFlag = false;
                }

                if (Calc.Text.Length == 0)
                {
                    y = 0;
                }
            }
        }
    }
    private string FormatNumber(decimal number)
    {
        string result = number.ToString(CultureInfo.CurrentCulture);
        if (result.Contains(","))
        {
            result = result.TrimEnd('0');
            if (result.EndsWith(","))
            {
                result = result.Substring(0, result.Length - 1);
            }
        }
        return result;
    }
}
