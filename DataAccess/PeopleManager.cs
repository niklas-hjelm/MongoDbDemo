using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess;

public class PeopleManager : IRepository<Person>
{
    private readonly IMongoCollection<Person> _collection;
    public PeopleManager()
    {
        var hostname = "localhost";
        var databaseName = "demo";
        var connectionString = $"mongodb://{hostname}:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<Person>("people", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public void Add(Person item)
    {
        _collection.InsertOne(item);
    }

    public IEnumerable<Person> GetAll()
    {
        return _collection.Find(_ => true).ToEnumerable();
    }

    public IEnumerable<Person> GetByName(string firstName, string lastName)
    {
        return _collection
            .Find(p =>
                (!string.IsNullOrEmpty(firstName) && p.FirstName == firstName)
                && (!string.IsNullOrEmpty(lastName) && p.LastName == lastName))
            .ToEnumerable();
    }

    public void UpdateFirstName(object id, string firstName)
    {
        var filter = Builders<Person>.Filter.Eq("Id", id);
        var update = Builders<Person>
            .Update
            .Set("FirstName", firstName);

        _collection
            .FindOneAndUpdate(
                filter,
                update,
                new FindOneAndUpdateOptions<Person, Person>
                {
                    IsUpsert = true
                }
            );
    }

    public void UpdateLastName(object id, string lastName)
    {
        var filter = Builders<Person>.Filter.Eq("Id", id);
        var update = Builders<Person>
            .Update
            .Set("LastName", lastName);

        _collection
            .FindOneAndUpdate(
                filter,
                update,
                new FindOneAndUpdateOptions<Person, Person>
                {
                    IsUpsert = true
                }
            );
    }


    public void UpdateAge(object id, int age)
    {
        var filter = Builders<Person>.Filter.Eq("Id", id);
        var update = Builders<Person>
            .Update
            .Set("Age", age);

        _collection
            .FindOneAndUpdate(
                filter,
                update,
                new FindOneAndUpdateOptions<Person, Person>
                {
                    IsUpsert = true
                }
            );
    }

    public void Replace(object id, Person item)
    {
        var filter = Builders<Person>.Filter.Eq("Id", id);
        var update = Builders<Person>
            .Update
            .Set("Age", item.Age)
            .Set("FirstName", item.FirstName)
            .Set("LastName", item.LastName);

        _collection
            .FindOneAndUpdate(
                filter,
                update,
                new FindOneAndUpdateOptions<Person, Person>
                {
                    IsUpsert = true
                }
            );
    }
    public void Delete(object id)
    {
        var filter = Builders<Person>.Filter.Eq("Id", id);
        _collection.FindOneAndDelete(filter);
    }
}