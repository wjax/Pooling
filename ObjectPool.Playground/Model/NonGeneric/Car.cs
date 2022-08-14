namespace ObjectPool.Playground.Model;

public class Car : Vehicle, IPoolable
{
    public string Engine { get; set; }
    
    public void Reset()
    {
        Name = "";
        Engine = "";
        Speed = 0;
    }
    
    public void Dispose()
    {
        ObjectPool.ReturnToPool(this);
    }
}