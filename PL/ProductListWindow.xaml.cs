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
        private BlApi.IBl bl = new BlImplementation.Bl();
        IEnumerable<BO.ProductForList> products;
        public ProductListWindow(BlApi.IBl _bl)
        {
            InitializeComponent();
            bl = _bl;
            products = bl.Product.GetProducts();
            ProductListview.ItemsSource = products;
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories categories = (BO.Categories)CategoriesSelector.SelectedItem;
            //products = bl.Product.GetProduct();
        }
    }
}
