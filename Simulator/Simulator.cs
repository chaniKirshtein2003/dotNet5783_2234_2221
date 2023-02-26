namespace Simulator;
public static class Simulator
{
    private static event EventHandler<EventStatus>? reportStart;
    private static event EventHandler<EventStatus>? reportEnd;
    private static event EventHandler? reportEndSim;

    private static BlApi.IBl bl = BlApi.Factory.Get();
    volatile private static bool isActive;

    public static void Deactive() => isActive = false;

    public static void Active()
    {
        Random rn = new Random();
        new Thread(() =>
        {
            isActive = true;
            while (isActive)
            {
                int? oldId = bl.Order.GetOldestOrder();
                if (oldId != null)
                {
                    BO.Order boOrder = bl.Order.GetOrderDetails((int)oldId);
                    int delay = rn.Next(3, 11);
                    DateTime now = DateTime.Now;
                    DateTime time = now.AddSeconds(delay);
                    BO.OrderStatus willl = (BO.OrderStatus)(((int)boOrder.Status!) + 1);
                    EventStatus args = new EventStatus()
                    {
                        OrderId = (int)oldId,
                        start = now,
                        finish = time,
                        now = (BO.OrderStatus)boOrder.Status,
                        will = willl,
                        seconds=delay
                    };
                    reportStart?.Invoke(null, args);
                    Thread.Sleep(delay * 1000);
                    EventStatus args1 = new EventStatus()
                    {
                        OrderId = (int)oldId
                    };
                    bl.Order.UpdateStatus((int)oldId);
                }
                Thread.Sleep(1000);
            }
            reportEndSim?.Invoke(null, EventArgs.Empty);
        }).Start();
    }

    public static void ReportStart(EventHandler<EventStatus> ev) => reportStart += ev;

    public static void DereportStart(EventHandler<EventStatus> ev) => reportStart -= ev;

    public static void ReportEnd(EventHandler<EventStatus> ev) => reportEnd += ev;
    public static void DereportEnd(EventHandler<EventStatus> ev) => reportEnd -= ev;

    public static void ReportEndSim(EventHandler ev) => reportEndSim += ev;
    public static void DereportEndSim(EventHandler ev) => reportEndSim -= ev;

}

public class EventStatus : EventArgs
{
    public int OrderId { get; set; }
    public DateTime start { get; set; }
    public DateTime finish { get; set; }
    public int seconds { get; set; }
    public BO.OrderStatus now { get; set; }
    public BO.OrderStatus will { get; set; }

}