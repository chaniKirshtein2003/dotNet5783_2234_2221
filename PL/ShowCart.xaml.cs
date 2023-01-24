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

        public ObservableCollection<BO.OrderItem> Item
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(ObservableCollection<BO.OrderItem>), typeof(Window), new PropertyMetadata(null));


        public ShowCart(BO.Cart cart)
        {
            InitializeComponent();
            _myCart = cart;
            var help = _myCart.Items;
            Item = help == null ? new() : new(help!);
            //if (Item == null)
            //{
            //    //lblEmpty.Visibility = Visibility.Visible;
            //    //btnOK.Visibility = Visibility.Hidden;
            //}
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId;
            var x = (bl!.Cart.Update(_myCart, id, 0)).Items;
            Item = x == null ? new() : new(x!);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId;
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
            var x = (bl!.Cart.Update(_myCart, id, amount + 1)).Items;
            Item = x == null ? new() : new(x!);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId;
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
            var x = (bl!.Cart.Update(_myCart, id, amount - 1)).Items;
            Item = x == null ? new() : new(x!);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart();
            cart.CustomerName = txtName.Text;
            cart.CustomerAddress = txtAddress.Text;
            cart.CustomerEmail = txtEmail.Text;
            cart.Items = Item == null ? new() : new(Item);
            cart.TotalPrice = Item!.Sum(x => x.TotalPrice);
            try
            {
                bl!.Cart.MakeAnOrder(cart);
            }
            catch (BO.NotValidException ex)
            {
                MessageBox.Show(ex.ToString(),"ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (BO.NotExistBlException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (BO.NotInStockException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //cartItemsLst.Items.Refresh(); //show the updated cart
            //lblPrice.Content = cart.TotalPrice; //update total price
            //lblEmpty.Content = "Order successfully placed!";
            //lblEmpty.Visibility = Visibility.Visible;
            //btnMakeOrder.IsEnabled = false;
            //btnMakeOrder.Foreground = Brushes.DimGray;
        



        MessageBox.Show("Your order has been accepted; Thank you for shopping with us");
            this.Close();
        }
    }
}
