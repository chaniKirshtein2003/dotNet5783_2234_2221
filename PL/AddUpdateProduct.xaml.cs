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
    public partial class AddUpdateProduct : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        string state;

        public BO.Product product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }

        // Using a DependencyProperty as the backing store for product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));



        //Opening the form in addition mode
        public AddUpdateProduct()
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            state = "add";
        }
        //Opening the form in update mode
        public AddUpdateProduct(int id)
        {
            InitializeComponent();
            cmbCategory.ItemsSource= Enum.GetValues(typeof(BO.Categories));
            product = bl!.Product.GetProduct(id);
            state = "update";
        }
        
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //if (txtName.Text == "" || txtPrAmount.Text == "" || txtPrId.Text == "" || txtPrPrice.Text == "")
            //{
            //    MessageBox.Show("missing details");
            //}
            //else
            //{
            try
            {
                BO.Product product = new BO.Product();
                product.ProductName = txtName.Text;
                product.ProductId = int.Parse(txtPrId.Text);
                product.Price = int.Parse(txtPrPrice.Text);
                product.AmountInStock = int.Parse(txtPrAmount.Text);
                product.Category = (BO.Categories)cmbCategory.SelectedItem;
                if (state == "add")
                    try 
                    {
                        bl!.Product.Add(product);
                        MessageBox.Show("successfull product addition");
                        this.Close();
                    }
                    catch
                    {
                        throw new Exception("addition failed");
                    }
                else if (state == "update")
                    try
                    {
                        bl!.Product.Update(product);
                        MessageBox.Show("successfull product update");
                        this.Close();
                    }
                    catch
                    {
                        throw new Exception("update failed");
                    }
            }
            catch(Exception x)
            {
                MessageBox.Show("Missing details");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
