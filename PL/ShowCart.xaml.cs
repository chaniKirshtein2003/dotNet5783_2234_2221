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
    /// Interaction logic for ShowCart.xaml
    /// </summary>
    public partial class ShowCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart _myCart = new BO.Cart();


        public IObservable<BO.OrderItem> item
        {
            get { return (IObservable<BO.OrderItem>)GetValue(itemProperty); }
            set { SetValue(itemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty itemProperty =
            DependencyProperty.Register("item", typeof(IObservable<BO.OrderItem>), typeof(Window), new PropertyMetadata(null));


        public ShowCart(BO.Cart cart)
        {
            InitializeComponent();
            _myCart = cart;
        }
    }
}
