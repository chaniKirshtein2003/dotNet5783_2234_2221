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
    /// Interaction logic for AddUpdateProduct.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public BO.ProductItem ProductItem
        {
            get { return (BO.ProductItem)GetValue(ProductItemProperty); }
            set { SetValue(ProductItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductItemProperty =
            DependencyProperty.Register("ProductItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));



        public ProductItemWindow(int id)
        {
            InitializeComponent();
            BO.ProductItem prod = bl.Product.ProductForBuyer(id);
            ProductItem= prod;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
