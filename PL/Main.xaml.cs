﻿using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }


        private void btnOrderTracking_Click(object sender, RoutedEventArgs e) => new EnterOrderTracking().Show();

        private void btnManager_Click(object sender, RoutedEventArgs e) => new ManagerDisplay().Show();
       
        private void btnNewOrder_Click(object sender, RoutedEventArgs e) => new NewOrderWindow().Show();

        private void Button_Click(object sender, RoutedEventArgs e) => new SimulatorWindow().Show();
    }
}
