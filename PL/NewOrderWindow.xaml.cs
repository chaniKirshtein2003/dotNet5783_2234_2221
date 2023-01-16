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
            cmxFilterCategories.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            cmxFilterCategories.SelectedItem = BO.Categories.None;
            myCart.Items = null;
            myCart.TotalPrice = 0;
        }

        private void cmxFilterCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories category = (BO.Categories)cmxFilterCategories.SelectedItem;
            if (category.ToString() == "None")
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
            if (((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).InStock == false)
                MessageBox.Show("אזל מהמלאי");
            else
            {
                myCart = bl!.Cart.Add(myCart, id);
                MessageBox.Show("!המוצר נוסף בהצלחה");
            }
        }

        private void btnShowCart_Click(object sender, RoutedEventArgs e) => new ShowCart(myCart).Show();


        private void ProductItemsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductItem)((System.Windows.Controls.ListBox)sender).SelectedItem).ID;
            new ProductItemWindow(id).ShowDialog();
        }
    }
}
