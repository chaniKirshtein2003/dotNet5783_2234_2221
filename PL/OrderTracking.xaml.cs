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

        public BO.OrderTracking OrderTrack
        {
            get { return (BO.OrderTracking)GetValue(OrderTrackProperty); }
            set { SetValue(OrderTrackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackProperty =
            DependencyProperty.Register("OrderTrack", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        public OrderTracking(int idOrder)
        {
            InitializeComponent();
            try
            {
                //order = bl.Order.GetOrderDetails(idOrder);
                OrderTrack = bl.Order.OrderTracking(idOrder);
            }
            catch (Exception)
            {
            }
        }

        private void btnShowOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtOrderId.Text);
            new UpdateOrder(id, true).ShowDialog();
        }
    }
}
