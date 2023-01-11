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
    /// Interaction logic for ShowCart.xaml
    /// </summary>
    public partial class ShowCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart _myCart = new BO.Cart();

        public ObservableCollection<BO.OrderItem> item
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(itemProperty); }
            set { SetValue(itemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty itemProperty =
            DependencyProperty.Register("item", typeof(ObservableCollection<BO.OrderItem>), typeof(Window), new PropertyMetadata(null));


        public ShowCart(BO.Cart cart)
        {
            InitializeComponent();
            _myCart = cart;
            var help = _myCart.Items;
            item = help == null ? new() : new (help!);
        }

        //private void Button_Click(object sender, MouseButtonEventArgs e)
        //{
        //}

        //private void Button_Click_1(object sender, MouseButtonEventArgs e)
        //{
        //}
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemId;
            BO.OrderItem orderItem = _myCart.Items!.FirstOrDefault(x => x!.OrderItemId == id)!;
            bl!.Cart.Update(_myCart, orderItem.ProductId, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemId;
            BO.OrderItem orderItem = _myCart.Items!.FirstOrDefault(x => x!.OrderItemId == id)!;
            bl!.Cart.Update(_myCart, orderItem.ProductId, orderItem.Amount + 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemId;
            BO.OrderItem orderItem = _myCart.Items!.FirstOrDefault(x => x!.OrderItemId == id)!;
            bl!.Cart.Update(_myCart, orderItem.ProductId, orderItem.Amount - 1);
        }
    }
}
