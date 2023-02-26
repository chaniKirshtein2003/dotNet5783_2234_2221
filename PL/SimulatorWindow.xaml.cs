using BlApi;
using Simulator;
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
        public SimulatorWindow()
        {
            InitializeComponent();
            bw=new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }
        private void Bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if((int)e.ProgressPercentage==1)
            {
                lblclock.Content = DateTime.Now.ToString();
                return;
            }
            else
            {
                txtId.Text = ((EventStatus)e.UserState!).OrderId.ToString();
                lblStart.Content= ((EventStatus)e.UserState!).start.ToString();
                lblfinish.Content= ((EventStatus)e.UserState!).finish.ToString();
                nowStatus.Content = ((EventStatus)e.UserState!).now.ToString();
                lblnextstatus.Content= ((EventStatus)e.UserState!).will.ToString();
                lblSeconds.Content= ((EventStatus)e.UserState!).seconds;
            }
        }

        private void Bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("stoped");
        }

        private void Bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.ReportStart(repStart);
            Simulator.Simulator.ReportEnd(repEnd);
            Simulator.Simulator.ReportEndSim(repEndSim);
            Simulator.Simulator.Active();
            while (!bw.CancellationPending)
            {
                Thread.Sleep(1000);
                bw.ReportProgress(1);
            }
        }
        private void repStart(object? sender,EventStatus eveS) { bw.ReportProgress(2,eveS); }
        private void repEnd(object? sender,EventStatus eveS) { bw.ReportProgress(2, eveS); }
        private void repEndSim(object? sender, EventArgs e)
        {
            bw.CancelAsync();
            Simulator.Simulator.DereportStart(repStart);
            Simulator.Simulator.DereportEnd(repEnd);
            Simulator.Simulator.DereportEndSim(repEndSim);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.Deactive();
        }
    }
}
