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
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Order? order;

        public BO.OrderTracking orderTracking
        {
            get { return (BO.OrderTracking)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingProperty =
            DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        public OrderTracking(int idOrder)
        {
            InitializeComponent();
            try
            {
                //order = bl.Order.GetOrderDetails(idOrder);
                orderTracking = bl.Order.OrderTracking(idOrder);
                lstTracking.ItemsSource = orderTracking.Tracking;
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, There is no an order with id " + idOrder);
            }
        }

        private void btnShowOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtOrderId.Text);
            new UpdateOrder(id).ShowDialog();
        }
    }
}
