namespace ObjectPool.Playground.Model.WithDispose;

public class Car : Vehicle, INativePoolingFactory<Car>
{
    public string Engine { get; set; }

    public static Car Get() => Native.NativePool<Car>.Get();

    public override void Reset()
    {
        Console.WriteLine("Car reset");
        base.Reset();
    }
}