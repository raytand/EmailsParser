namespace EmailsParser.Interfaces
{
    public interface IWritingService
    {

        Task WriteMailAsync(string mail, string path);
        Task WriteNotFoundAsync(string fileName, string path);
    }
}
