namespace ObjectPool.Playground.Model.StaticGeneric;

public class Vehicle : IPoolable
{
    public string Name  { get; set; }
    public double Speed { get; set; }

    public virtual void Reset()
    {
        Name = "";
        Speed = 0;
    }

    public Action ReturnToPool { get; set; }
}