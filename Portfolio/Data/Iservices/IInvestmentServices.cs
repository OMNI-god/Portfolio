using Portfolio.Models;

namespace Portfolio.Data.Iservices
{
    public interface IInvestmentServices
    {
        List<Investment> getAll();
        void add(Investment investment);
        void remove(int id);
        void update(Investment investment);
        Investment getbyid(int id);
        void restore(int id);
        void downloadDetails();
    }
}
