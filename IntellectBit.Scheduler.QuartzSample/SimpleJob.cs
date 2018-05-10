using Common.Logging;
using Quartz;
using System.Threading.Tasks;

public class SimpleJob : IJob
{
    private static ILog logging = LogManager.GetLogger(typeof(SimpleJob));

    public Task Execute(IJobExecutionContext context)
    {
        logging.InfoFormat("Hello from job");
        return null;
    }
}