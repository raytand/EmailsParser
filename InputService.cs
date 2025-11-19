using EmailsParser.Interfaces;

namespace EmailsParser
{
    public class InputService : IInputService
    {
        private static string path;
        public async Task<string> PathInput()
        {
            try
            {
                while (path == null)
                {
                    await Console.Out.WriteLineAsync("Enter path to folder");
                    path = await Console.In.ReadLineAsync();
                    if (path == null)
                    {
                        await Console.Out.WriteLineAsync("Can not use null reference");
                    }
                }
                return path;

            }
            catch (Exception ex) 
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return path;
            }
        }
    }
}
