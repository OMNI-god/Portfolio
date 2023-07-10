using Portfolio.Data.Iservices;
using Portfolio.Models;

namespace Portfolio.Data.services
{
    public class LogsServices : ILogsServices
    {
        private readonly AppliDB db;
        private readonly IHttpContextAccessor session;
        public LogsServices(AppliDB db, IHttpContextAccessor session)
        {
            this.db = db;
            this.session = session;

        }
        public List<Logs> getall()
        {
            return db.logs.Where(x => x.Uemail == session.HttpContext.Session.GetString("email")).ToList();
        }

        public void Add(Logs log)
        {
            db.logs.Add(log);
            db.SaveChanges();
        }
    }
}
