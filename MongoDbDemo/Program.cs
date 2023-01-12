using DataAccess;
using DataAccess.Models;

var peopleManager = new PeopleManager();

ConsoleKey key = ConsoleKey.Enter;

while (key != ConsoleKey.Escape)
{
    Console.WriteLine("Enter first and last name seperated by a space:");
    var names = Console.ReadLine().Split(' ');
    Console.WriteLine("Enter an age:");
    var age = int.Parse(Console.ReadLine());
    Console.WriteLine("Press escape to stop entering people.");
    key = Console.ReadKey().Key;
    var newPerson = new Person() { Age = age, FirstName = names[0], LastName = names[1] };

    peopleManager.Add(newPerson);
}

var all = peopleManager.GetAll();
Console.WriteLine("==========================================");

foreach (var person in all)
{
    Console.WriteLine();
    Console.WriteLine($"Id: {person.Id}");
    Console.WriteLine($"First Name: {person.FirstName}");
    Console.WriteLine($"Last Name: {person.LastName}");
    Console.WriteLine($"Age: {person.Age}");
    Console.WriteLine();
}