using Application.Interface;
using Ninject;

namespace Application.Helper
{
    /// <summary>
    /// Class to get instance to do db operations
    /// </summary>
    public class GetInstance : IInstanceDB
    {
        public readonly IKernel Kernel;

        public GetInstance()
        {
            if (Kernel == null) Kernel = new StandardKernel(new DependencyResolver());
        }

        /// <summary>
        /// Generic method to get any type of db class from factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
