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
    /// Interaction logic for M_Login.xaml
    /// </summary>
    public partial class M_Login : Page
    {
        public M_Login()
        {
            InitializeComponent();
        }

        string patientEmail = "layne@email.com";
        string patientPassword = "1234";

        string doctorEmail = "lautrec@email.com";
        string doctorPassword = "0000"; // Dark Souls reference


        private void patientButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            // Close the main window and open the patient window
            PatientWindow patientWindow = new PatientWindow();
            patientWindow.Show();
            Window.GetWindow(this).Close();

        }

        private void doctorButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindow doctorWindow = new DoctorWindow();
            doctorWindow.Show();
            Window.GetWindow(this).Close();

        }

        private void LoginEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // If the email textbox is focused, clear the text. If it's not focused, set the text to "E-mailadres"
            if (LoginEmail.IsKeyboardFocused)
            {
                LoginEmail.Text = "";
            }
            else if (LoginEmail.Text == "")
            {
                LoginEmail.Text = "E-mailadres";
            }
        }

        private void LoginPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // If the password textbox is focused, clear the text. If it's not focused, set the text to "Wachtwoord"
            if (LoginPassword.IsKeyboardFocused)
            {
                LoginPassword.Text = "";
            }
            else if (LoginPassword.Text == "")
            {
                LoginPassword.Text = "Wachtwoord";
            }

        }

        private void LoginButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (LoginEmail.Text != "" && LoginPassword.Text != "" && LoginEmail.Text != "E-mailadres" && LoginPassword.Text != "Wachtwoord")
            {
                LoginButton.Fill = new SolidColorBrush(Color.FromArgb(255, 122, 188, 255));
                LoginButtonLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 39, 51, 74));
            }

        }

        private void LoginButton_MouseLeave(object sender, MouseEventArgs e)
        {
            // when mouse leaves, set the color back to the original color (#FF11151F)

            //#FF11151F
            LoginButton.Fill = new SolidColorBrush(Color.FromArgb(255, 17, 21, 31));

            LoginButtonLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        }

        private void LoginButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginEmail.Text) ||
                string.IsNullOrWhiteSpace(LoginPassword.Text) ||
                LoginEmail.Text == "E-mailadres" ||
                LoginPassword.Text == "Wachtwoord")
            {
                MessageBox.Show("Oeps! Vergeet niet om zowel een e-mailadres als een wachtwoord in te vullen!");
                return;
            }
            // else if the email and password are equal to patientEmail and patientPassword, open the patient window
            else if (LoginEmail.Text == patientEmail && LoginPassword.Text == patientPassword)
            {
                PatientWindow patientWindow = new PatientWindow();
                patientWindow.Show();
                Window.GetWindow(this).Close();
            }
            // else if the email and password are equal to doctorEmail and doctorPassword, open the doctor window
            else if (LoginEmail.Text == doctorEmail && LoginPassword.Text == doctorPassword)
            {
                DoctorWindow doctorWindow = new DoctorWindow();
                doctorWindow.Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Oeps! De accountgegevens kloppen niet, probeer het opnieuw of neem contact op met de organisatie!");
            }


        }

        private void ForgotPasswordPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Navigate to the M_PasswordReset page
            M_PasswordReset passwordResetPage = new M_PasswordReset();
            this.NavigationService.Navigate(passwordResetPage);

        }
    }
}
