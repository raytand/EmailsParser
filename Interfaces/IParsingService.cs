namespace EmailsParser.Interfaces
{
    public interface IParsingService
    {
        Task ParseAsync(string path);
    }
}
