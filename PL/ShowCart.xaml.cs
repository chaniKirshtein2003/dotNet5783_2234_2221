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
            item = help == null ? new() : new(help!);
            if (item == null)
            {
                lblEmpty.Visibility = Visibility.Visible;
                btnOK.Visibility = Visibility.Hidden;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId;
            var x = (bl!.Cart.Update(_myCart, id, 0)).Items;
            item = x == null ? new() : new(x!);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId;
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
            var x = (bl!.Cart.Update(_myCart, id, amount + 1)).Items;
            item = x == null ? new() : new(x!);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId;
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
            var x = (bl!.Cart.Update(_myCart, id, amount - 1)).Items;
            item = x == null ? new() : new(x!);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (item == null)
                    MessageBox.Show("The cart is empty");
                else
                {
                    BO.Cart cart = new BO.Cart();
                    cart.CustomerName = txtName.Text;
                    cart.CustomerAddress = txtAddress.Text;
                    cart.CustomerEmail = txtEmail.Text;
                    cart.Items = item == null ? new() : new(item);
                    cart.TotalPrice = item!.Sum(x => x.TotalPrice);
                    bl!.Cart.MakeAnOrder(cart);
                    MessageBox.Show("הזמנתך התקבלה במערכת. תודה שקנית אצלנו");
                }
            }
            catch (BO.NotValidException ex)
            {
                MessageBox.Show(""+ex);
            }
            //if (txtAddress.Text == "" || txtName.Text == "" || txtEmail.Text == "")

        }
    }
}
