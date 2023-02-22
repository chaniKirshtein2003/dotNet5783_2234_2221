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


        public ObservableCollection<BO.ProductForList> Prods
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(ProdsProperty); }
            set { SetValue(ProdsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prods.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProdsProperty =
            DependencyProperty.Register("Prods", typeof(ObservableCollection<BO.ProductForList>), typeof(Window), new PropertyMetadata(null));

        public ProductListWindow()
        {
            InitializeComponent();
            var help= bl!.Product.GetProducts();
            Prods = help == null ? new() : new(help);
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            BO.Categories category = (BO.Categories)CategoriesSelector.SelectedItem;
            if (category.ToString() == "Choose_Category")
            {
                var help = bl!.Product.GetProducts();
                Prods = help == null ? new() : new(help);
            }
            else
            {
                var help = bl!.Product.GetProductsListByCategory(category);
                Prods = help == null ? new() : new(help);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUpdateProduct().ShowDialog();
            var help = bl!.Product.GetProducts();
            Prods = help == null ? new() : new(help);
            CategoriesSelector.SelectedItem = BO.Categories.Choose_Category;
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)((ListView)sender).SelectedItem).ID;
            new AddUpdateProduct(id).ShowDialog();
            var help = bl!.Product.GetProducts();
            Prods = help == null ? new() : new(help);
            CategoriesSelector.SelectedItem = BO.Categories.Choose_Category;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp = bl!.Product.PopularProductItems();
            Prods = temp == null ? new() : new(temp!);
        }
    }
}
