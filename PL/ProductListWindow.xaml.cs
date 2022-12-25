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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListview.ItemsSource = bl!.Product.GetProducts(); ;
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            CategoriesSelector.SelectedItem = BO.Categories.None;
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories category = (BO.Categories)CategoriesSelector.SelectedItem;
            if (category.ToString() == "None")
                ProductListview.ItemsSource = bl!.Product.GetProducts();
            else
                ProductListview.ItemsSource = bl!.Product.GetProductsListByCategory(category);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUpdateProduct().ShowDialog();
            ProductListview.ItemsSource = bl!.Product.GetProducts();
            CategoriesSelector.SelectedItem = BO.Categories.None;
        } 

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)((ListView)sender).SelectedItem).ID;
            new AddUpdateProduct(id).ShowDialog();
            ProductListview.ItemsSource = bl!.Product.GetProducts();
            CategoriesSelector.SelectedItem = BO.Categories.None;
        }
    }
}
