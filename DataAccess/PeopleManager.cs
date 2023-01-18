using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess;

public class PeopleManager : IRepository<Person>
{
    private readonly IMongoCollection<Person> _collection;
    public PeopleManager()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://niklas:Apa123@cluster0.j0lyf.mongodb.net/?retryWrites=true&w=majority");
        var client = new MongoClient(settings);
        var database = client.GetDatabase("People");

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

    public Person GetById(object id)
    {
        return _collection
            .Find(p => p.Id == ObjectId.Parse((string)id)).FirstOrDefault();
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