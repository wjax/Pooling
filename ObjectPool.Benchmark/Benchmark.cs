using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.ObjectPool;
using ObjectPool.Benchmark.Model.StaticGeneric;
using ObjectPool.Native;

namespace ObjectPool.Benchmark;

[MemoryDiagnoser]
//[SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 100)]
public class Benchmark
{
    private List<Car> cars = new();
    private DefaultObjectPool<Car> msPool = new(new DefaultPooledObjectPolicy<Car>(), 10);
    
    [Params(1, 10)]
    public int NumberOfRuns { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Pool<Car>.MaxPoolSize = 10;
        List<Car> cars = new(10);
        
        // Generic Pool
        for (int i = 0; i < 10; i++)
        {
            cars.Add(Pool<Car>.Get());
        }
        foreach (var car in cars)
        {
            //GenericPool<Car>.Return(car);
            car.ReturnToPool();
        }
        cars.Clear();
        
        // MS Pool
        for (int i = 0; i < 10; i++)
        {
            cars.Add(msPool.Get());
        }
        foreach (var car in cars)
        {
            msPool.Return(car);
        }
        cars.Clear();
    }

    [Benchmark]
    public void GenericPool()
    {
        for (int run = 0; run < NumberOfRuns; run++)
        {
            for (int i = 0; i < 10; i++)
            {
                cars.Add(Pool<Car>.Get());
            }
            foreach (var car in cars)
            {
                car.ReturnToPool();
                //GenericPool<Car>.Return(car);
            }
            cars.Clear();
        }

        cars.Clear();
    }
    
    [Benchmark]
    public void NativePool()
    {
        for (int run = 0; run < NumberOfRuns; run++)
        {
            for (int i = 0; i < 10; i++)
            {
                cars.Add(NativePool<Car>.Get());
            }
            foreach (var car in cars)
            {
                car.ReturnToPool();
                //GenericPool<Car>.Return(car);
            }
            cars.Clear();
        }

        cars.Clear();
    }

    [Benchmark (Baseline = true)]
    public void MSObjectPool()
    {
        for (int run = 0; run < NumberOfRuns; run++)
        {
            for (int i = 0; i < 10; i++)
            {
                cars.Add(msPool.Get());
            }
            foreach (var car in cars)
            {
                msPool.Return(car);
            }
            cars.Clear();
        }
    }
}