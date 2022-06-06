﻿using System;
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
using LabProject.ViewModel;

namespace LabProject
{
    /// <summary>
    /// Логика взаимодействия для Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        public Connection(Tools.CurrentPageControl currentPageControl)
        {
            InitializeComponent();
            DataContext = new ConnectionKod(passwordBox, currentPageControl);
        }

        private void Close_Connection(object sender, RoutedEventArgs e)
        {
            Connection v = new Connection(null);
            this.Close();
        }
    }
}
