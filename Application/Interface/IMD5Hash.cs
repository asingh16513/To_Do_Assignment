namespace Application.Interface
{
    /// <summary>
    /// Interface for creating hash 
    /// </summary>
    public interface IMD5Hash
    {
        string GetMD5Hash(string text);
    }
}
