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
using LabProject.Код;

namespace LabProject.Окна
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users(Tools.CurrentPageControl currentPageControl)
        {
            InitializeComponent();
            DataContext = new UsersView();
        }

        private void CloseUsers(object sender, RoutedEventArgs e)
        {
            Users u = new Users(null);
            this.Close();
        }

        private void Update(object sender, MouseButtonEventArgs e)
        {
            Users newUS = new Users(null);
            Application.Current.MainWindow = newUS;
            newUS.Show();
            this.Close();
        }
    }
}
