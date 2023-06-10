using Portfolio.Models;

namespace Portfolio.Data.Iservices
{
    public interface ILogsServices
    {
        void add(Logs log);
        List<Logs> getall();

    }
}
