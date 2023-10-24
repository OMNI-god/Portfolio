using Portfolio.Data.Iservices;
using Portfolio.Models;

namespace Portfolio.Data.services
{
    public class UserServices:IUserServices
    {
        private readonly AppliDB db;
        private readonly IHttpContextAccessor session;
        public UserServices(AppliDB db,IHttpContextAccessor session)
        {
            this.db = db;
            this.session = session;
        }

        public bool getUser(string mailID)
        {
            return db.users.Any(x => x.EmailId.Equals(mailID));
        }

        public void login(User user)
        {
            var data = db.users.FirstOrDefault(x => x.EmailId == user.EmailId && x.Password == user.Password);
            if (data != null)
            {
                session.HttpContext.Session.SetString("login", "true");
                session.HttpContext.Session.SetString("email", data.EmailId);
                session.HttpContext.Session.SetString("name", data.FullName);
            }
        }

        public void logout()
        {
            session.HttpContext.Session.Clear(); 
        }

        public void register(User user)
        {
            db.users.Add(user);
            db.SaveChanges();
        }
    }
}
