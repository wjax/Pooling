using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    /// <summary>
    /// An interface for classes to implement an static Pool Get method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPoolingFactory<T> where T : class, IPoolable, new()
    {
        /// <summary>
        /// Method that will return an object from a static generic pool
        /// </summary>
        /// <returns></returns>
        static abstract T Get();
    }
}
