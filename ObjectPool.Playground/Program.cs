// See https://aka.ms/new-console-template for more information

using ObjectPool;
using ObjectPool.Native;
using ObjectPool.Playground.Model.WithDispose;

// Console.WriteLine("Generic");
//
// Console.WriteLine($"Objects in Pool: {GenericObjectPool<GenericPooledCar>.ObjectsInPool}");
// {
//     using var bmwSerie3 = GenericObjectPool<GenericPooledCar>.RentFromPool();
//     Console.WriteLine($"Objects in Pool: {GenericObjectPool<GenericPooledCar>.ObjectsInPool}");
// }
// Console.WriteLine($"Objects in Pool: {GenericObjectPool<GenericPooledCar>.ObjectsInPool}");
//
// Console.WriteLine();
// Console.WriteLine();
//
// Console.WriteLine("Non - Generic");
//
// Console.WriteLine($"Objects in Pool: {ObjectPool.ObjectPool.ObjectsInPool<Car>()}");
// {
//     using var bmwSerie3 = ObjectPool.ObjectPool.RentFromPool<Car>();
//     Console.WriteLine($"Objects in Pool: {ObjectPool.ObjectPool.ObjectsInPool<Car>()}");
// }
// Console.WriteLine($"Objects in Pool: {ObjectPool.ObjectPool.ObjectsInPool<Car>()}");

// Console.WriteLine($"Objects in Pool: {GenericPool<Car>.Count}");
// {
//     var bmwSerie3 = GenericPool<Car>.Get();
//     Console.WriteLine($"Objects in Pool: {GenericPool<Car>.Count}");
//     
//     Vehicle vehicle = bmwSerie3;
//     vehicle.ReturnToPool();
// }
// Console.WriteLine($"Objects in Pool: {GenericPool<Car>.Count}");

//Console.WriteLine($"Objects in Pool: {NativePool<Car>.Count}");
{
    var bmwSerie3 = Car.Get();
    Console.WriteLine($"Objects in Pool: {NativePool<Car>.Count}");

    //bmwSerie3.ReturnToPool();

    Vehicle vehicle = bmwSerie3;
    vehicle.Dispose();
    //vehicle.ReturnToPool();
}
Console.WriteLine($"Objects in Pool: {NativePool<Car>.Count}");

