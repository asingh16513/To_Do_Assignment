namespace Application.Interface
{
    /// <summary>
    /// Interface to get instance for DB classes
    /// </summary>
    public interface IInstanceDB
    {
        T Get<T>();
    }
}
