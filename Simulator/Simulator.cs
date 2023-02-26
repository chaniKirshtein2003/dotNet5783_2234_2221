using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public static class Simulator
    {
        private static event EventHandler ReportStart;
        private static event EventHandler ReportEnd;
        private static event EventHandler ReportEndSim;
        volatile static bool flagActive;
        private static Random rn = new Random();
        private static BlApi.IBl bl = BlApi.Factory.Get();
        public static void Deactivate() => flagActive = false;
        public static void Activate()
        {
            new Thread(() =>
            {
                flagActive = true;
                while (flagActive)
                {
                    int? oldId = bl.Order.GetOldestOrder();
                    if (oldId != null)
                    {
                        BO.Order order = bl.Order.GetOrderDetails((int)oldId);
                        int delay = rn.Next(3, 11);
                        DateTime tm = DateTime.Now + new TimeSpan(delay * 1000);
                        ReportStart(order, new ReportStartEventArgs(tm, order));
                        Thread.Sleep(delay * 1000);
                        bl.Order.UpdateStatus(order.OrderId);
                        ReportEnd();
                    }
                    Thread.Sleep(8000);
                }
                ReportEndSim();
            }).Start();
        }
    }
}