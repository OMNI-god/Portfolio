using Portfolio.Data.Iservices;
using Portfolio.Models;
using System.Net.NetworkInformation;

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
                savingsCal(data);
            }
        }

        private void savingsCal(User user)
        {
            bool updateCheck = user.DOJ.HasValue || user.Last_update_date.HasValue;
            if (updateCheck)
            {
                var Ldata = db.logs.Where(x => x.Uemail == user.EmailId && x.Action.ToLower().Equals("matured"));
                double salary = user.Salary != null ? user.Salary.Value : 0;
                double bank_return = Ldata.Sum(x => x.Maturity_Amount);
                double total_savings = user.Total_savings!=null?user.Total_savings.Value:0;
                if (!user.Last_update_date.HasValue && (user.DOJ.Value.Month < DateTime.Now.Month || DateTime.Now > user.DOJ.Value.Date))
                {
                    double diff = (DateTime.Now.Year - user.DOJ.Value.Year) * 12 + (DateTime.Now.Month - user.DOJ.Value.Month);
                    if (salary > 0 && diff > 0)
                    {
                        salary *= diff;
                        total_savings += salary + bank_return;
                        user.Last_update_date = DateTime.Now.Date;
                        user.Total_savings = total_savings;
                        db.users.Update(user);
                        db.SaveChanges();
                    }
                }
                else if(user.Last_update_date.Value.Month< DateTime.Now.Month || DateTime.Now > user.DOJ.Value.Date)
                {
                    double diff = (DateTime.Now.Year - user.Last_update_date.Value.Year) * 12 + (DateTime.Now.Month - user.Last_update_date.Value.Month);
                    if (salary > 0 && diff > 0)
                    {
                        salary *= diff;
                        total_savings += salary + bank_return;
                        user.Last_update_date = DateTime.Now.Date;
                        user.Total_savings = total_savings;
                        db.users.Update(user);
                        db.SaveChanges();
                    }
                }
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
