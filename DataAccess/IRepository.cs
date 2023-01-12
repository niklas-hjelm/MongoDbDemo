namespace DataAccess;

public interface IRepository<T>
{
    void Add(T item);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByName(string firstName, string lastName);
    void UpdateFirstName(object id, string firstName);
    void UpdateLastName(object id, string lastName);
    void UpdateAge(object id, int age);
    void Replace(object id, T item);
    void Delete(object id);
}