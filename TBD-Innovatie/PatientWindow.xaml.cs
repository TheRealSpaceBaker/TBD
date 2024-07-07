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
using System.Windows.Shapes;

namespace TBD_Innovatie
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        public PatientWindow()
        {
            InitializeComponent();
            // navigate to the P_Home page
            NavigateToPage("P_Home");
        }
        private void NavigateToPage(string pageName)
        {
            MainFrame.Source = new Uri($"Pages/{pageName}.xaml", UriKind.Relative);
        }

        private void HomeNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the P_Home page
            NavigateToPage("P_Home");
        }

        private void StarMapNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the P_StarMap page
            NavigateToPage("P_StarMap");

        }

        private void ProfileNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the P_Profile page
            NavigateToPage("P_Profile");

        }

        private void AgendaNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the P_Agenda page
            NavigateToPage("P_Agenda");

        }


        private void InsightNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the P_Insight page
            NavigateToPage("P_Insight");

        }
    }
}
