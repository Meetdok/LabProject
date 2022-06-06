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
using LabProject.Model;
using MySql.Data.MySqlClient;

namespace LabProject
{
    /// <summary>
    /// Логика взаимодействия для Auth2.xaml
    /// </summary>
    public partial class Auth2 : Window
    {
        public Auth2()
        {
            InitializeComponent();
        }

        private void Auth_num(object sender, RoutedEventArgs e)
        {
            var loginUser = LoginNum.Text;
            var db = MySqlDB.GetDB();
            bool enter = false;
            if (db.OpenConnection())
            {
                string querystring = $"select id, phonenumber from users where phonenumber ='{loginUser}'";
                using (MySqlCommand command = new MySqlCommand(querystring, MySqlDB.GetDB().GetConnection()))
                {
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            enter = dr.GetInt32("id") != 0;
                        }
                    }
                }
                db.CloseConnection();
            }
            if (enter)
            {

                MainWindow main = new MainWindow();
                this.Close();
                main.Show();

            }
            else
                MessageBox.Show("Неверно!", "Номера не существет!!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            Auth a = new Auth();
            this.Close();
            a.Show();
            
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Auth2 a = new Auth2();
            this.Close();
            Environment.Exit(0);
        }
    }
}
