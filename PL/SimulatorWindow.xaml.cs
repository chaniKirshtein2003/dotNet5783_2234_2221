using BlApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker bw;
        private bool cancel;
        public SimulatorWindow()
        {
            InitializeComponent();
            bw=new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.ProgressChanged += Bw_ProgressChanged;
            lblClock.Content = DateTime.Now.ToString("h:mm:ss");

            //להוסיף 2 דגלים ביטול ועוד משהו!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            bw.RunWorkerAsync();
        }
        private void Bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            //!!!!!!!!!!!!!!!!!!!!!!
            while (!cancel)
            //!!!!!!!!!!!!!!!!!!!!!!
            {
                Thread.Sleep(1000);
                bw.ReportProgress(1);
            }
        }
    }
}
