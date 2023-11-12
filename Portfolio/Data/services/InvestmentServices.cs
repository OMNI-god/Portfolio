using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Portfolio.Data.Iservices;
using Portfolio.Models;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Portfolio.Data.services
{
    public class InvestmentServices : IInvestmentServices
    {
        private readonly AppliDB db;
        private readonly IHttpContextAccessor session;
        private readonly ILogsServices services;
        public InvestmentServices(AppliDB db, IHttpContextAccessor session,ILogsServices services)
        {
            this.db = db;
            this.session = session;
            this.services=services;
        }

        public void add(Investment investment)
        {
            investment.Bank_Name = investment.Bank_Name.ToUpper();
            investment.Type = investment.Type.ToUpper();
            investment.Time_Left_To_Mature = duration(investment);
            investment.Uemail = session.HttpContext.Session.GetString("email");
            investment.lastUpdate = DateTime.Today;
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
            matured();
            ledger();
            return getData().ToList();
        }

        public IEnumerable<Investment> getData()
        {
            var data = db.investments.Where(x => x.Uemail == session.HttpContext.Session.GetString("email"));
            return data;
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
            var data = getbyid(id);
            db.investments.Remove(data);
            services.add(new Logs
            {
                Number=data.Number,
                Bank_Name=data.Bank_Name,
                Type=data.Type,
                ROI=data.ROI,
                Investment_Start_Date=data.Investment_Start_Date,
                Maturity_Date=data.Maturity_Date,
                Investment_Amount=data.Investment_Amount,
                Maturity_Amount=data.Maturity_Amount,
                Uemail=data.Uemail,
                Action="Deleted"
            });
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
                    return Math.Abs(data).ToString() + " days left";
                }
                else
                {
                    item.Time_Left_To_Mature = data.ToString();
                    item.lastUpdate = DateTime.Today;
                    db.SaveChanges();
                    return Math.Abs(data).ToString() + " days overdue";
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
                        item.Time_Left_To_Mature = Math.Abs(data).ToString() + " days left";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                    else
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString() + " days overdue";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                }
                else if (item.lastUpdate <= DateTime.MinValue)
                {
                    var data = (DateTime.Today - item.Maturity_Date).TotalDays;
                    if (data < 0)
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString()+" days left";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                    else
                    {
                        item.Time_Left_To_Mature = Math.Abs(data).ToString()+" days overdue";
                        item.lastUpdate = DateTime.Today;
                        db.SaveChanges();
                    }
                }
                
            }

        }
        private void matured()
        {
            var data = db.investments.Where(x => x.Maturity_Date < DateTime.Today&&x.Uemail==session.HttpContext.Session.GetString("email")).ToList();
            foreach (var item in data)
            {
                db.logs.Add(
                    new Logs
                    {
                        Number = item.Number,
                        Bank_Name = item.Bank_Name,
                        Type = item.Type,
                        ROI = item.ROI,
                        Investment_Start_Date = item.Investment_Start_Date,
                        Maturity_Date = item.Maturity_Date,
                        Investment_Amount = item.Investment_Amount,
                        Maturity_Amount = item.Maturity_Amount,
                        Uemail = item.Uemail,
                        Action ="Matured"
                    }
                        );
                db.investments.Remove(item);
                db.SaveChanges();
            }
        }

        public void restore(int id)
        {
            var data = db.logs.FirstOrDefault(x => x.Id == id);
            Investment i = new Investment
            {
                Number = data.Number,
                Bank_Name = data.Bank_Name,
                Type = data.Type,
                ROI = data.ROI,
                Investment_Start_Date = data.Investment_Start_Date,
                Maturity_Date = data.Maturity_Date,
                Investment_Amount = data.Investment_Amount,
                Maturity_Amount = data.Maturity_Amount,
            };
            add(i);
            db.logs.Remove(data);
            db.SaveChanges();
        }

        public byte[] downloadDetails()
        {
            var data = getData().ToList();
            byte[] excelFileBytes;
            using (var excel =new ExcelPackage())
            {
                var worksheet = excel.Workbook.Worksheets.Add("Investments");
                worksheet.Cells["A1"].LoadFromCollection(data,true);
                worksheet.DeleteColumn(1);
                worksheet.DeleteColumn(10,11);
                worksheet.Cells[2, 5, worksheet.Dimension.End.Row, 5]
                    .Style.Numberformat.Format = "yyyy-mm-dd";
                worksheet.Cells[2, 6, worksheet.Dimension.End.Row, 6]
                    .Style.Numberformat.Format = "yyyy-mm-dd";
                worksheet.Cells[1, 1, 1, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1, 1, 9].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                worksheet.Cells.AutoFitColumns();
                excelFileBytes =excel.GetAsByteArray();
            }
            return excelFileBytes;
        }

        public void upload(IFormFile file)
        {
            DataTable dt = new DataTable();
            using(var excel=new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];
                dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
                {
                    c.FirstRowIsColumnNames = true;
                });
            }
            if(dt!=null && dt.Rows.Count > 0)
            {
                addToDatabase(dt);
            }
        }

        private void addToDatabase(DataTable dt)
        {
            var data=getData().Select(row=>row.Number).ToList();
            var uncommonData = dt.AsEnumerable()
                .Where(row => !data.Contains(row["Number"].ToString()))
                .CopyToDataTable();
            if(uncommonData!=null && uncommonData.Rows.Count > 0)
            {
                foreach(DataRow row in uncommonData.Rows)
                {
                    try
                    {
                        add(new Investment
                        {
                            Number = row["Number"].ToString(),
                            Bank_Name = row["Bank Name"].ToString(),
                            Type = row["Type"].ToString(),
                            ROI = row["ROI"].ToString(),
                            Investment_Start_Date = (DateTime)row["Investment Start Date"],
                            Maturity_Date = (DateTime)row["Maturity Date"],
                            Investment_Amount = (double)row["Investment Amount"],
                            Maturity_Amount = (double)row["Maturity Amount"]
                        });
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
        }

        public byte[] uploadTemplate()
        {
            byte[] excelFileBytes;
            using (var excel = new ExcelPackage())
            {
                var worksheet = excel.Workbook.Worksheets.Add("Investments");
                worksheet.Cells[1, 1].Value = "Number";
                worksheet.Cells[1, 2].Value = "Bank Name";
                worksheet.Cells[1, 3].Value = "Type";
                worksheet.Cells[1, 4].Value = "ROI";
                worksheet.Cells[1, 5].Value = "Investment Start Date";
                worksheet.Cells[1, 6].Value = "Maturity Date";
                worksheet.Cells[1, 7].Value = "Investment Amount";
                worksheet.Cells[1, 8].Value = "Maturity Amount";
                var dateStyle = worksheet.Workbook.Styles.CreateNamedStyle("DateStyle");
                dateStyle.Style.Numberformat.Format = "dd-mm-yyyy";
                worksheet.Cells[1, 5, 1, 5].StyleName = "DateStyle";
                worksheet.Cells[1, 6, 1, 6].StyleName = "DateStyle";
                worksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                worksheet.Cells.AutoFitColumns();
                excelFileBytes = excel.GetAsByteArray();
            }
            return excelFileBytes;
        }
    }
}
