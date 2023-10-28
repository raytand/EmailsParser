using EmailsParser.Interfaces;

namespace EmailsParser
{
    public class Writing : IWritingService
    {
        public async Task WriteMailAsync(string email, string path)
        {
            try
            {
                using (var stream = new StreamWriter(path, true))
                {
                    if (File.Exists(path))
                    {
                        await Console.Out.WriteLineAsync(email);
                        await stream.WriteLineAsync(email);

                    }
                    else
                    {
                        var fileController = File.Create(path);
                        fileController.Close();
                        await Console.Out.WriteLineAsync(email);
                        await stream.WriteLineAsync(email);

                    }

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
                    if (File.Exists(path))
                    {
                        await stream.WriteLineAsync(fileName);

                    }
                    else
                    {
                        var fileController = File.Create(path);
                        fileController.Close();
                        await stream.WriteLineAsync(fileName);

                    }

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
