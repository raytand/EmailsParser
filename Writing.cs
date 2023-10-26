using EmailsParser.Interfaces;

namespace EmailsParser
{
    public class Writing : IWritingService
    {
        public async Task WriteMailAsync(string mail, string path)
        {
            try
            {
                using (var stream = new StreamWriter(path, true))
                {
                    await stream.WriteLineAsync(mail);
                    stream.Close();
                }

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task WriteNotFoundAsync(string fileName, string path)
        {
            try
            {
                using (var stream = new StreamWriter(path, true))
                {
                    await stream.WriteLineAsync(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return;
            }
        }
    }
}
