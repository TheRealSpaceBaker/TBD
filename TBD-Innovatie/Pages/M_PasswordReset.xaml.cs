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

namespace TBD_Innovatie.Pages
{
    /// <summary>
    /// Interaction logic for M_PasswordReset.xaml
    /// </summary>
    public partial class M_PasswordReset : Page
    {
        public M_PasswordReset()
        {
            InitializeComponent();
        }

        private void LoginPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Navigate to the login page
            this.NavigationService.Navigate(new Uri("Pages/M_Login.xaml", UriKind.Relative));
        }

        private void PasswordResetEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // If the email textbox is focused, clear the text. If it's not focused, set the text to "E-mailadres"
            if (PasswordResetEmail.IsKeyboardFocused)
            {
                PasswordResetEmail.Text = "";
            }
            else if (PasswordResetEmail.Text == "")
            {
                PasswordResetEmail.Text = "E-mailadres";
            }
        }

        private void PasswordResetButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PasswordResetEmail.Text != "" && PasswordResetEmail.Text != "E-mailadres")
            {
                PasswordResetButton.Fill = new SolidColorBrush(Color.FromArgb(255, 122, 188, 255));
                PasswordResetButtonLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 39, 51, 74));
            }

        }

        private void PasswordResetButton_MouseLeave(object sender, MouseEventArgs e)
        {
            PasswordResetButton.Fill = new SolidColorBrush(Color.FromArgb(255, 17, 21, 31));
            PasswordResetButtonLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        }

        private void PasswordResetButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the email is valid
            if (PasswordResetEmail.Text == "" || PasswordResetEmail.Text == "E-mailadres")
            {
                MessageBox.Show("Vul een geldig e-mailadres in.");
            }
            else
            {
                MessageBox.Show("Er is een e-mail verstuurd naar " + PasswordResetEmail.Text + " met instructies om je wachtwoord te resetten.");
            }

        }
    }
}
