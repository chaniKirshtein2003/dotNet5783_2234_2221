using BO;
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
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart myCart = new BO.Cart();

        public ObservableCollection<BO.ProductItem> NewOrder
        {
            get { return (ObservableCollection<BO.ProductItem>)GetValue(NewOrderProperty); }
            set { SetValue(NewOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewOrderProperty =
            DependencyProperty.Register("NewOrder", typeof(ObservableCollection<BO.ProductItem>), typeof(Window), new PropertyMetadata(null));

        public NewOrderWindow()
        {
            InitializeComponent();
            var help = bl!.Product.ListProductsToBuy();
            NewOrder = help == null ? new() : new(help);
            myCart.Items = null;
            myCart.TotalPrice = 0;
        }

        private void cmxFilterCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories category = (BO.Categories)cmxFilterCategories.SelectedItem;
            if (category.ToString() == "Choose_Category")
            {
                var help = bl!.Product.ListProductsToBuy();
                NewOrder = help == null ? new() : new(help);
            }
            else
            {
                var help = bl!.Product.GetProductsItemByCategory(category);
                NewOrder = help == null ? new() : new(help);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).ID;
            int amount = ((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
            if (((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).InStock == false)
                MessageBox.Show("Out of stock");
            else
            {
                myCart = bl!.Cart.UpdateAmount(myCart, id, amount);
                MessageBox.Show("The product has been successfully added!");
            }
        }

        private void btnShowCart_Click(object sender, RoutedEventArgs e) => new ShowCart(myCart).Show();

        private void ProductItemsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductItem)((System.Windows.Controls.ListBox)sender).SelectedItem).ID;
            new ProductItemWindow(id, myCart).ShowDialog();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = (sender as TextBox)!;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other  controls
            return;

        }
    }
}
