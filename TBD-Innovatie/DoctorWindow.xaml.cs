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
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
            // navigate to the D_Home page
            NavigateToPage("D_Home");
        }
        private void NavigateToPage(string pageName)
        {
            MainFrame.Source = new Uri($"Pages/{pageName}.xaml", UriKind.Relative);
        }

        private void HomeNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the D_Home page
            NavigateToPage("D_Home");
        }

        private void SearchNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the D_Search page
            NavigateToPage("D_Search");
        }

        private void AgendaNav_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the D_Agenda page
            NavigateToPage("D_Agenda");
        }
    }
}
