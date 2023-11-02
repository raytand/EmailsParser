namespace EmailsParser
{
    public class ConsoleInput
    {
        private static string _path;
        public Task<string> PathInput()
        {
            try
            {
                while (_path == null) // Could be an infinity loop? 
                {
                    Console.WriteLine("Enter path to folder");
                    _path = Console.ReadLine();
                    if (_path == null)
                    {
                        Console.WriteLine("Can not use null reference");
                    }
                }
                return Task.FromResult<string>(_path);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult<string>(_path);
            }
        }
    }
}
