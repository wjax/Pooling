namespace ObjectPool.Playground.Model;

public class GenericPooledCar : GenericPooledVehicle, IPoolable
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
        GenericObjectPool<GenericPooledCar>.ReturnToPool(this);
    }
}