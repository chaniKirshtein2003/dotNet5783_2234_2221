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
        public AddUpdateProduct(BlApi.IBl _bl)
        {
            InitializeComponent();
            bl = _bl;
            state = "add";
            btnOK.Content = state;
        }
    }
}
