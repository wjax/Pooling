namespace ObjectPool.Benchmark.Model.StaticGeneric;

public class Car : Vehicle
{
    public string? Engine { get; set; }

    public override void Reset()
    {
        //Console.WriteLine("Car reset");
    }
}