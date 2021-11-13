using System.Threading;

namespace Blog.EndPoint.Infrastructure.Schedules
{
    public class HangFireSchedules
    {
        public static void HangFireJobRecurring()
        {
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;


            //RecurringJob.RemoveIfExists(nameof(KeepAliveTask));
            //RecurringJob.AddOrUpdate<KeepAliveTask>
            //(nameof(KeepAliveTask),
            //    job => job.Execute(cancellationToken),
            //    Cron.Minutely, 
            //    TimeZoneInfo.Local);



        }
    }
}
