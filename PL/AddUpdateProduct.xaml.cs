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
    /// Interaction logic for AddUpdateProduct.xaml
    /// </summary>
    public partial class AddUpdateProduct : Window
    {
        private BlApi.IBl bl = new BlImplementation.Bl();
        string state;

        //Opening the form in addition mode
        public AddUpdateProduct()
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            state = "add";
            btnOK.Content = "להוספה";
        }
        //Opening the form in update mode
        public AddUpdateProduct(int id)
        {
            InitializeComponent();
            cmbCategory.ItemsSource= Enum.GetValues(typeof(BO.Categories));
            BO.Product product = bl.Product.GetProduct(id);
            state = "update";
            btnOK.Content = "לעדכון";
            txtPrId.IsEnabled = false;
            //fill all the textboxes with the attributes of current product
            txtPrId.Text = product.ProductId.ToString();
            txtName.Text = product.ProductName;
            txtPrPrice.Text = product.Price.ToString();
            txtPrAmount.Text = product.AmountInStock.ToString();
            cmbCategory.SelectedItem =product.Category;
        }
        
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPrAmount.Text == "" || txtPrId.Text == "" || txtPrPrice.Text == "")
                MessageBox.Show("missing details");
            BO.Product product = new BO.Product();
            product.ProductName = txtName.Text;
            product.ProductId = int.Parse(txtPrId.Text);
            product.Price = int.Parse(txtPrPrice.Text);
            product.AmountInStock = int.Parse(txtPrAmount.Text);
            product.Category = (BO.Categories)cmbCategory.SelectedItem;
            if (state == "add")
                try
                {
                    bl.Product.Add(product);
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
                    bl.Product.Update(product);
                    MessageBox.Show("successfull product update");
                    this.Close ();
                }
                catch
                {
                    throw new Exception("update failed");
                }
        }
    }
}
