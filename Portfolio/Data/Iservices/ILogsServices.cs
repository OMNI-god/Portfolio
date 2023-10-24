using Portfolio.Models;

namespace Portfolio.Data.Iservices
{
    public interface ILogsServices
    {
        List<Logs> getall();
        void add(Logs log);
        void delete(int id);
    }
}
