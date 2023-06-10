using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Portfolio.Data.Iservices;
using Portfolio.Models;

namespace Portfolio.Data.services
{
    public class InvestmentServices : IInvestmentServices
    {
        private readonly AppliDB db;
        private readonly IHttpContextAccessor session;
        public InvestmentServices(AppliDB db, IHttpContextAccessor session)
        {
            this.db = db;
            this.session = session;
        }

        public void add(Investment investment)
        {
            investment.Time_Left_To_Mature = duration(investment);
            investment.Uemail = session.HttpContext.Session.GetString("email");
            investment.lastUpdate = DateTime.Today;
            investment.Time_Left_To_Mature = duration(investment);
            db.investments.Add(investment);
            db.SaveChanges();
        }

        public Investment getbyid(int id)
        {
            return db.investments.FirstOrDefault(x => x.Id == id);
        }

        public List<Investment> getAll()
        {
            duration();
            ledger();
            return db.investments.Where(x => x.Uemail == session.HttpContext.Session.GetString("email")).ToList();
        }

        private void ledger()
        {
            double inves = 0;
            double matur = 0;
            var data=db.investments.Where(x => x.Uemail == session.HttpContext.Session.GetString("email")).ToList();
            foreach(var item in data)
            {
                inves += item.Investment_Amount;
                matur += item.Maturity_Amount;
            }
            session.HttpContext.Session.SetString("inv", inves.ToString());
            session.HttpContext.Session.SetString("mat", matur.ToString());
        }

        public void remove(int id)
        {
            db.investments.Remove(getbyid(id));
            db.SaveChanges();
        }

        public void update(Investment investment)
        {
            var data = getbyid(investment.Id);
            data.Number = investment.Number;
            data.Bank_Name = investment.Bank_Name;
            data.Type = investment.Type;
            data.ROI = investment.ROI;
            data.Investment_Start_Date = investment.Investment_Start_Date;
            data.Maturity_Amount = investment.Maturity_Amount;
            data.Investment_Amount = investment.Investment_Amount;
            data.Maturity_Date = investment.Maturity_Date;
            data.Time_Left_To_Mature = duration(data);
            data.lastUpdate = DateTime.Today;
            db.investments.Update(data);
            db.SaveChanges();
        }

        public string duration(Investment item)
        {
            if (DateTime.Today > item.lastUpdate)
            {
                var data = (DateTime.Today - item.Maturity_Date).TotalDays;
                if (data < 0)
                {
                    item.Time_Left_To_Mature = data.ToString();
                    item.lastUpdate=DateTime.Today;
                    db.SaveChanges();
                    return Math.Abs(data).ToString() + " left";
                }
                else
                {
                    item.Time_Left_To_Mature = data.ToString();
                    item.lastUpdate = DateTime.Today;
                    db.SaveChanges();
                    return Math.Abs(data).ToString() + " overdue";
                }
            }
            return item.Time_Left_To_Mature;
        }

        public void duration()
        {
            var list = db.investments.Where(x => x.Uemail == session.HttpContext.Session.GetString("email")).ToList();
            foreach (var item in list)
            {
                if (DateTime.Today > item.lastUpdate)
                {
                    var data = (DateTime.Today - item.Maturity_Date).TotalDays;
                    if (data < 0)
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString() + " left";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                    else
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString() + " overdue";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                }
                else if (item.lastUpdate <= DateTime.MinValue)
                {
                    var data = (DateTime.Today - item.Maturity_Date).TotalDays;
                    if (data < 0)
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString()+" left";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                    else
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString()+" overdue";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                }
                
            }

        }
    }
}
