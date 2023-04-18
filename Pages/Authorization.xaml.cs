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
using System.Windows.Threading;

namespace Exam.Pages
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        Random random = new Random();
        DispatcherTimer timer = new DispatcherTimer();

        int time = 0;
        string capchaSymbols = "";

        public Authorization()
        {
            InitializeComponent();
            UpdateCapcha();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if (passwdBox_password.Password == "123")
            {
                Window helloWindow = new HelloWindow();
                helloWindow.Show();
                this.Close();
            } else
            {
                loginFailed();
                UpdateCapcha();
                MessageBox.Show("Неправильный логин или пароль");
                StartTimer();
            }
            
        }

        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (time == 10)
            {
                CapchaLogin.Visibility = Visibility.Visible;
                timer.Stop();
                time = 0;
                timer.Tick -= TimerTick;
                txbTime.Visibility = Visibility.Hidden;
                txbTime.Text = Convert.ToString(TimeSpan.FromSeconds(0));
            }
            else
            {
                time += 1;
                txbTime.Text = Convert.ToString(TimeSpan.FromSeconds(time));
            }
        }

        private void btn_checkCapcha_Click(object sender, RoutedEventArgs e)
        {
            if (tbxCapcha.Text == capchaSymbols)
            {
                btn_auth.Visibility = Visibility.Visible;
                Capcha.Visibility = Visibility.Hidden;
                CapchaBox.Visibility = Visibility.Hidden;
                CapchaLogin.Visibility = Visibility.Hidden;
            }
            else
            {
                CapchaLogin.Visibility = Visibility.Visible;
                txbTime.Visibility = Visibility.Visible;
                CapchaLogin.Visibility = Visibility.Hidden;
            }
        }

        private void loginFailed()
        {
            btn_auth.Visibility = Visibility.Collapsed;
            Capcha.Visibility = Visibility.Visible;
            CapchaBox.Visibility = Visibility.Visible;
            CapchaLogin.Visibility= Visibility.Visible;
        }

        private void ShowPassword_Click(object sender, MouseButtonEventArgs e)
        {
            if (passwdBox_password.Visibility == Visibility.Visible)
            {
                passwdBox_password.Visibility = Visibility.Collapsed;
                textbox_password.Text = passwdBox_password.Password;
                textbox_password.Visibility = Visibility.Visible;
            } else
            {
                textbox_password.Visibility = Visibility.Collapsed;
                passwdBox_password.Password = textbox_password.Text;
                passwdBox_password.Visibility = Visibility.Visible;
            }
        }

        private void btnUpdateCapcha_Click(object sender, RoutedEventArgs e)
        {
            UpdateCapcha();
        }

        private void UpdateCapcha()
        {
            stackPanelSymbols.Children.Clear();
            capchaSymbols = "";
            GenerateSymbols(3);
        }

        private void GenerateSymbols(int count)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456890";

            for (int i = 0; i < count; i++)
            {
                string symbols = alpha.ElementAt(random.Next(0, alpha.Length)).ToString();
                TextBlock txtBlockCapcha = new TextBlock();
                txtBlockCapcha.Text = symbols;
                txtBlockCapcha.FontSize = 25;
                txtBlockCapcha.Margin = new Thickness(-3);
                txtBlockCapcha.RenderTransform = new RotateTransform(random.Next(-45, 45));

                capchaSymbols += symbols;
                stackPanelSymbols.Children.Add(txtBlockCapcha);

            }

        }

    }
}
