using DataAccess.Models;
using MongoDB.Bson;

namespace DataAccess;

public class HumanManager : IRepository<Human>
{
    private readonly HumanContext _humanContext;

    public HumanManager()
    {
        _humanContext = new HumanContext();
    }

    public void Add(Human item)
    {
        _humanContext.Add(item);
        _humanContext.SaveChanges();
    }

    public IEnumerable<Human> GetAll()
    {
        return _humanContext.People;
    }

    public Human GetById(object id)
    {
        return _humanContext.People.FirstOrDefault(h => h.Id == (int)id);
    }

    public void Replace(object id, Human item)
    {
        var existingHuman = _humanContext.People.FirstOrDefault(h => h.Id == (int)id);
        if (existingHuman != null)
        {
            existingHuman.FirstName = item.FirstName;
            existingHuman.LastName = item.LastName;
            existingHuman.Age = item.Age;
            _humanContext.Update(existingHuman);
            _humanContext.SaveChanges();
        }
    }

    public void Delete(object id)
    {
        var h = _humanContext.People.FirstOrDefault(h => h.Id == (int)id);
        if (h is null)
        {
            return;
        }
        _humanContext.People.Remove(h);
        _humanContext.SaveChanges();
    }
}