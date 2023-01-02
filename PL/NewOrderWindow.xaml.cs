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


        public ObservableCollection<BO.ProductItem> newOrder
        {
            get { return (ObservableCollection<BO.ProductItem>)GetValue(newOrderProperty); }
            set { SetValue(newOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newOrderProperty =
            DependencyProperty.Register("newOrder", typeof(ObservableCollection<BO.ProductItem>), typeof(Window), new PropertyMetadata(null));


        public NewOrderWindow()
        {
            InitializeComponent();
            var help = bl!.Product.ListProductsToBuy();
            newOrder = help == null ? new() : new(help);
            cmxFilterCategories.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            cmxFilterCategories.SelectedItem = BO.Categories.None;
        }

        private void cmxFilterCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories category = (BO.Categories)cmxFilterCategories.SelectedItem;
            if (category.ToString() == "None")
            {
                var help = bl!.Product.ListProductsToBuy();
                newOrder = help == null ? new() : new(help);
            }
            else
            {
                var help = bl!.Product.GetProductsItemByCategory(category);
                newOrder = help == null ? new() : new(help);
            }
        }
    }
}
