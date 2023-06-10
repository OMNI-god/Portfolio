using Portfolio.Data.Iservices;
using Portfolio.Models;

namespace Portfolio.Data.services
{
    public class LogsServices : ILogsServices
    {
        private readonly AppliDB db;
        public LogsServices(AppliDB db)
        {
            this.db = db;
        }
        public void add(Logs log)
        {
            throw new NotImplementedException();
        }

        public List<Logs> getall()
        {
            return (db.logs.ToList());
        }
    }
}
