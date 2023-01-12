using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Order order
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public UpdateOrder(int id, bool isTrack =false)
        {
            InitializeComponent();
            order = bl!.Order.GetOrderDetails(id);
            if (isTrack)
            {
                btnUpdateOrder.Visibility = Visibility.Hidden;
                btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            order = bl!.Order.supplyUpdate(order.OrderId);
        }

        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            order = bl!.Order.UpdateSending(order.OrderId);
        }
    }
}
