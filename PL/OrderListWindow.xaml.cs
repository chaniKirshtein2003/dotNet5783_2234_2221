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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<BO.OrderForList?> Ords
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(OrdsProperty); }
            set { SetValue(OrdsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ords. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdsProperty =
            DependencyProperty.Register("Ords", typeof(ObservableCollection<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));
        public OrderListWindow()
        {
            InitializeComponent();
            var help = bl!.Order.GetOrders();
            Ords = help == null ? new() : new(help);
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.OrderForList)((ListView)sender).SelectedItem).ID;
            new UpdateOrder(id).ShowDialog();
            var help = bl!.Order.GetOrders();
            Ords = help == null ? new() : new(help);   
        }
    }
}
