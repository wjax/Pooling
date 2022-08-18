namespace ObjectPool.Playground.Model.WithDispose;

public abstract class Vehicle : IPoolable, IDisposable
{
    private bool disposedValue;

    public string Name  { get; set; }
    public double Speed { get; set; }

    public virtual void Reset()
    {
        Console.WriteLine("Vehicle reset");
        Name = "";
        Speed = 0;
    }

    public Action ReturnToPool { get; set; }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Console.WriteLine("Vehicle dispose");
                //Reset();
                ReturnToPool();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Vehicle()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }


    // protected virtual void Dispose(bool disposing)
    // {
    //     if (disposing)
    //     {
    //     }
    // }
    //
    // public void Dispose()
    // {
    //     Dispose(true);
    //     GC.SuppressFinalize(this);
    // }
}