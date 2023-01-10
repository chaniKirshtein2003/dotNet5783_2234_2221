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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<BO.ProductForList> prods
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(prodsProperty); }
            set { SetValue(prodsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prods.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty prodsProperty =
            DependencyProperty.Register("prods", typeof(ObservableCollection<BO.ProductForList>), typeof(Window), new PropertyMetadata(null));

        public ProductListWindow()
        {
            InitializeComponent();
            var help= bl!.Product.GetProducts();
            prods = help == null ? new() : new(help);
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            CategoriesSelector.SelectedItem = BO.Categories.None;
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            BO.Categories category = (BO.Categories)CategoriesSelector.SelectedItem;
            if (category.ToString() == "None")
            {
                var help = bl!.Product.GetProducts();
                prods = help == null ? new() : new(help);
            }
            else
            {
                var help = bl!.Product.GetProductsListByCategory(category);
                prods = help == null ? new() : new(help);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUpdateProduct().ShowDialog();
            CategoriesSelector.SelectedItem = BO.Categories.None;
            var help = bl!.Product.GetProducts();
            prods = help == null ? new() : new(help);
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)((ListView)sender).SelectedItem).ID;
            new AddUpdateProduct(id).ShowDialog();
            var help = bl!.Product.GetProducts();
            prods = help == null ? new() : new(help);
            CategoriesSelector.SelectedItem = BO.Categories.None;
        }
    }
}
