namespace ObjectPool.Playground.Model;

public class Car : Vehicle
{
    public string Engine { get; set; }

    public override void Reset()
    {
        Console.WriteLine("Car reset");
    }
}