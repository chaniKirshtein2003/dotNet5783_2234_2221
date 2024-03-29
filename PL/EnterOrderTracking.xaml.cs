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

namespace PL
{
    /// <summary>
    /// Interaction logic for EnterOrderTracking.xaml
    /// </summary>
    public partial class EnterOrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public EnterOrderTracking()
        {
            InitializeComponent();
        }

        private void btnTracking_Click(object sender, RoutedEventArgs e)
        {
            if (txtInsertId.Text == "")
                MessageBox.Show("Please enter an order number");
            else
            {
                int id = int.Parse(txtInsertId.Text);
                try
                {
                    var x = bl!.Order.GetOrderDetails(id);
                    new OrderTracking(id).ShowDialog();

                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, There is no an order with id " + id);
                    txtInsertId.Text = "";
                }
            }
        }
    }
}
