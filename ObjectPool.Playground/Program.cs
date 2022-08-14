// See https://aka.ms/new-console-template for more information

using ObjectPool;
using ObjectPool.Playground.Model;

Console.WriteLine("Hello, World!");

Console.WriteLine($"Objects in Pool: {ObjectPool<Car>.ObjectsInPool}");
{
    using var bmwSerie3 = ObjectPool<Car>.RentFromPool();
    Console.WriteLine($"Objects in Pool: {ObjectPool<Car>.ObjectsInPool}");
}
Console.WriteLine($"Objects in Pool: {ObjectPool<Car>.ObjectsInPool}");

