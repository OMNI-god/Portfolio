using Portfolio.Models;

namespace Portfolio.Data.Iservices
{
    public interface IEventServices
    {
        public void add(Event e);
        public void remove(int id);
        public void edit(Event e);
        public Event getbyid(int id);
    }
}
