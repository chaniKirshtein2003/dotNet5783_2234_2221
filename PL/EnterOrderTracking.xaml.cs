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
    /// Interaction logic for EnterOrderTracking.xaml
    /// </summary>
    public partial class EnterOrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public EnterOrderTracking()
        {
            InitializeComponent();
        }

        private void btnTracking_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtInsertId.Text);
            new OrderTracking(id).ShowDialog();
        }
    }
}
